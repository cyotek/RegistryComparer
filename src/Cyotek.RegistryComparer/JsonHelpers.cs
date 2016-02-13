using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Cyotek.RegistryComparer
{
  internal static class JsonHelpers
  {
    #region Static Methods

    public static T CreateFromJsonFile<T>(string fileName)
    {
      T data;

      using (Stream stream = File.OpenRead(fileName))
      {
        data = CreateFromJsonStream<T>(stream);
      }

      return data;
    }

    public static T CreateFromJsonStream<T>(Stream stream)
    {
      JsonSerializer serializer;
      T data;

      serializer = new JsonSerializer();

      using (StreamReader reader = new StreamReader(stream))
      {
        data = (T)serializer.Deserialize(reader, typeof(T));
      }

      return data;
    }

    public static T CreateFromJsonString<T>(string json)
    {
      T data;

      using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
      {
        data = CreateFromJsonStream<T>(stream);
      }

      return data;
    }

    public static void SaveToJsonFile<T>(string fileName, T value)
    {
      using (Stream stream = File.Create(fileName))
      {
        SaveToJsonStream(stream, value);
      }
    }

    public static void SaveToJsonStream<T>(Stream stream, T value)
    {
      JsonSerializer serializer;

      serializer = new JsonSerializer();

      using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
      {
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
#if DEBUG
          jsonWriter.Formatting = Formatting.Indented;
#endif

          serializer.Serialize(jsonWriter, value, typeof(T));
        }
      }
    }

    #endregion
  }
}
