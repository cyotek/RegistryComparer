using System.Text;
using Newtonsoft.Json;

namespace Cyotek.RegistryComparer
{
  public class RegistryKeySnapshot
  {
    #region Constructors

    public RegistryKeySnapshot()
    { }

    public RegistryKeySnapshot(string name)
      : this()
    {
      this.Name = name;
    }

    #endregion

    #region Properties

    [JsonIgnore]
    public string FullPath
    {
      get
      {
        RegistryKeySnapshot parent;
        string result;
        string name;

        parent = this.Parent;
        name = this.Name;

        if (parent == null)
        {
          result = name;
        }
        else
        {
          StringBuilder sb;

          sb = new StringBuilder();

          sb.Append(name);

          do
          {
            if (sb.Length != 0)
            {
              sb.Insert(0, '\\');
            }

            sb.Insert(0, parent.Name);

            parent = parent.Parent;
          } while (parent != null);

          result = sb.ToString();
        }

        return result;
      }
    }

    public string Name { get; set; }

    [JsonIgnore]
    public RegistryKeySnapshot Parent { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public RegistryKeySnapshotCollection SubKeys { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public RegistryValueSnapshotCollection Values { get; set; }

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
