using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cyotek.RegistryComparer.Client
{
  internal class BaseForm : Form
  {
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      this.Font = SystemFonts.MessageBoxFont;
    }
  }
}
