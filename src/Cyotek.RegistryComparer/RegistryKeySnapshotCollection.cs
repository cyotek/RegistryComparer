using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Cyotek.RegistryComparer
{
  public class RegistryKeySnapshotCollection : KeyedCollection<string, RegistryKeySnapshot>
  {
    #region Constructors

    public RegistryKeySnapshotCollection()
      : base(StringComparer.Ordinal)
    { }

    public RegistryKeySnapshotCollection(RegistryKeySnapshot parent)
      : this()
    {
      this.Parent = parent;
    }

    #endregion

    #region Properties

    [JsonIgnore]
    public RegistryKeySnapshot Parent { get; set; }

    #endregion

    #region Methods

    public bool TryGetValue(string key, out RegistryKeySnapshot value)
    {
      IDictionary<string, RegistryKeySnapshot> dictionary;

      dictionary = this.Dictionary;
      value = null;

      return dictionary != null && dictionary.TryGetValue(key, out value);
    }

    protected override string GetKeyForItem(RegistryKeySnapshot item)
    {
      return item.Name;
    }

    /// <summary>
    /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param><param name="item">The object to insert.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.</exception>
    protected override void InsertItem(int index, RegistryKeySnapshot item)
    {
      item.Parent = this.Parent;

      base.InsertItem(index, item);
    }

    #endregion
  }
}
