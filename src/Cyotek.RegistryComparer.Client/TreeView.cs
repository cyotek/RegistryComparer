using System;
using System.ComponentModel;

// http://www.cyotek.com/blog/enabling-shell-styles-for-the-listview-and-treeview-controls-in-csharp

namespace Cyotek.RegistryComparer.Client
{
  internal class TreeView : System.Windows.Forms.TreeView
  {
    #region Constructors

    public TreeView()
    {
      base.ShowLines = false;
    }

    #endregion

    #region Properties

    [DefaultValue(false)]
    public new bool ShowLines
    {
      get { return base.ShowLines; }
      set { base.ShowLines = value; }
    }

    #endregion

    #region Methods

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);

      if (!this.DesignMode && Environment.OSVersion.Platform == PlatformID.Win32NT &&
          Environment.OSVersion.Version.Major >= 6)
      {
        NativeMethods.SetWindowTheme(this.Handle, "explorer", null);
      }
    }

    #endregion
  }
}
