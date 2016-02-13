using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cyotek.RegistryComparer.Client
{
  internal class ChangeResultListView : ListView
  {
    #region Fields

    private List<ChangeResult> _results;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the Values property value changes
    /// </summary>
    [Category("Property Changed")]
    public event EventHandler ResultsChanged;

    #endregion

    #region Properties

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual List<ChangeResult> Results
    {
      get { return _results; }
      set
      {
        if (_results != value)
        {
          _results = value;

          this.OnResultsChanged(EventArgs.Empty);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Raises the <see cref="ResultsChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    protected virtual void OnResultsChanged(EventArgs e)
    {
      EventHandler handler;

      this.LoadList();

      handler = this.ResultsChanged;

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
        columns.Add("key", "Key", 200);
        columns.Add("name", "Name", 100);
        columns.Add("type", "Type", 100);
        columns.Add("data", "Old Data", 150);
        columns.Add("data", "New Data", 150);
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

      if (_results != null)
      {
        foreach (ChangeResult result in _results)
        {
          ListViewItem item;

          item = new ListViewItem
                 {
                   Text = result.KeyName,
                   ImageIndex = result.ValueName == null ? 0 : this.GetImageIndex(result.ValueType)
                 };

          item.SubItems.Add(result.ValueName == string.Empty ? "(Default)" : result.ValueName);
          item.SubItems.Add(result.ValueName == null ? string.Empty : this.GetTypeName(result.ValueType));
          item.SubItems.Add(result.OldValue);
          item.SubItems.Add(result.NewValue);

          switch (result.Type)
          {
            case ChangeType.Insertion:
            case ChangeType.Modification:
              item.ForeColor = Color.SeaGreen;
              break;
            case ChangeType.Deletion:
              item.ForeColor = Color.Firebrick;
              break;
          }

          items.Add(item);
        }
      }

      this.EndUpdate();
    }

    #endregion
  }
}
