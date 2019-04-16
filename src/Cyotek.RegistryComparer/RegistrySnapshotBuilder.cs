using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Win32;

namespace Cyotek.RegistryComparer
{
  public class RegistrySnapshotBuilder
  {
    #region Constants

    private readonly byte[] _defaultBinaryValue = new byte[0];

    private const string _defaultStringValue = null;

    private const int _defaultWordValue = 0;

    private const RegistryKeyPermissionCheck _permissions = RegistryKeyPermissionCheck.Default;

    private const RegistryRights _rights = RegistryRights.EnumerateSubKeys | RegistryRights.QueryValues;

    private readonly string[] _emptyArray = new string[0];

    #endregion

    #region Methods

    public RegistrySnapshot TakeSnapshot(string[] keys)
    {
      RegistrySnapshot snapshot;

      snapshot = new RegistrySnapshot();

      foreach (string key in keys)
      {
        snapshot.Keys.Add(this.TakeSnapshot(key));
      }

      return snapshot;
    }

    public RegistryKeySnapshot TakeSnapshot(string key)
    {
      int endOfHivePosition;
      RegistryKey registryKey;

      endOfHivePosition = key.IndexOf('\\');

      if (endOfHivePosition == -1)
      {
        registryKey = this.GetHive(key);
      }
      else
      {
        string hive;

        hive = key.Substring(0, endOfHivePosition);
        registryKey = this.GetSubKey(this.GetHive(hive), key.Substring(endOfHivePosition + 1));
      }

      return this.TakeSnapshot(registryKey);
    }

    private string EncodeByteArray(byte[] value)
    {
      StringBuilder sb;

      sb = new StringBuilder();

      foreach (byte bte in value)
      {
        if (sb.Length != 0)
        {
          sb.Append(' ');
        }

        sb.Append(bte.ToString("x2"));
      }

      return sb.ToString();
    }

    private void FillKeys(RegistryKeySnapshot snapshot, RegistryKey key)
    {
      if (key.SubKeyCount != 0)
      {
        RegistryKeySnapshotCollection children;

        children = new RegistryKeySnapshotCollection(snapshot);

        // ReSharper disable once LoopCanBePartlyConvertedToQuery
        foreach (string name in key.GetSubKeyNames())
        {
          // HACK: Although I thought key names were unique, clearly I was wrong as the scan used to crash on
          // HKEY_LOCAL_MACHINE\SOFTWARE\Yamaha APO which appears at least twice on my system, although RegEdit
          // only shows a single copy

          if (!children.Contains(this.GetShortName(name)))
          {
            RegistryKey subKey;

            subKey = this.GetSubKey(key, name);

            if (subKey != null)
            {
              RegistryKeySnapshot childSnapshot;

              childSnapshot = this.TakeSnapshot(subKey);

              children.Add(childSnapshot);
            }
          }
        }

        snapshot.SubKeys = children;
      }
    }

    private void FillSnapshot(RegistryKeySnapshot snapshot, RegistryKey key)
    {
      this.FillKeys(snapshot, key);
      this.FillValues(snapshot, key);
    }

    private void FillValues(RegistryKeySnapshot snapshot, RegistryKey key)
    {
      if (key.ValueCount != 0)
      {
        RegistryValueSnapshotCollection children;
        string[] names;

        children = new RegistryValueSnapshotCollection(snapshot);

        try
        {
          names = key.GetValueNames();
        }
        catch (IOException)
        {
          // system error, saw this in latest Windows 10
          // when trying to scan HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager\CreativeEventCache\SubscribedContent-314559
          names = _emptyArray;
        }

        for (int i = 0; i < names.Length; i++)
        {
          string name;
          string value;
          RegistryValueKind type;
          object rawValue;

          name = names[i];
          type = key.GetValueKind(name);
          rawValue = this.GetValue(key, type, name);
          value = this.TransformRawValue(type, rawValue);

          children.Add(new RegistryValueSnapshot(name, value, type));
        }

        snapshot.Values = children;
      }
    }

