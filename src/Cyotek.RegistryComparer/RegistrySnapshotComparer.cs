using System;
using System.Collections.Generic;

namespace Cyotek.RegistryComparer
{
  public class RegistrySnapshotComparer
  {
    #region Constants

    private readonly RegistrySnapshot _lhs;

    private readonly RegistrySnapshot _rhs;

    #endregion

    #region Constructors

    public RegistrySnapshotComparer(RegistrySnapshot lhs, RegistrySnapshot rhs)
    {
      _lhs = lhs;
      _rhs = rhs;
    }

    #endregion

    #region Methods

    public ChangeResult[] Compare()
    {
      List<ChangeResult> results;

      results = new List<ChangeResult>();

      this.CompareKeys(_lhs.Keys, _rhs.Keys, results);

      return results.ToArray();
    }

    private void CompareKeys(RegistryKeySnapshotCollection lhs, RegistryKeySnapshotCollection rhs,
                             ICollection<ChangeResult> results)
    {
      if (lhs != null && rhs != null)
      {
        this.CompareKeys(lhs, rhs, ChangeType.Insertion, results);

        this.CompareKeys(rhs, lhs, ChangeType.Deletion, results);
      }
    }

    private void CompareKeys(RegistryKeySnapshotCollection lhs, RegistryKeySnapshotCollection rhs, ChangeType type,
                             ICollection<ChangeResult> results)
    {
      foreach (RegistryKeySnapshot key in lhs)
      {
        string name;
        RegistryKeySnapshot compare;

        name = key.Name;

        if (rhs == null || !rhs.TryGetValue(name, out compare))
        {
          results.Add(new ChangeResult(type, key.FullPath));
        }
        else if (type == ChangeType.Insertion)
        {
          this.CompareKeys(key, compare, results);
        }
      }
    }

    private void CompareKeys(RegistryKeySnapshot lhs, RegistryKeySnapshot rhs, ICollection<ChangeResult> results)
    {
      this.CompareValues(lhs.Values, rhs.Values, results);
      this.CompareKeys(lhs.SubKeys, rhs.SubKeys, results);
    }

    private void CompareValues(RegistryValueSnapshotCollection lhs, RegistryValueSnapshotCollection rhs,
                               ICollection<ChangeResult> results)
    {
      if (lhs != null && rhs != null)
      {
        this.CompareValues(lhs, rhs, ChangeType.Insertion, results);

        this.CompareValues(rhs, lhs, ChangeType.Deletion, results);
      }
    }

    private void CompareValues(RegistryValueSnapshotCollection lhs, RegistryValueSnapshotCollection rhs, ChangeType type,
                               ICollection<ChangeResult> results)
    {
      foreach (RegistryValueSnapshot value in lhs)
      {
        string name;
        RegistryValueSnapshot compare;

        name = value.Name;

        if (rhs == null || !rhs.TryGetValue(name, out compare))
        {
          results.Add(new ChangeResult(type, lhs.Parent.FullPath, value.Name, value.Type, value.Value, null));
        }
        else if (type == ChangeType.Insertion)
        {
          if (!string.Equals(value.Value, compare.Value, StringComparison.Ordinal) || value.Type != compare.Type)
          {
            results.Add(new ChangeResult(ChangeType.Modification, lhs.Parent.FullPath, value.Name, value.Type,
                                         compare.Value, value.Value));
          }
        }
      }
    }

    #endregion
  }
}
