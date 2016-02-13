using System;
using System.IO;

namespace Cyotek.RegistryComparer
{
  internal static class PathHelpers
  {
    #region Static Methods

    public static string GetFullPath(string relativePath)
    {
      return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, relativePath));
    }

    public static string GetUniqueFileName(string name, string path)
    {
      string fileName;

      fileName = Path.Combine(path, name + ".rge");

      if (File.Exists(fileName))
      {
        int counter;

        counter = 0;

        do
        {
          counter++;
          fileName = Path.Combine(path, $"{name} ({counter}).rge");
        } while (File.Exists(fileName));
      }

      return fileName;
    }

    #endregion
  }
}
