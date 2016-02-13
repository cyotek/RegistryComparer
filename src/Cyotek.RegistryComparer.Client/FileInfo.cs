using System.IO;

namespace Cyotek.RegistryComparer.Client
{
  internal sealed class FileInfo
  {
    #region Constructors

    public FileInfo(string fullPath)
    {
      this.FullPath = fullPath;
    }

    #endregion

    #region Properties

    public string FullPath { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>
    /// A string that represents the current object.
    /// </returns>
    public override string ToString()
    {
      return Path.GetFileName(this.FullPath) ?? base.ToString();
    }

    #endregion
  }
}
