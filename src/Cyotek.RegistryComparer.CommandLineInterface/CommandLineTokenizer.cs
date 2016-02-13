using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cyotek.RegistryComparer.CommandLineInterface
{
  internal sealed class CommandLineTokenizer
  {
    #region Constants

    private readonly List<string> _files = new List<string>();

    private const string _splitTokenPattern = @"^([/-]|--){1}(?<name>(\w+|\?))([:=])?(?<value>.+)?$";

    private static readonly char[] _tokenWhitespace =
    {
      ' ',
      '"',
      '\''
    };

    #endregion

    #region Constructors

    public CommandLineTokenizer(IEnumerable<string> arguments)
    {
      this.LoadArguments(arguments);
    }

    #endregion

    #region Properties

    public int Count
    {
      get { return this.Parameters.Count; }
    }

    public IList<string> Files
    {
      get { return _files; }
    }

    public string this[string token]
    {
      get { return this.Parameters[token]; }
    }

    public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

    #endregion

    #region Methods

    public bool ContainsKey(string name)
    {
      return this.Parameters.ContainsKey(name);
    }

    public bool ContainsKeys(params string[] keys)
    {
      return keys.Any(this.Parameters.ContainsKey);
    }

    public bool GetBoolean(string name)
    {
      return this.GetBoolean(name, false);
    }

    public bool GetBoolean(string name, bool defaultValue)
    {
      return this.GetValue(name, defaultValue, this.GetBooleanValue);
    }

    public double GetDouble(string name)
    {
      return this.GetDouble(name, 0);
    }

    public double GetDouble(string name, double defaultValue)
    {
      return this.GetValue(name, defaultValue, Convert.ToDouble);
    }

    public T GetEnum<T>(string name) where T : struct
    {
      return this.GetEnum(name, default(T));
    }

    public T GetEnum<T>(string name, T defaultValue) where T : struct
    {
      if (!typeof(T).IsEnum)
      {
        throw new ArgumentException("Type is not an enum", nameof(defaultValue));
      }

      return this.GetValue(name, defaultValue, value => (T)Enum.Parse(typeof(T), value, true));
    }

    public int GetInteger(string name)
    {
      return this.GetInteger(name, 0);
    }

    public int GetInteger(string name, int defaultValue)
    {
      return this.GetValue(name, defaultValue, Convert.ToInt32);
    }

    public string GetString(string name)
    {
      return this.GetString(name, string.Empty);
    }

    public string GetString(string name, string defaultValue)
    {
      return this.GetValue(name, defaultValue, value => value.ToString(CultureInfo.InvariantCulture));
    }

    public string[] GetStringList(string name)
    {
      string value;

      value = this.GetString(name, null);

      return !string.IsNullOrEmpty(value) ? value.Split(',') : new string[0];
    }

    public T GetValue<T>(string name, T defaultValue, Func<string, T> conversion)
    {
      string rawValue;
      T value;

      if (conversion == null)
      {
        throw new ArgumentNullException(nameof(conversion));
      }

      value = this.Parameters.TryGetValue(name, out rawValue) ? conversion(rawValue) : defaultValue;

      return value;
    }

    public bool HasValue(string key)
    {
      string value;

      return this.Parameters.TryGetValue(key, out value) && !string.IsNullOrEmpty(value);
    }

    public bool IsFlagSet(string name)
    {
      return this.GetBoolean(name, false);
    }

    public void LoadArguments(IEnumerable<string> arguments)
    {
      Regex tokenizer;
      string currentToken;

      tokenizer = new Regex(_splitTokenPattern);
      currentToken = null;

      // ReSharper disable once LoopCanBePartlyConvertedToQuery
      foreach (string argument in arguments)
      {
        if (!string.IsNullOrEmpty(argument) && argument.Trim()[0] != ';')
        {
          Match match;

          match = tokenizer.Match(argument);

          if (!match.Success)
          {
            if (currentToken != null)
            {
              this.SetToken(currentToken, argument);
            }
            else
            {
              this.Files.Add(argument);
            }
          }
          else
          {
            string tokenValue;

            currentToken = match.Groups["name"].Value;
            tokenValue = match.Groups["value"].Value;

            this.SetToken(currentToken, tokenValue);
          }
        }
      }
    }

    internal bool GetBooleanValue(string value)
    {
      int integerValue;

      return !string.IsNullOrEmpty(value) &&
             (value.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
              int.TryParse(value, out integerValue) && integerValue != 0);
    }

    private void SetToken(string name, string value)
    {
      value = value.Trim(_tokenWhitespace);

      if (value.Length == 0)
      {
        if (!this.Parameters.ContainsKey(name))
        {
          this.Parameters[name] = "true";
        }
      }
      else
      {
        string currentValue;

        if (this.Parameters.TryGetValue(name, out currentValue) && currentValue != "true")
        {
          value = string.Concat(currentValue, ",", value);
        }

        this.Parameters[name] = value;
      }
    }

    #endregion
  }
}
