using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration.Tests
{
    [TestClass]
    public class MockConfigTests
    {
        private IConfig setup()
        {
            return new MockConfig();
        }

        [TestMethod]
        [DataRow(null)]
        public void MockConfig_GetConfigValue_NameIsNull_ReturnsFalse(string name)
        {
            IConfig config = setup();
            bool worked = config.GetConfigValue(name, out string? value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow(" ")]
        [DataRow("test Name")]
        public void MockConfig_GetConfigValue_NameIsContainsSpaces_ReturnsFalse(string name)
        {
            IConfig config = setup();
            bool worked = config.GetConfigValue(name, out string? value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("")]
        public void MockConfig_GetConfigValue_NameIsEmptyString_ReturnsFalse(string name)
        {
            IConfig config = setup();
            bool worked = config.GetConfigValue(name, out string? value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("=")]
        [DataRow("test=Name")]
        [DataRow("te=s=t=Nam=e")]
        public void MockConfig_GetConfigValue_NameContainsEqualsSign_ReturnsFalse(string name)
        {
            IConfig config = setup();
            bool worked = config.GetConfigValue(name, out string? value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName2")]
        public void MockConfig_GetConfigValue_ValueNotFound_ReturnsFalse(string name)
        {
            IConfig config = setup();
            bool worked = config.GetConfigValue(name, out string? value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName", "testValue")]
        [DataRow("notATestName", "notATestValue")]
        [DataRow("testingNames", "testingValues")]
        [DataRow("testerName", "testerValue")]
        public void MockConfig_GetConfigValue_CorrectInput_ReturnsFalse(string name, string expectedOutput)
        {
            IConfig config = setup();
            bool worked = config.GetConfigValue(name, out string? value);

            Assert.AreEqual(expectedOutput, value);
        }

        [TestMethod]
        [DataRow(null,"testValue")]
        public void MockConfig_SetConfigValue_NameIsNull_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }
        [TestMethod]
        [DataRow("testName", null)]
        public void MockConfig_SetConfigValue_ValueIsNull_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("", "testValue")]
        public void MockConfig_SetConfigValue_NameIsEmptyString_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName", "")]
        public void MockConfig_SetConfigValue_ValueIsEmptyString_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("test Name", "testValue2")]
        public void MockConfig_SetConfigValue_NameContainsSpaces_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName", "test Valu e2")]
        public void MockConfig_SetConfigValue_ValueContainsSpaces_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("test=Na=m=e", "testValue2")]
        public void MockConfig_SetConfigValue_NameContainsEqualsSign_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName", "te=s=tValu=e=2")]
        public void MockConfig_SetConfigValue_ValueContainsEqualsSign_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName2", "testValue2")]
        public void MockConfig_SetConfigValue_ValueNotFound_ReturnsFalse(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsFalse(worked);
        }

        [TestMethod]
        [DataRow("testName", "testValue2")]
        [DataRow("notATestName", "notATestValue2")]
        [DataRow("testingNames", "testingValues2")]
        [DataRow("testerName", "testerValue2")]
        public void MockConfig_SetConfigValue_CorrectInput_ReturnsTrue(string name, string value)
        {
            IConfig config = setup();
            bool worked = config.SetConfigValue(name, value);

            Assert.IsTrue(worked);
        }

        [TestMethod]
        [DataRow("testName", "testValue2")]
        [DataRow("notATestName", "notATestValue2")]
        [DataRow("testingNames", "testingValues2")]
        [DataRow("testerName", "testerValue2")]
        public void MockConfig_SetConfigValue_CorrectInputCorrectlyWritten_ReturnsTrue(string name, string value)
        {
            IConfig config = setup();
            config.SetConfigValue(name, value);
            config.GetConfigValue(name, out string? value2);

            Assert.AreEqual(value, value2);
        }
    }
}
