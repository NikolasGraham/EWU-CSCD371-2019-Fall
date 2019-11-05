using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mailbox.Tests
{
    [TestClass]
    public class DataLoaderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Load_StreamIsNull_ThrowNullException()
        {
            DataLoader dL = new DataLoader(null);
        }

        [TestMethod]
        public void Load_NotValidJson_ReturnsNull()
        {
            MemoryStream stream = GenerateTextFile();
            DataLoader dL = new DataLoader(stream);
            List<Mailbox> worked = dL.Load();

            stream.Close();

            Assert.IsNull(worked);
        }

        [TestMethod]
        public void Load_GeneratedFile_ReturnsCorrectStringRepresentation()
        {
            string filePath = Path.GetRandomFileName();
            MemoryStream stream = GenerateJsonFile();
            DataLoader dL = new DataLoader(stream);
            List<Mailbox> returnValue = dL.Load();
            List<Mailbox> expectedValue = GenerateExpectedValue();

            stream.Close();

            Assert.AreEqual(expectedValue.ToString(), returnValue.ToString());
        }

        [TestMethod]
        public void Save_GenerateValidInfo_SavesCorrectly()
        {
            string filePath = Path.GetRandomFileName();
            MemoryStream stream = new MemoryStream();
            DataLoader dL = new DataLoader(stream);
            dL.Save(GenerateExpectedValue());
            List<Mailbox> returnValue = dL.Load();
            List<Mailbox> expectedValue = GenerateExpectedValue();

            stream.Close();

            Assert.AreEqual(expectedValue.ToString(), returnValue.ToString());
        }

        private List<Mailbox> GenerateExpectedValue()
        {
            return new List<Mailbox> 
            {
                new Mailbox(Sizes.Small, (1, 1), new Person("firstName", "lastName")),
                new Mailbox(Sizes.SmallPremium, (5, 3), new Person("firstName2", "lastName2")),
                new Mailbox(Sizes.Medium, (15, 5), new Person("firstName3", "lastName3")),
                new Mailbox(Sizes.Large, (25, 7), new Person("firstName4", "lastName4")),
                new Mailbox(Sizes.LargePremium, (30, 10), new Person("firstName5", "lastName5"))
            };
        }

        private MemoryStream GenerateJsonFile()
        {
            MemoryStream source = new MemoryStream();

            using(StreamWriter writer = new StreamWriter(source, leaveOpen: true))
            {
                foreach(Mailbox mB in GenerateExpectedValue())
                {
                    string json = JsonConvert.SerializeObject(mB);
                    writer.WriteLine(json);
                }
            }
            return source;
        }

        private MemoryStream GenerateTextFile()
        {
            MemoryStream source = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(source, leaveOpen: true))
            {
                writer.WriteLine("Line of Text!!");
            }

            return source;
        }
    }
}
