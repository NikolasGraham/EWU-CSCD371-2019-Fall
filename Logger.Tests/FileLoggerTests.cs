using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        [TestMethod]
        public void FileLogger_Log_CreatesFile()
        {
            string filePath = Path.GetTempFileName();
            FileLogger logger = new FileLogger(filePath);
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void FileLogger_Log_WritesToFile_SeparateLinesPerLog()
        {
            string filePath = Path.GetTempFileName();
            FileLogger logger = new FileLogger(filePath);
            logger.Log(LogLevel.Error, "Testing Error");
            logger.Log(LogLevel.Warning, "Testing Warning");

            int lineCount = File.ReadLines(filePath).Count();

            Assert.AreEqual(2, lineCount);
        }
    }
}
