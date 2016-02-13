using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using NUnit.Framework;

namespace Cyotek.RegistryComparer.Tests
{
  [TestFixture]
  public class TestBase
  {
    #region Test Helpers

    protected void CreateBase()
    {
      Registry.CurrentUser.CreateSubKey(this.RootKeyName);

      this.CreateSubKey1();
      this.CreateSubKey2();
    }

    protected void DeleteBase()
    {
      RegistryKey key;

      key = Registry.CurrentUser.OpenSubKey(this.RootKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);

      if (key != null)
      {
        Registry.CurrentUser.DeleteSubKeyTree(this.RootKeyName);
      }
    }

    protected RegistrySnapshot LoadBaseSnapshot()
    {
      return this.LoadSnapshot("test_base");
    }

    protected RegistrySnapshot LoadChangedValueSnapshot()
    {
      return this.LoadSnapshot("test_modvalue");
    }

    protected RegistrySnapshot LoadDeletedKeySnapshot()
    {
      return this.LoadSnapshot("test_delsubkey");
    }

    protected RegistrySnapshot LoadDeletedValueSnapshot()
    {
      return this.LoadSnapshot("test_delvalue");
    }

    protected RegistrySnapshot LoadInsertedKeySnapshot()
    {
      return this.LoadSnapshot("test_newsubkey");
    }

    protected RegistrySnapshot LoadInsertedValueSnapshot()
    {
      return this.LoadSnapshot("test_newvalue");
    }

    protected RegistrySnapshot LoadSnapshot(string name)
    {
      return RegistrySnapshot.LoadFromFile(Path.Combine(this.DataPath, name + ".rge"));
    }

    private void CreateSubKey1()
    {
      RegistryKey key;
      RegistryKey subKey;

      key = Registry.CurrentUser.CreateSubKey(this.RootKeyName);

      subKey = key.CreateSubKey("Alpha");

      subKey.SetValue("string", "alpha", RegistryValueKind.String);
      subKey.SetValue("multi_string", new[]
                                      {
                                        "beta",
                                        "gamma"
                                      }, RegistryValueKind.MultiString);
      subKey.SetValue("expanded_string", "%systemroot%\\delta", RegistryValueKind.ExpandString);
    }

    private void CreateSubKey2()
    {
      RegistryKey key;
      RegistryKey subKey;

      key = Registry.CurrentUser.CreateSubKey(this.RootKeyName);

      subKey = key.CreateSubKey("Beta");

      subKey.SetValue("dword", int.MaxValue, RegistryValueKind.DWord);
      subKey.SetValue("qword", long.MaxValue, RegistryValueKind.QWord);
      subKey.SetValue("binary", Encoding.ASCII.GetBytes("EPSILON"), RegistryValueKind.Binary);
    }

    private void CreateSubKey3()
    {
      RegistryKey key;
      RegistryKey subKey;

      key = Registry.CurrentUser.CreateSubKey(this.RootKeyName);

      subKey = key.CreateSubKey("Gamma");

      subKey.SetValue(null, "zeta");
    }

    protected string DataPath
    {
      get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data"); }
    }

    protected string FullRootKeyName
    {
      get { return @"HKEY_CURRENT_USER\SOFTWARE\Cyotek\Registry Comparer\Tests"; }
    }

    protected string RootKeyName
    {
      get { return @"SOFTWARE\Cyotek\Registry Comparer\Tests"; }
    }

    #endregion
  }
}
