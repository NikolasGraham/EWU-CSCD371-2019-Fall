using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        private string filePath;
        public FileLogger(string filePath)
        {
            this.filePath = filePath;

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
        }
        public override void Log(LogLevel logLevel, string message)
        {
           DateTime date = DateTime.Now;
            String level = logLevel.ToString();

            string loggedString = $"{date} {className} {level} {message}";

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(loggedString);
            }
        }
    }
}
