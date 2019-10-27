using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Configuration
{
    public class FileConfig : IConfig
    {
        private string FileName { get; }

        public FileConfig(string fileName)
        {
            FileName = fileName;
        }
        public bool GetConfigValue(string name, out string? value)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }
            if (name.Contains(" ") || name.Length == 0) { throw new ArgumentException("No name given!"); }
            if (name.Contains("=")) { throw new ArgumentException("Cannot have name wih equals!"); }

            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException(nameof(FileName) + " Does not exist!");
            }

            foreach(string line in File.ReadAllLines(FileName))
            {
                string[] nameValuePair = line.Split("=");

                if (nameValuePair[0].Equals(name))
                {
                    value = nameValuePair[1];
                    return true;
                }
            }
            value = null;
            return false;
        }

        public bool SetConfigValue(string name, string? value)
        {
            if (name is null) { throw new ArgumentNullException(nameof(name)); }
            if (name.Contains(" ") || name.Length == 0) { throw new ArgumentException("No name given!"); }
            if (name.Contains("=")) { throw new ArgumentException("Cannot have name wih equals!"); }

            if (value is null) { throw new ArgumentNullException(nameof(value)); }
            if (value.Contains(" ") || value.Length == 0) { throw new ArgumentException("No value given!"); }
            if (value.Contains("=")) { throw new ArgumentException("Cannot have value wih equals!"); }

            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException(nameof(FileName) + " Does not exist!");
            }

            foreach (string line in File.ReadAllLines(FileName))
            {
                string[] nameValuePair = line.Split("=");

                if (nameValuePair[0].Equals(name))
                {
                    string textToWrite = File.ReadAllText(FileName)
                        .Replace($"{name}={nameValuePair[1]}", $"{name}={value}");
                    File.WriteAllText(FileName, textToWrite);
                    return true;
                }
            }

            File.AppendAllText(FileName, $"{name}={value}{Environment.NewLine}");
            return true;
        }
    }
}