    private object GetDefaultValue(RegistryValueKind type)
    {
      object defaultValue;

      switch (type)
      {
        case RegistryValueKind.String:
        case RegistryValueKind.ExpandString:
        case RegistryValueKind.MultiString:
          defaultValue = _defaultStringValue;
          break;
        case RegistryValueKind.DWord:
        case RegistryValueKind.QWord:
          // ReSharper disable once HeapView.BoxingAllocation
          defaultValue = _defaultWordValue;
          break;
        case RegistryValueKind.Binary:
        case RegistryValueKind.Unknown:
          defaultValue = _defaultBinaryValue;
          break;
        case RegistryValueKind.None:
          // GitHub Issue: #1
          // Not entirely sure what value I should use in the case of a None, went with a null
          defaultValue = null;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return defaultValue;
    }

    private RegistryKey GetHive(string hive)
    {
      RegistryKey rootHive;

      switch (hive)
      {
        case "HKEY_CLASSES_ROOT":
          rootHive = Registry.ClassesRoot;
          break;
        case "HKEY_CURRENT_USER":
          rootHive = Registry.CurrentUser;
          break;
        case "HKEY_LOCAL_MACHINE":
          rootHive = Registry.LocalMachine;
          break;
        case "HKEY_USERS":
          rootHive = Registry.Users;
          break;
        case "HKEY_CURRENT_CONFIG":
          rootHive = Registry.CurrentConfig;
          break;
        case "HKEY_PERFORMANCE_DATA":
          rootHive = Registry.PerformanceData;
          break;
        case "HKEY_DYN_DATA":
          rootHive = Registry.DynData;
          break;
        default:
          throw new ArgumentException("Invalid hive.", nameof(hive));
      }

      return rootHive;
    }

    private string GetShortName(string name)
    {
      int pathPosition;

      pathPosition = name.LastIndexOf('\\');

      if (pathPosition != -1)
      {
        name = name.Substring(pathPosition + 1);
      }

      return name;
    }

    private RegistryKey GetSubKey(RegistryKey key, string name)
    {
      RegistryKey subKey;

      try
      {
        subKey = key.OpenSubKey(name, _permissions, _rights);
      }
      catch (SecurityException ex)
      {
        Trace.WriteLine($"EXCEPTION: {ex.GetBaseException().Message}");
        subKey = null;
      }

      return subKey;
    }

    private object GetValue(RegistryKey key, RegistryValueKind type, string name)
    {
      object value;
      object defaultValue;

      defaultValue = this.GetDefaultValue(type);

      try
      {
        value = key.GetValue(name, defaultValue, RegistryValueOptions.DoNotExpandEnvironmentNames);
      }
      catch (SecurityException)
      {
        value = defaultValue;
      }
      catch (UnauthorizedAccessException)
      {
        value = defaultValue;
      }

      return value;
    }

    private RegistryKeySnapshot TakeSnapshot(RegistryKey key)
    {
      RegistryKeySnapshot snapshot;
      string name;

      Trace.WriteLine($"Scanning: {key.Name}");

      name = this.GetShortName(key.Name);

      snapshot = new RegistryKeySnapshot(name);

      this.FillSnapshot(snapshot, key);

      return snapshot;
    }

    private string TransformRawValue(RegistryValueKind type, object rawValue)
    {
      string value;

      switch (type)
      {
        case RegistryValueKind.String:
        case RegistryValueKind.ExpandString:
          value = (string)rawValue;
          break;
        case RegistryValueKind.MultiString:
          value = string.Join("\n", (string[])rawValue);
          break;
        case RegistryValueKind.DWord:
        case RegistryValueKind.QWord:
          value = Convert.ToInt64(rawValue).
                          ToString(CultureInfo.InvariantCulture);
          break;
        default:
          value = this.EncodeByteArray((byte[])rawValue);
          break;
      }
      return value;
    }

    #endregion
  }
}
