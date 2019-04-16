using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cyotek.RegistryComparer.Client
{
  internal sealed partial class MainForm : BaseForm
  {
    #region Constructors

    public MainForm()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);

      hiveRadioButton.Checked = true;
      keysTextBox.Bounds = hivesListBox.Bounds;
      this.LoadRootHives();
      this.LoadCustomKeys();

      outputFolderTextBox.Text = Environment.CurrentDirectory;
    }

    private void aboutButton_Click(object sender, EventArgs e)
    {
      using (AboutDialog dialog = new AboutDialog())
      {
        dialog.ShowDialog(this);
      }
    }

    private void AddCustomKey(string key)
    {
      keysTextBox.AppendText(key + Environment.NewLine);
    }

    private void AddHive(string name, bool select)
    {
      RegistryKeySnapshot item;

      item = new RegistryKeySnapshot(name);

      hivesListBox.Items.Add(item);

      if (select)
      {
        hivesListBox.SetItemChecked(hivesListBox.Items.Count - 1, true);
      }
    }

    private void AddSnapshot(RegistrySnapshot snapshot)
    {
      this.AddSnapshot(snapshot.FileName);
    }

    private void AddSnapshot(string fileName)
    {
      FileInfo info;

      info = new FileInfo(fileName);

      snapshotsListBox.Items.Add(info);
      firstComboBox.Items.Add(info);
      secondComboBox.Items.Add(info);
    }

    private void compareBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      TaskOptions options;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;
      RegistrySnapshotComparer comparer;

      options = (TaskOptions)e.Argument;
      lhs = RegistrySnapshot.LoadFromFile(options.FileName1);
      rhs = RegistrySnapshot.LoadFromFile(options.FileName2);
      comparer = new RegistrySnapshotComparer(lhs, rhs);

      e.Result = comparer.Compare();
    }

    private void compareBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.SetStatus(null);

      if (e.Error != null)
      {
        MessageBox.Show(e.Error.GetBaseException().
                          Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else
      {
        this.HandleCompareResults((ChangeResult[])e.Result);
      }
    }

    private void compareButton_Click(object sender, EventArgs e)
    {
      if (firstComboBox.SelectedItem == null || secondComboBox.SelectedItem == null)
      {
        MessageBox.Show("Please select two snapshots to compare.", this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
      }
      else
      {
        TaskOptions options;

        this.SetStatus("Comparing snapshots...");

        options = new TaskOptions
        {
          FileName1 = ((FileInfo)firstComboBox.SelectedItem).FullPath,
          FileName2 = ((FileInfo)secondComboBox.SelectedItem).FullPath
        };

        compareBackgroundWorker.RunWorkerAsync(options);
      }
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      if (snapshotsListBox.SelectedItems.Count == 0)
      {
        MessageBox.Show("Please select the snapshot files to delete.", this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
      }
      else if (
        MessageBox.Show("Are you sure you want to delete the selected snapshots?", this.Text, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.Yes)
      {
        List<FileInfo> filesToDelete;

        filesToDelete = snapshotsListBox.SelectedItems.Cast<FileInfo>().
                                         ToList();

        foreach (FileInfo file in filesToDelete)
        {
          try
          {
            File.Delete(file.FullPath);
            this.RemoveSnapshot(file);
          }
          catch (Exception ex)
          {
            MessageBox.Show($"Failed to delete file. {ex.GetBaseException().Message}", this.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }

    private void exitButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void firstComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetToolTip(firstComboBox);
    }

    private string[] GetKeysToScan()
    {
      List<string> keys;

      keys = new List<string>();

      if (hiveRadioButton.Checked)
      {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (object item in hivesListBox.CheckedItems)
        {
          keys.Add(((RegistryKeySnapshot)item).Name);
        }
      }
      else
      {
        foreach (string rawKey in keysTextBox.Lines)
        {
          string key;

          key = rawKey.Trim();

          if (!string.IsNullOrEmpty(key) && !keys.Contains(key))
          {
            keys.Add(key);
          }
        }
      }

      return keys.ToArray();
    }

    private void HandleCompareResults(ChangeResult[] results)
    {
      if (results.Length == 0)
      {
        MessageBox.Show("No differences found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        using (ViewCompareResultsDialog dialog = new ViewCompareResultsDialog(results))
        {
          dialog.ShowDialog(this);
        }
      }
    }

    private void hiveRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      bool useHives;

      useHives = hiveRadioButton.Checked;

      hivesListBox.Visible = useHives;
      keysTextBox.Visible = !useHives;
    }

    private void LoadCustomKeys()
    {
      keysTextBox.Clear();

      this.AddCustomKey("HKEY_CURRENT_USER\\SOFTWARE");
      this.AddCustomKey("HKEY_LOCAL_MACHINE\\SOFTWARE");
      //this.AddCustomKey(@"HKEY_CURRENT_USER\SOFTWARE\Cyotek");
    }

    private void loadFileListDelayTimer_Tick(object sender, EventArgs e)
    {
      string path;

      loadFileListDelayTimer.Stop();

      path = outputFolderTextBox.Text;

      if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
      {
        snapshotsListBox.BeginUpdate();

        snapshotsListBox.Items.Clear();
        firstComboBox.Items.Clear();
        secondComboBox.Items.Clear();

        foreach (string fileName in Directory.GetFiles(path, "*.rge"))
        {
          this.AddSnapshot(fileName);
        }

        snapshotsListBox.EndUpdate();
      }
    }

    private void LoadRootHives()
    {
      hivesListBox.BeginUpdate();

      this.AddHive("HKEY_CLASSES_ROOT", false);
      this.AddHive("HKEY_CURRENT_USER", true);
      this.AddHive("HKEY_LOCAL_MACHINE", true);
      this.AddHive("HKEY_USERS", false);
      this.AddHive("HKEY_CURRENT_CONFIG", false);
      // this.AddHive("HKEY_PERFORMANCE_DATA", false);
      // this.AddHive("HKEY_DYN_DATA", false);

      hivesListBox.EndUpdate();
    }

    private void outputFolderBrowseButton_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog dialog = new FolderBrowserDialog
      {
        Description = "Select the &folder to store snapshots",
        ShowNewFolderButton = true,
        SelectedPath = outputFolderTextBox.Text
      })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          outputFolderTextBox.Text = dialog.SelectedPath;
        }
      }
    }

    private void outputFolderTextBox_TextChanged(object sender, EventArgs e)
    {
      loadFileListDelayTimer.Stop();
      loadFileListDelayTimer.Start();
    }

    private void RemoveSnapshot(FileInfo file)
    {
      snapshotsListBox.Items.Remove(file);
      firstComboBox.Items.Remove(file);
      secondComboBox.Items.Remove(file);
    }

    private void secondComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SetToolTip(secondComboBox);
    }

    private void SetStatus(string message)
    {
      statusToolStripStatusLabel.Text = message;

      this.SetControls(string.IsNullOrEmpty(message));
      this.UseWaitCursor = !string.IsNullOrEmpty(message);
    }

    private void SetControls(bool enabled)
    {
      foreach (Control control in this.Controls)
      {
        if (!object.ReferenceEquals(control, exitButton))
        {
          control.Enabled = enabled;
        }
      }
    }

    private void SetToolTip(ComboBox comboBox)
    {
      FileInfo info;

      info = comboBox.SelectedItem as FileInfo;

      toolTip.SetToolTip(comboBox, info?.FullPath);
    }

    private void snapshotBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      TaskOptions options;
      RegistrySnapshot snapshot;
      string fileName;
      string name;

      options = (TaskOptions)e.Argument;
      name = DateTime.Now.ToString("yyyyMMdd HHmm");
      fileName = PathHelpers.GetUniqueFileName(name, options.OutputPath);

      snapshot = RegistrySnapshot.TakeSnapshot(options.Keys);
      snapshot.Name = name;
      snapshot.Save(fileName);

      e.Result = snapshot;
    }

    private void snapshotBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.SetStatus(null);

      if (e.Error != null)
      {
        MessageBox.Show(e.Error.GetBaseException().
                          Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else
      {
        RegistrySnapshot snapshot;

        snapshot = (RegistrySnapshot)e.Result;

        this.AddSnapshot(snapshot);

        MessageBox.Show(string.Format("Snapshot created.\n\nFile saved to '{0}'.", snapshot.FileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void takeSnapshotButton_Click(object sender, EventArgs e)
    {
      TaskOptions options;
      string[] keys;
      string path;

      keys = this.GetKeysToScan();
      path = PathHelpers.GetFullPath(outputFolderTextBox.Text);

      this.SetStatus("Taking snapshot...");

      options = new TaskOptions
      {
        Keys = keys,
        OutputPath = path
      };

      snapshotBackgroundWorker.RunWorkerAsync(options);
    }

    private void viewButton_Click(object sender, EventArgs e)
    {
      if (snapshotsListBox.SelectedItem == null)
      {
        MessageBox.Show("Please select the snapshot to view.", this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
      }
      else
      {
        RegistrySnapshot snapshot;
        string fileName;

        this.SetStatus("Loading snapshot...");

        fileName = ((FileInfo)snapshotsListBox.SelectedItem).FullPath;

        snapshot = RegistrySnapshot.LoadFromFile(fileName);

        this.SetStatus(null);

        using (ViewDialog dialog = new ViewDialog(snapshot))
        {
          dialog.ShowDialog(this);
        }
      }
    }

    #endregion
  }
}
