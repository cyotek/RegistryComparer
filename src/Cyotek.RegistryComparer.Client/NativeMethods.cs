using System;
using System.Runtime.InteropServices;

// http://www.cyotek.com/blog/enabling-shell-styles-for-the-listview-and-treeview-controls-in-csharp

namespace Cyotek.RegistryComparer.Client
{
  internal class NativeMethods
  {
    #region Externals

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    #endregion
  }
}
