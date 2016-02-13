using Microsoft.Win32;

namespace Cyotek.RegistryComparer
{
  public class RegistryValueSnapshot
  {
    #region Constructors

    public RegistryValueSnapshot(string name, string value, RegistryValueKind type)
      : this()
    {
      this.Name = name;
      this.Value = value;
      this.Type = type;
    }

    public RegistryValueSnapshot()
    { }

    #endregion

    #region Properties

    public string Name { get; set; }

    public RegistryValueKind Type { get; set; }

    public string Value { get; set; }

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
      return this.Name;
    }

    #endregion
  }
}
