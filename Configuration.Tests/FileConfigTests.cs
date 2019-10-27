using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Configuration.Tests
{
    [TestClass]
    public class FileConfigTests
    {
        private string FileName {get; set;}
        private IConfig testConfig;

        private void setup()
        {
            FileName = "fileNameTest.txt";
            testConfig = new FileConfig(FileName);
        }

        private void cleanup()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            FileName = "";
        }

        private void createFile()
        {
            FileName = "fileNameTest.txt";
            var filePath = File.Create(FileName);
            StreamWriter fileWriter = new StreamWriter(filePath);
            string[] testText = {
                "testName=testValue",
                "notATestName=notATestValue"
            };
            if (File.Exists(FileName))
            {
                foreach (string line in testText)
                { 
                    fileWriter.WriteLine(line);
                }
            }
            fileWriter.Close();
        }


        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileConfig_GetConfigValue_FileDoesNotExist_ThrowsException()
        {
            setup();
            
            testConfig.GetConfigValue("testName", out string? value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileConfig_GetConfigValue_NameIsNull_ThrowsException()
        {
            setup();
            testConfig.GetConfigValue(null, out string? value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_GetConfigValue_NameIsEmptyString_ThrowsException()
        {
            setup();
            testConfig.GetConfigValue("", out string? value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_GetConfigValue_NameContainsSpaces_ThrowsException()
        {
            setup();
            testConfig.GetConfigValue("test Name ", out string? value);
        }

        [TestMethod]
        [DataRow("testName", "testValue")]
        [DataRow("notATestName", "notATestValue")]
        public void FileConfig_GetConfigValue_CorrectName(string name, string expectedValue)
        {
            setup();
            createFile();
            
            testConfig.GetConfigValue(name, out string? value);

            Assert.AreEqual(expectedValue, value);

            cleanup();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileConfig_SetConfigValue_FileDoesNotExist_ThrowsException()
        {
            setup();

            testConfig.SetConfigValue("testName", "testValue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileConfig_SetConfigValue_NameIsNull_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue(null, "testValue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileConfig_SetConfigValue_ValueIsNull_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("testName", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_SetConfigValue_NameIsEmptyString_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("", "testValue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_SetConfigValue_ValueIsEmptyString_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("testName", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_SetConfigValue_NameHasSpaces_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("te stNa me ", "testValue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_SetConfigValue_ValueHasSpaces_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("testName", "test Va lu e ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_SetConfigValue_NameHasEqualsSign_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("tes=tNa=me", "testValue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileConfig_SetConfigValue_ValueHasEqualsSign_ThrowsException()
        {
            setup();
            testConfig.SetConfigValue("testName", "test=Value");
        }

        [TestMethod]
        public void FileConfig_SetConfigValue_CorrectValues_ReturnsTrue()
        {
            setup();
            createFile();
            
            bool works = testConfig.SetConfigValue("testName", "testValue");

            Assert.IsTrue(works);
            cleanup();
        }

        [TestMethod]
        [DataRow("testName", "testValue2")]
        [DataRow("notATestName", "testValue3")]
        [DataRow("testName", "testName")]
        public void FileConfig_SetConfigValue_CorrectValuesInserted_ReturnsTrue(string name, string ExpectedValue)
        {
            setup();
            createFile();

            bool works = testConfig.SetConfigValue(name, ExpectedValue);

            string writtenString = "";
            foreach (string line in File.ReadAllLines(FileName))
            {
                string[] nameValuePair = line.Split("=");
                if (nameValuePair[0].Equals(name))
                {
                    writtenString = nameValuePair[1];
                    break;
                }
            }

            Assert.AreEqual(ExpectedValue, writtenString);
            cleanup();
        }

        [TestMethod]
        [DataRow("testName2", "testValue2")]
        [DataRow("notATestName2", "testValue3")]
        [DataRow("testName3", "testValue4")]
        public void FileConfig_SetConfigValue_CorrectNewNameValuesInserted_ReturnsTrue(string name, string ExpectedValue)
        {
            setup();
            createFile();

            bool works = testConfig.SetConfigValue(name, ExpectedValue);

            string writtenString = "";
            foreach (string line in File.ReadAllLines(FileName))
            {
                string[] nameValuePair = line.Split("=");
                if (nameValuePair[0].Equals(name))
                {
                    writtenString = nameValuePair[1];
                    break;
                }
            }

            Assert.AreEqual(ExpectedValue, writtenString);
            cleanup();
        }
    }
}
