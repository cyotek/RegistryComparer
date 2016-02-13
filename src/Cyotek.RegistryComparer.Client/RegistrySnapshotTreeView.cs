using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Cyotek.RegistryComparer.Client
{
  internal class RegistrySnapshotTreeView : TreeView
  {
    #region Fields

    private RegistrySnapshot _snapshot;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the Snapshot property value changes
    /// </summary>
    [Category("Property Changed")]
    public event EventHandler SnapshotChanged;

    #endregion

    #region Properties

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RegistrySnapshot Snapshot
    {
      get { return _snapshot; }
      set
      {
        if (_snapshot != value)
        {
          _snapshot = value;

          this.OnSnapshotChanged(EventArgs.Empty);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeExpand"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs"/> that contains the event data. </param>
    protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
    {
      TreeNode node;

      node = e.Node;

      if (node.Nodes.Count == 1 && node.Nodes[0].Tag == null)
      {
        RegistryKeySnapshot key;

        key = (RegistryKeySnapshot)node.Tag;

        this.AddKeys(key.SubKeys, node.Nodes);
      }

      base.OnBeforeExpand(e);
    }

    /// <summary>
    /// Raises the <see cref="SnapshotChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnSnapshotChanged(EventArgs e)
    {
      EventHandler handler;

      this.LoadTree();

      handler = this.SnapshotChanged;

      handler?.Invoke(this, e);
    }

    private void AddKey(RegistryKeySnapshot key, TreeNodeCollection container)
    {
      TreeNode node;

      node = new TreeNode
             {
               Name = key.Name,
               Text = key.Name,
               ImageIndex = 0,
               Tag = key
             };

      if (key.SubKeys != null && key.SubKeys.Count != 0)
      {
        node.Nodes.Add(new TreeNode());
      }

      container.Add(node);
    }

    private void AddKeys(RegistryKeySnapshotCollection keys, TreeNodeCollection container)
    {
      container.Clear();

      foreach (RegistryKeySnapshot key in keys)
      {
        this.AddKey(key, container);
      }
    }

    private void LoadTree()
    {
      this.BeginUpdate();

      this.Nodes.Clear();

      if (_snapshot != null)
      {
        this.AddKeys(_snapshot.Keys, this.Nodes);
      }

      this.EndUpdate();
    }

    #endregion
  }
}
