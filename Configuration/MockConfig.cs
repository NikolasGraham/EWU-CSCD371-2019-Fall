using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    class MockConfig : IConfig
    {
        public List<string[]> ConfigSettings {get; private set;}

        public MockConfig()
        {
            string[] temp = { "00_CORE", "00_CORE" };
            ConfigSettings.Add(temp);
        }
        public bool GetConfigValue(string name, out string? value) //TODO
        {
            if (checkInput(name))
            {

            }
            value = "";
            return true;
        }

        public bool SetConfigValue(string name, string? value)
        {
            throw new NotImplementedException();
        }

        private bool checkInput(string toCheck)
        {
            if (toCheck is null || toCheck.Contains(" ") || toCheck.Length == 0 || toCheck.Contains("="))
            {
                return false;
            }
            return true;
        }
    }
}
