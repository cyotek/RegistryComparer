using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyotek.RegistryComparer.Client
{
  internal partial class ViewCompareResultsDialog : BaseForm
  {
    #region Constants

    private readonly List<ChangeResult> _results;

    #endregion

    #region Constructors

    public ViewCompareResultsDialog()
    {
      this.InitializeComponent();
    }

    public ViewCompareResultsDialog(ChangeResult[] results)
      : this()
    {
      _results = results.ToList();
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

      if (_results != null)
      {
        listView.Results = _results;
      }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    #endregion
  }
}
