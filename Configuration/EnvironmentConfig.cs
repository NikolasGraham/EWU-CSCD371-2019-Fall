using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    public class EnvironmentConfig : IConfig
    {
        public bool GetConfigValue(string name, string? value)
        {
            if(name is null) { throw new ArgumentNullException(nameof(name)); }
            if (name == " " || name.Length == 0) { throw new ArgumentException("No name given!"); }

            try
            {
                value = Environment.GetEnvironmentVariable(name);
                return true;
            }
            catch(ArgumentNullException)
            {
                return false;
            }
        }

        public bool SetConfigValue(string name, string? value)
        {
            if(name is null) { throw new ArgumentNullException(nameof(name)); }
            if(value is null) { throw new ArgumentNullException(nameof(value)); }

            if(name.Contains(" ") || name.Length == 0) { throw new ArgumentException("No name given!"); }
            if (name.Contains("=")) { throw new ArgumentException("Cannot have name wih equals!"); }
            if (value.Contains(" ") || value.Length == 0) { throw new ArgumentException("No value given!"); }
            if (value.Contains("=")) { throw new ArgumentException("Cannot have value wih equals!"); }

            try
            {
                Environment.SetEnvironmentVariable(name, value);
                return true;
            }
            catch(ArgumentException)
            {
                return false;
            }
        }
    }
}
