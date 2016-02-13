#define CATCHEXCEPTIONS
#if DEBUG
#undef CATCHEXCEPTIONS
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Cyotek.RegistryComparer.CommandLineInterface
{
  internal sealed class Program
  {
    #region Constants

    private readonly CommandLineTokenizer _commandLine;

    #endregion

    #region Fields

    private bool _addTimestamp;

    private RegistrySnapshot _currentSnapshot;

    private string[] _files;

    private string[] _keys;

    private bool _noLogo;

    private string _snapshotName;

    #endregion

    #region Constructors

    internal Program(IEnumerable<string> args)
    {
      _commandLine = new CommandLineTokenizer(args);
    }

    #endregion

    #region Static Methods

    private static int Main(string[] args)
    {
      ExitCode exitCode;

      exitCode = new Program(args).Run();

      if (Debugger.IsAttached)
      {
        Console.WriteLine("(Press any key to exit)");
        Console.ReadKey(true);
      }

      return (int)exitCode;
    }

    #endregion

    #region Properties

    public ExitCode ExitCode { get; set; }

    #endregion

    #region Methods

    internal ExitCode Run()
    {
      ProgramAction action;

      this.ExitCode = ExitCode.InvalidArguments;

      this.LoadArguments();

      if (!_noLogo)
      {
        this.WriteHeader();
      }

      action = this.DefineAction();

      if (action == ProgramAction.None)
      {
        Console.WriteLine("At least one key to snapsnot or two files to compare must be specified.");
      }
      else
      {
#if CATCHEXCEPTIONS
        try
#endif
        {
          switch (action)
          {
            case ProgramAction.Snapshot:
              this.TakeSnapshot();
              break;
            case ProgramAction.Compare:
              this.CompareSnapshots();
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
#if !CATCHEXCEPTIONS
        try
        { }
#endif
        catch (Exception ex)
        {
          this.ExitCode = ExitCode.Exception;

          Console.Error.Write(ex.ToString());
        }
      }

      return this.ExitCode;
    }

    private void CompareSnapshots()
    {
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;
      ChangeResult[] results;

      lhs = null;
      rhs = null;
      results = new ChangeResult[0];

      this.PerformAction(() => lhs = RegistrySnapshot.LoadFromFile(PathHelpers.GetFullPath(_files[0])),
                         "Loading first snapshot");
      this.PerformAction(() => rhs = RegistrySnapshot.LoadFromFile(PathHelpers.GetFullPath(_files[1])),
                         "Loading second snapshot");

      if (lhs != null && rhs != null)
      {
        this.PerformAction(() =>
                           {
                             RegistrySnapshotComparer comparer;

                             comparer = new RegistrySnapshotComparer(lhs, rhs);

                             results = comparer.Compare();
                           }, "Comparing snapshots");
      }

      this.PrintResults(results);

      if (this.ExitCode == ExitCode.InvalidArguments)
      {
        this.ExitCode = results.Length == 0 ? ExitCode.Success : ExitCode.CompareMismatch;
      }
    }

    private ProgramAction DefineAction()
    {
      ProgramAction result;

      if (_keys.Length != 0)
      {
        result = ProgramAction.Snapshot;
      }
      else if (_files.Length == 2)
      {
        result = ProgramAction.Compare;
      }
      else
      {
        result = ProgramAction.None;
      }

      return result;
    }

    private string GetSnapshotName()
    {
      string name;

      name = _snapshotName;

      if (string.IsNullOrEmpty(name))
      {
        name = DateTime.Now.ToString("yyyyMMdd HHmm");
      }
      else if (_addTimestamp)
      {
        name += DateTime.Now.ToString(" yyyyMMdd HHmm");
      }

      return name;
    }

    private void LoadArguments()
    {
      _noLogo = _commandLine.GetBoolean("nologo");
      _keys = _commandLine.GetStringList("key");
      _snapshotName = _commandLine.GetString("name");
      _addTimestamp = _commandLine.GetBoolean("addtimestamp");
      _files = _commandLine.Files.ToArray();
    }

    private void PerformAction(Action action, string message)
    {
      Console.Write(message);
      Console.Write("... ");

#if CATCHEXCEPTIONS
      try
#endif
      {
        action();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Done");
      }
#if !CATCHEXCEPTIONS
      try
      { }
#endif
      catch (Exception ex)
      {
        this.ExitCode = ExitCode.Exception;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Failed");
        Console.WriteLine(ex.ToString());
      }
      finally
      {
        Console.ResetColor();
      }
    }

    private void PrintResults(ChangeResult[] results)
    {
      if (results.Length == 0)
      {
        Console.WriteLine("\nNo changes detected.");
      }
      else
      {
        Console.WriteLine();

        foreach (ChangeResult result in results)
        {
          switch (result.Type)
          {
            case ChangeType.Insertion:
              Console.ForegroundColor = ConsoleColor.Green;
              Console.Write("NEW");
              break;
            case ChangeType.Deletion:
              Console.ForegroundColor = ConsoleColor.Red;
              Console.Write("DEL");
              break;
            case ChangeType.Modification:
              Console.ForegroundColor = ConsoleColor.Green;
              Console.Write("UPD");
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }

          Console.ForegroundColor = ConsoleColor.Gray;
          Console.Write('\t');
          Console.Write(result.KeyName);

          if (result.ValueName != null)
          {
            Console.Write('@');
            Console.Write(result.ValueName);
          }

          Console.WriteLine();
        }
      }
    }

    private void SaveSnapshot()
    {
      this.PerformAction(() =>
                         {
                           string name;
                           string fileName;

                           name = this.GetSnapshotName();
                           fileName = PathHelpers.GetFullPath(name + ".rge");

                           _currentSnapshot.Save(fileName);
                         }, "Saving snapshot");
    }

    private void TakeSnapshot()
    {
      RegistrySnapshot snapshot;
      RegistrySnapshotBuilder builder;

      builder = new RegistrySnapshotBuilder();
      snapshot = new RegistrySnapshot();

      foreach (string key in _keys)
      {
        this.PerformAction(() =>
                           {
                             RegistryKeySnapshot keySnapshot;

                             keySnapshot = builder.TakeSnapshot(key);
                             keySnapshot.Name = key;

                             snapshot.Keys.Add(keySnapshot);
                           }, $"Snapshotting {key}");
      }

      _currentSnapshot = snapshot;

      this.SaveSnapshot();

      if (this.ExitCode == ExitCode.InvalidArguments)
      {
        this.ExitCode = ExitCode.Success;
      }
    }

    private void WriteHeader()
    {
      Assembly assembly;
      FileVersionInfo fileVersionInfo;

      assembly = Assembly.GetEntryAssembly();
      fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine(fileVersionInfo.FileDescription);
      Console.WriteLine(string.Concat("Version ", fileVersionInfo.ProductMajorPart.ToString(), ".",
                                      fileVersionInfo.ProductMinorPart.ToString()));
      Console.ResetColor();
      Console.WriteLine(fileVersionInfo.LegalCopyright);
      Console.WriteLine();
    }

    #endregion
  }
}
