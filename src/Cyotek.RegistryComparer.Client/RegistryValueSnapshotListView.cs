using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cyotek.RegistryComparer.Client
{
  internal class RegistryValueSnapshotListView : ListView
  {
    #region Fields

    private RegistryValueSnapshotCollection _values;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the Values property value changes
    /// </summary>
    [Category("Property Changed")]
    public event EventHandler ValuesChanged;

    #endregion

    #region Properties

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RegistryValueSnapshotCollection Values
    {
      get { return _values; }
      set
      {
        if (_values != value)
        {
          _values = value;

          this.OnValuesChanged(EventArgs.Empty);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Raises the <see cref="ValuesChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnValuesChanged(EventArgs e)
    {
      EventHandler handler;

      this.LoadList();

      handler = this.ValuesChanged;

      handler?.Invoke(this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);

      if (!this.DesignMode)
      {
        ColumnHeaderCollection columns;

        columns = this.Columns;

        columns.Clear();
        columns.Add("name", "Name", 140);
        columns.Add("type", "Type", 100);
        columns.Add("data", "Data", 300);
      }
    }

    private int GetImageIndex(RegistryValueKind type)
    {
      int result;

      switch (type)
      {
        case RegistryValueKind.String:
        case RegistryValueKind.ExpandString:
        case RegistryValueKind.MultiString:
          result = 2;
          break;
        default:
          result = 1;
          break;
      }

      return result;
    }

    private string GetTypeName(RegistryValueKind type)
    {
      string result;

      switch (type)
      {
        case RegistryValueKind.String:
          result = "REG_SZ";
          break;
        case RegistryValueKind.ExpandString:
          result = "REG_EXPAND_SZ";
          break;
        case RegistryValueKind.Binary:
          result = "REG_BINARY";
          break;
        case RegistryValueKind.DWord:
          result = "REG_DWORD";
          break;
        case RegistryValueKind.MultiString:
          result = "REG_MULTI_SZ";
          break;
        case RegistryValueKind.QWord:
          result = "REG_QWORD";
          break;
        default:
          result = "{Unknown}";
          break;
      }

      return result;
    }

    private void LoadList()
    {
      ListViewItemCollection items;

      this.BeginUpdate();

      items = this.Items;

      items.Clear();

      if (_values != null)
      {
        foreach (RegistryValueSnapshot value in _values)
        {
          ListViewItem item;

          item = new ListViewItem
                 {
                   Name = value.Name,
                   Text = string.IsNullOrEmpty(value.Name) ? "(Default)" : value.Name,
                   ImageIndex = this.GetImageIndex(value.Type)
                 };

          item.SubItems.Add(this.GetTypeName(value.Type));
          item.SubItems.Add(value.Value);

          items.Add(item);
        }
      }

      this.EndUpdate();
    }

    #endregion
  }
}
