using System;
using System.Windows.Forms;

namespace Cyotek.RegistryComparer.Client
{
  internal partial class ViewDialog : Form
  {
    #region Constants

    private readonly RegistrySnapshot _snapshot;

    #endregion

    #region Constructors

    public ViewDialog()
    {
      this.InitializeComponent();
    }

    public ViewDialog(RegistrySnapshot snapshot)
      : this()
    {
      _snapshot = snapshot;
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

      if (_snapshot != null)
      {
        treeView.Snapshot = _snapshot;

        if (treeView.Nodes.Count != 0)
        {
          treeView.SelectedNode = treeView.Nodes[0];
        }
      }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      TreeNode node;
      RegistryValueSnapshotCollection values;

      node = e.Node;

      if (node != null)
      {
        RegistryKeySnapshot key;

        key = (RegistryKeySnapshot)e.Node.Tag;

        values = key.Values;
      }
      else
      {
        values = null;
      }

      listView.Values = values;
    }

    #endregion
  }
}
