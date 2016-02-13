using System;
using System.Windows.Forms;

namespace Cyotek.RegistryComparer.Client
{
  static class Program
  {
    #region Static Methods

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    #endregion
  }
}
