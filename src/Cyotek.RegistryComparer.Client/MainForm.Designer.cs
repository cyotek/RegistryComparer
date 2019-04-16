namespace Cyotek.RegistryComparer.Client
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.hivesGroupBox = new System.Windows.Forms.GroupBox();
      this.keysTextBox = new System.Windows.Forms.TextBox();
      this.keyRadioButton = new System.Windows.Forms.RadioButton();
      this.hiveRadioButton = new System.Windows.Forms.RadioButton();
      this.hivesListBox = new System.Windows.Forms.CheckedListBox();
      this.takeSnapshotButton = new System.Windows.Forms.Button();
      this.compareButton = new System.Windows.Forms.Button();
      this.deleteButton = new System.Windows.Forms.Button();
      this.aboutButton = new System.Windows.Forms.Button();
      this.exitButton = new System.Windows.Forms.Button();
      this.outputOptionsGroupBox = new System.Windows.Forms.GroupBox();
      this.outputFolderBrowseButton = new System.Windows.Forms.Button();
      this.outputFolderTextBox = new System.Windows.Forms.TextBox();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.statusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.snapshotBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.snapshotsGroupBox = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.firstLabel = new System.Windows.Forms.Label();
      this.secondComboBox = new System.Windows.Forms.ComboBox();
      this.secondLabel = new System.Windows.Forms.Label();
      this.firstComboBox = new System.Windows.Forms.ComboBox();
      this.snapshotsListBox = new System.Windows.Forms.ListBox();
      this.loadFileListDelayTimer = new System.Windows.Forms.Timer(this.components);
      this.viewButton = new System.Windows.Forms.Button();
      this.compareBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.hivesGroupBox.SuspendLayout();
      this.outputOptionsGroupBox.SuspendLayout();
      this.statusStrip.SuspendLayout();
      this.snapshotsGroupBox.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // hivesGroupBox
      // 
      this.hivesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.hivesGroupBox.Controls.Add(this.keysTextBox);
      this.hivesGroupBox.Controls.Add(this.keyRadioButton);
      this.hivesGroupBox.Controls.Add(this.hiveRadioButton);
      this.hivesGroupBox.Controls.Add(this.hivesListBox);
      this.hivesGroupBox.Location = new System.Drawing.Point(14, 78);
      this.hivesGroupBox.Name = "hivesGroupBox";
      this.hivesGroupBox.Size = new System.Drawing.Size(373, 174);
      this.hivesGroupBox.TabIndex = 1;
      this.hivesGroupBox.TabStop = false;
      this.hivesGroupBox.Text = "Source";
      // 
      // keysTextBox
      // 
      this.keysTextBox.AcceptsReturn = true;
      this.keysTextBox.Location = new System.Drawing.Point(173, 105);
      this.keysTextBox.Multiline = true;
      this.keysTextBox.Name = "keysTextBox";
      this.keysTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.keysTextBox.Size = new System.Drawing.Size(116, 22);
      this.keysTextBox.TabIndex = 3;
      // 
      // keyRadioButton
      // 
      this.keyRadioButton.AutoSize = true;
      this.keyRadioButton.Location = new System.Drawing.Point(69, 22);
      this.keyRadioButton.Name = "keyRadioButton";
      this.keyRadioButton.Size = new System.Drawing.Size(44, 19);
      this.keyRadioButton.TabIndex = 1;
      this.keyRadioButton.TabStop = true;
      this.keyRadioButton.Text = "&Key";
      this.keyRadioButton.UseVisualStyleBackColor = true;
      // 
      // hiveRadioButton
      // 
      this.hiveRadioButton.AutoSize = true;
      this.hiveRadioButton.Location = new System.Drawing.Point(7, 22);
      this.hiveRadioButton.Name = "hiveRadioButton";
      this.hiveRadioButton.Size = new System.Drawing.Size(49, 19);
      this.hiveRadioButton.TabIndex = 0;
      this.hiveRadioButton.TabStop = true;
      this.hiveRadioButton.Text = "&Hive";
      this.hiveRadioButton.UseVisualStyleBackColor = true;
      this.hiveRadioButton.CheckedChanged += new System.EventHandler(this.hiveRadioButton_CheckedChanged);
      // 
      // hivesListBox
      // 
      this.hivesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.hivesListBox.FormattingEnabled = true;
      this.hivesListBox.IntegralHeight = false;
      this.hivesListBox.Location = new System.Drawing.Point(7, 48);
      this.hivesListBox.Name = "hivesListBox";
      this.hivesListBox.Size = new System.Drawing.Size(359, 118);
      this.hivesListBox.TabIndex = 2;
      // 
      // takeSnapshotButton
      // 
      this.takeSnapshotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.takeSnapshotButton.Location = new System.Drawing.Point(394, 14);
      this.takeSnapshotButton.Name = "takeSnapshotButton";
      this.takeSnapshotButton.Size = new System.Drawing.Size(87, 27);
      this.takeSnapshotButton.TabIndex = 3;
      this.takeSnapshotButton.Text = "Snapsho&t";
      this.takeSnapshotButton.UseVisualStyleBackColor = true;
      this.takeSnapshotButton.Click += new System.EventHandler(this.takeSnapshotButton_Click);
      // 
      // compareButton
      // 
      this.compareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.compareButton.Location = new System.Drawing.Point(394, 47);
      this.compareButton.Name = "compareButton";
      this.compareButton.Size = new System.Drawing.Size(87, 27);
      this.compareButton.TabIndex = 4;
      this.compareButton.Text = "&Compare";
      this.compareButton.UseVisualStyleBackColor = true;
      this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
      // 
      // deleteButton
      // 
      this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.deleteButton.Location = new System.Drawing.Point(394, 125);
      this.deleteButton.Margin = new System.Windows.Forms.Padding(3, 14, 3, 3);
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new System.Drawing.Size(87, 27);
      this.deleteButton.TabIndex = 6;
      this.deleteButton.Text = "&Delete...";
      this.deleteButton.UseVisualStyleBackColor = true;
      this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
      // 
      // aboutButton
      // 
      this.aboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.aboutButton.Location = new System.Drawing.Point(394, 168);
      this.aboutButton.Margin = new System.Windows.Forms.Padding(3, 14, 3, 3);
      this.aboutButton.Name = "aboutButton";
      this.aboutButton.Size = new System.Drawing.Size(87, 27);
      this.aboutButton.TabIndex = 7;
      this.aboutButton.Text = "&About";
      this.aboutButton.UseVisualStyleBackColor = true;
      this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
      // 
      // exitButton
      // 
      this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.exitButton.Location = new System.Drawing.Point(394, 202);
      this.exitButton.Name = "exitButton";
      this.exitButton.Size = new System.Drawing.Size(87, 27);
      this.exitButton.TabIndex = 8;
      this.exitButton.Text = "E&xit";
      this.exitButton.UseVisualStyleBackColor = true;
      this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
      // 
      // outputOptionsGroupBox
      // 
      this.outputOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.outputOptionsGroupBox.Controls.Add(this.outputFolderBrowseButton);
      this.outputOptionsGroupBox.Controls.Add(this.outputFolderTextBox);
      this.outputOptionsGroupBox.Location = new System.Drawing.Point(14, 14);
      this.outputOptionsGroupBox.Name = "outputOptionsGroupBox";
      this.outputOptionsGroupBox.Size = new System.Drawing.Size(373, 58);
      this.outputOptionsGroupBox.TabIndex = 0;
      this.outputOptionsGroupBox.TabStop = false;
      this.outputOptionsGroupBox.Text = "Output &Folder";
      // 
      // outputFolderBrowseButton
      // 
      this.outputFolderBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.outputFolderBrowseButton.Location = new System.Drawing.Point(279, 22);
      this.outputFolderBrowseButton.Name = "outputFolderBrowseButton";
      this.outputFolderBrowseButton.Size = new System.Drawing.Size(87, 27);
      this.outputFolderBrowseButton.TabIndex = 1;
      this.outputFolderBrowseButton.Text = "&Browse...";
      this.outputFolderBrowseButton.UseVisualStyleBackColor = true;
      this.outputFolderBrowseButton.Click += new System.EventHandler(this.outputFolderBrowseButton_Click);
      // 
      // outputFolderTextBox
      // 
      this.outputFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.outputFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.outputFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
      this.outputFolderTextBox.Location = new System.Drawing.Point(7, 24);
      this.outputFolderTextBox.Name = "outputFolderTextBox";
      this.outputFolderTextBox.Size = new System.Drawing.Size(264, 23);
      this.outputFolderTextBox.TabIndex = 0;
      this.outputFolderTextBox.TextChanged += new System.EventHandler(this.outputFolderTextBox_TextChanged);
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabel});
      this.statusStrip.Location = new System.Drawing.Point(0, 490);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
      this.statusStrip.Size = new System.Drawing.Size(496, 22);
      this.statusStrip.TabIndex = 9;
      // 
      // statusToolStripStatusLabel
      // 
      this.statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
      this.statusToolStripStatusLabel.Size = new System.Drawing.Size(479, 17);
      this.statusToolStripStatusLabel.Spring = true;
      this.statusToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // snapshotBackgroundWorker
      // 
      this.snapshotBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.snapshotBackgroundWorker_DoWork);
      this.snapshotBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.snapshotBackgroundWorker_RunWorkerCompleted);
      // 
      // snapshotsGroupBox
      // 
      this.snapshotsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.snapshotsGroupBox.Controls.Add(this.tableLayoutPanel1);
      this.snapshotsGroupBox.Controls.Add(this.snapshotsListBox);
      this.snapshotsGroupBox.Location = new System.Drawing.Point(14, 260);
      this.snapshotsGroupBox.Name = "snapshotsGroupBox";
      this.snapshotsGroupBox.Size = new System.Drawing.Size(373, 224);
      this.snapshotsGroupBox.TabIndex = 2;
      this.snapshotsGroupBox.TabStop = false;
      this.snapshotsGroupBox.Text = "&Snapshots";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.firstLabel, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.secondComboBox, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.secondLabel, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.firstComboBox, 0, 1);
      this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 171);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(359, 46);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // firstLabel
      // 
      this.firstLabel.AutoSize = true;
      this.firstLabel.Location = new System.Drawing.Point(0, 0);
      this.firstLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
      this.firstLabel.Name = "firstLabel";
      this.firstLabel.Size = new System.Drawing.Size(32, 15);
      this.firstLabel.TabIndex = 0;
      this.firstLabel.Text = "F&irst:";
      // 
      // secondComboBox
      // 
      this.secondComboBox.Dock = System.Windows.Forms.DockStyle.Top;
      this.secondComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.secondComboBox.FormattingEnabled = true;
      this.secondComboBox.Location = new System.Drawing.Point(182, 18);
      this.secondComboBox.Name = "secondComboBox";
      this.secondComboBox.Size = new System.Drawing.Size(174, 23);
      this.secondComboBox.TabIndex = 3;
      this.secondComboBox.SelectedIndexChanged += new System.EventHandler(this.secondComboBox_SelectedIndexChanged);
      // 
      // secondLabel
      // 
      this.secondLabel.AutoSize = true;
      this.secondLabel.Location = new System.Drawing.Point(179, 0);
      this.secondLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
      this.secondLabel.Name = "secondLabel";
      this.secondLabel.Size = new System.Drawing.Size(49, 15);
      this.secondLabel.TabIndex = 2;
      this.secondLabel.Text = "Sec&ond:";
      // 
      // firstComboBox
      // 
      this.firstComboBox.Dock = System.Windows.Forms.DockStyle.Top;
      this.firstComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.firstComboBox.FormattingEnabled = true;
      this.firstComboBox.Location = new System.Drawing.Point(3, 18);
      this.firstComboBox.Name = "firstComboBox";
      this.firstComboBox.Size = new System.Drawing.Size(173, 23);
      this.firstComboBox.TabIndex = 1;
      this.firstComboBox.SelectedIndexChanged += new System.EventHandler(this.firstComboBox_SelectedIndexChanged);
      // 
      // snapshotsListBox
      // 
      this.snapshotsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.snapshotsListBox.FormattingEnabled = true;
      this.snapshotsListBox.IntegralHeight = false;
      this.snapshotsListBox.ItemHeight = 15;
      this.snapshotsListBox.Location = new System.Drawing.Point(7, 17);
      this.snapshotsListBox.Name = "snapshotsListBox";
      this.snapshotsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.snapshotsListBox.Size = new System.Drawing.Size(359, 146);
      this.snapshotsListBox.TabIndex = 0;
      // 
      // loadFileListDelayTimer
      // 
      this.loadFileListDelayTimer.Tick += new System.EventHandler(this.loadFileListDelayTimer_Tick);
      // 
      // viewButton
      // 
      this.viewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.viewButton.Location = new System.Drawing.Point(394, 81);
      this.viewButton.Name = "viewButton";
      this.viewButton.Size = new System.Drawing.Size(87, 27);
      this.viewButton.TabIndex = 5;
      this.viewButton.Text = "&View";
      this.viewButton.UseVisualStyleBackColor = true;
      this.viewButton.Click += new System.EventHandler(this.viewButton_Click);
      // 
      // compareBackgroundWorker
      // 
      this.compareBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.compareBackgroundWorker_DoWork);
      this.compareBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.compareBackgroundWorker_RunWorkerCompleted);
      // 
      // MainForm
      // 
      this.AcceptButton = this.takeSnapshotButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.exitButton;
      this.ClientSize = new System.Drawing.Size(496, 512);
      this.Controls.Add(this.viewButton);
      this.Controls.Add(this.snapshotsGroupBox);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.outputOptionsGroupBox);
      this.Controls.Add(this.exitButton);
      this.Controls.Add(this.aboutButton);
      this.Controls.Add(this.deleteButton);
      this.Controls.Add(this.compareButton);
      this.Controls.Add(this.takeSnapshotButton);
      this.Controls.Add(this.hivesGroupBox);
      this.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Cyotek Registry Comparer";
      this.hivesGroupBox.ResumeLayout(false);
      this.hivesGroupBox.PerformLayout();
      this.outputOptionsGroupBox.ResumeLayout(false);
      this.outputOptionsGroupBox.PerformLayout();
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.snapshotsGroupBox.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox hivesGroupBox;
    private System.Windows.Forms.Button takeSnapshotButton;
    private System.Windows.Forms.Button compareButton;
    private System.Windows.Forms.Button deleteButton;
    private System.Windows.Forms.Button aboutButton;
    private System.Windows.Forms.Button exitButton;
    private System.Windows.Forms.CheckedListBox hivesListBox;
    private System.Windows.Forms.GroupBox outputOptionsGroupBox;
    private System.Windows.Forms.Button outputFolderBrowseButton;
    private System.Windows.Forms.TextBox outputFolderTextBox;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabel;
    private System.ComponentModel.BackgroundWorker snapshotBackgroundWorker;
    private System.Windows.Forms.GroupBox snapshotsGroupBox;
    private System.Windows.Forms.ListBox snapshotsListBox;
    private System.Windows.Forms.Timer loadFileListDelayTimer;
    private System.Windows.Forms.RadioButton keyRadioButton;
    private System.Windows.Forms.RadioButton hiveRadioButton;
    private System.Windows.Forms.TextBox keysTextBox;
    private System.Windows.Forms.Button viewButton;
    private System.ComponentModel.BackgroundWorker compareBackgroundWorker;
    private System.Windows.Forms.ComboBox secondComboBox;
    private System.Windows.Forms.Label secondLabel;
    private System.Windows.Forms.ComboBox firstComboBox;
    private System.Windows.Forms.Label firstLabel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ToolTip toolTip;
  }
}

