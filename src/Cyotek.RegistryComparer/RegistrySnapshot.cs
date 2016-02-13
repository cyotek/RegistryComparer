using System;
using Newtonsoft.Json;

namespace Cyotek.RegistryComparer
{
  public class RegistrySnapshot
  {
    #region Constructors

    public RegistrySnapshot()
    {
      this.Timestamp = DateTime.UtcNow;
      this.Keys = new RegistryKeySnapshotCollection();
    }

    #endregion

    #region Static Methods

    public static RegistrySnapshot LoadFromFile(string fileName)
    {
      RegistrySnapshot snapshot;
      RegistryKeySnapshotCollection keys;

      snapshot = JsonHelpers.CreateFromJsonFile<RegistrySnapshot>(fileName);

      keys = snapshot.Keys;

      foreach (RegistryKeySnapshot key in keys)
      {
        snapshot.SetParents(key);
      }

      return snapshot;
    }

    public static RegistrySnapshot TakeSnapshot(string[] keys)
    {
      return new RegistrySnapshotBuilder().TakeSnapshot(keys);
    }

    #endregion

    #region Properties

    [JsonIgnore]
    public string FileName { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public RegistryKeySnapshotCollection Keys { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    public DateTime Timestamp { get; set; }

    #endregion

    #region Methods

    public void Load(string fileName)
    {
      RegistrySnapshot snapshot;

      snapshot = LoadFromFile(fileName);

      this.Name = snapshot.Name;
      this.Keys = snapshot.Keys;
      this.FileName = fileName;
    }

    public void Save(string fileName)
    {
      JsonHelpers.SaveToJsonFile(fileName, this);

      this.FileName = fileName;
    }

    private void SetParents(RegistryKeySnapshot key)
    {
      RegistryKeySnapshotCollection subKeys;
      RegistryValueSnapshotCollection values;

      values = key.Values;
      subKeys = key.SubKeys;

      if (values != null)
      {
        values.Parent = key;
      }

      if (subKeys != null)
      {
        subKeys.Parent = key;

        foreach (RegistryKeySnapshot child in subKeys)
        {
          child.Parent = key;

          this.SetParents(child);
        }
      }
    }

    #endregion
  }
}
