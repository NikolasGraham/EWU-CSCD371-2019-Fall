using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    public class MockConfig : IConfig
    {
        public List<(string, string)> ConfigSettings {get; private set;}

        public MockConfig()
        {
            ConfigSettings = new List<(string, string)>();
            ConfigSettings.Add(("testName","testValue"));
            ConfigSettings.Add(("notATestName", "notATestValue"));
            ConfigSettings.Add(("testingNames", "testingValues"));
            ConfigSettings.Add(("testerName", "testerValue"));
        }
        public bool GetConfigValue(string name, out string? value) //TODO
        {
            if (checkInput(name))
            {
                foreach ((string,string) listItem in ConfigSettings)
                {
                    if (listItem.Item1 == name)
                    {
                        value = listItem.Item2;
                        return true;
                    }
                }
            }
            value = "";
            return false;
        }

        public bool SetConfigValue(string name, string? value)
        {
            if (checkInput(name) && checkInput(value))
            {
                for (int i=0; i < ConfigSettings.Count; i++)
                {
                    if (ConfigSettings[i].Item1 == name)
                    {
                        ConfigSettings[i] = (name,value);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkInput(string toCheck)
        {
            if(toCheck is null) { return false; }
            if (toCheck.Contains(" ") || toCheck.Length == 0 || toCheck.Contains("="))
            {
                return false;
            }
            return true;
        }
    }
}
