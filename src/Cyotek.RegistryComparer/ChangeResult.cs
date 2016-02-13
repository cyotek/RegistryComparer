using System;
using Microsoft.Win32;

namespace Cyotek.RegistryComparer
{
  public class ChangeResult : IEquatable<ChangeResult>
  {
    #region Constructors

    public ChangeResult(ChangeType type, string keyName)
      : this(type, keyName, null)
    { }

    public ChangeResult(ChangeType type, string keyName, string valueName)
      : this(type, keyName, valueName, RegistryValueKind.Unknown, null, null)
    { }

    public ChangeResult(ChangeType type, string keyName, string valueName, RegistryValueKind valueType, string oldValue,
                        string newValue)
    {
      this.KeyName = keyName;
      this.ValueName = valueName;
      this.Type = type;
      this.OldValue = oldValue;
      this.NewValue = newValue;
      this.ValueType = valueType;
    }

    #endregion

    #region Properties

    public string KeyName { get; }

    public string NewValue { get; }

    public string OldValue { get; }

    public ChangeType Type { get; }

    public string ValueName { get; }

    public RegistryValueKind ValueType { get; }

    #endregion

    #region Methods

    public override bool Equals(object obj)
    {
      return obj != null && obj.GetType() == this.GetType() && this.Equals((ChangeResult)obj);
    }

    public override int GetHashCode()
    {
      // http://stackoverflow.com/a/263416/148962
      unchecked
      {
        int hash = 17;

        if (this.KeyName != null)
        {
          hash = hash * 486187739 + this.KeyName.GetHashCode();
        }

        if (this.ValueName != null)
        {
          hash = hash * 486187739 + this.ValueName.GetHashCode();
        }

        if (this.OldValue != null)
        {
          hash = hash * 486187739 + this.OldValue.GetHashCode();
        }

        if (this.NewValue != null)
        {
          hash = hash * 486187739 + this.NewValue.GetHashCode();
        }

        hash = hash * 486187739 + this.Type.GetHashCode();
        hash = hash * 486187739 + this.ValueType.GetHashCode();

        return hash;
      }
    }

    #endregion

    #region IEquatable<ChangeResult> Interface

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <returns>
    /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public bool Equals(ChangeResult other)
    {
      return other != null && this.Type == other.Type && this.ValueType == other.ValueType &&
             string.Equals(this.KeyName, other.KeyName, StringComparison.Ordinal) &&
             string.Equals(this.ValueName, other.ValueName, StringComparison.Ordinal) &&
             string.Equals(this.OldValue, other.OldValue, StringComparison.Ordinal) &&
             string.Equals(this.NewValue, other.NewValue, StringComparison.Ordinal);
    }

    #endregion
  }
}
