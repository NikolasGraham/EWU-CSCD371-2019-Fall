using Configuration;
using System;
using System.Collections.Generic;

namespace SampleApp
{
    public class Program
    {
        public static void Main()
        {
            printSettings(new MockConfig());
        }
        public static void printSettings(IConfig config)
        {
            List<(string, string)> configSettings = new List<(string, string)> 
            {
                ("testName", ""),
                ("notATestName", ""),
                ("DOESNT_EXIST", "THIS_WONT_PRINT"),
                ("testingNames", ""),
                ("testerName", "")
            };

            for (int i= 0; i < configSettings.Count; i++)
            {
                config.GetConfigValue(configSettings[i].Item1, out string? value);
                configSettings[i] = (configSettings[i].Item1, value);
                Console.WriteLine($"{configSettings[i].Item1} : {configSettings[i].Item2}");
            }
        }

    }
}
