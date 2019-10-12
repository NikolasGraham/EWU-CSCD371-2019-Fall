using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        public override void Log(LogLevel logLevel, string message)
        {
            Console.WriteLine(message);
        }
    }
}
