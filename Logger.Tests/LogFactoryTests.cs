using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests
{
    [TestClass]
    public class LogFactoryTests
    {

        [TestMethod]
        public void CreateLogger_PathIsNull()
        {
            LogFactory logFactory = new LogFactory();
            logFactory.ConfigureFileLogger(null);
            BaseLogger logger = logFactory.CreateLogger("Is Null Test");

            Assert.IsNull(logger);
        }

        [TestMethod]
        public void CreateLogger_PathIsNotNull()
        {
            string tempPath = Path.GetTempFileName();
            LogFactory logFactory = new LogFactory();
            logFactory.ConfigureFileLogger(tempPath);
            BaseLogger logger = logFactory.CreateLogger("Is Not Null Test");

            Assert.IsNotNull(logger);
        }
    }
}
