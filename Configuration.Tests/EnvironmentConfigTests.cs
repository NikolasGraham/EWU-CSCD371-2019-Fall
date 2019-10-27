using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration.Tests
{
    [TestClass]
    public class EnvironmentConfigTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentConfig_SetEnvironmentVariable_NullVariables_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue(null, null);

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentConfig_SetEnvironmentVariable_NullName_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue(null, "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentConfig_SetEnvironmentVariable_NullValue_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("testName", null);

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_SetEnvironmentVariable_NameWithSpaces_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue(" ", "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_SetEnvironmentVariable_ValueWithSpaces_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("testName", " ");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_SetEnvironmentVariable_NameEmptyString_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("", "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_SetEnvironmentVariable_ValueEmptyString_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("testName", "");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_SetEnvironmentVariable_NameWithEqualsSign_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("99=99=", "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_SetEnvironmentVariable_ValueWithEqualsSign_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("testName", "83=234=234=");

            Assert.IsFalse(works);
        }

        [TestMethod]
        public void EnvironmentConfig_SetEnvironmentVariable_NonNullVariables_ReturnsTrue()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("testName", "testValue");

            Assert.IsTrue(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnvironmentConfig_GetEnvironmentVariable_NameIsNull_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue(null, "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_GetEnvironmentVariable_NameWithEqualsSign_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("test=Nam=e", "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_GetEnvironmentVariable_NameWithEmptyString_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue(" ", "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EnvironmentConfig_GetEnvironmentVariable_NameWithLengthZero_ReturnsFalse()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("", "testValue");

            Assert.IsFalse(works);
        }

        [TestMethod]
        public void EnvironmentConfig_GetEnvironmentVariable_GoodName_ReturnsTrue()
        {
            IConfig testConfig = new EnvironmentConfig();

            bool works = testConfig.SetConfigValue("testName", "testValue");

            Assert.IsTrue(works);
        }
    }
}
