namespace Cyotek.RegistryComparer.CommandLineInterface
{
  internal enum ExitCode
  {
    Success,

    InvalidArguments,

    CompareMismatch,

    Exception = 255
  }
}
