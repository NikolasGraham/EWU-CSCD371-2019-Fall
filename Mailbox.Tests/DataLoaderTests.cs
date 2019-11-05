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
        public void DataLoader_Load_StreamIsNull_ThrowNullExeption()
        {
            DataLoader dL = new DataLoader(null);
        }

        [TestMethod]
        public void DataLoader_Load_NotValidJson_ReturnsNull()
        {
            string filePath = Path.GetRandomFileName();
            Stream stream = GenerateTextFile(filePath);
            DataLoader dL = new DataLoader(stream);
            List<Mailbox> worked = dL.Load();

            stream.Close();
            File.Delete(filePath);
            Assert.IsNull(worked);
        }

        [TestMethod]
        public void DataLoader_Load()
        {
            string filePath = Path.GetRandomFileName();
            Stream stream = GenerateJsonFile(filePath);
            DataLoader dL = new DataLoader(stream);
            List<Mailbox> returnValue = dL.Load();
            List<Mailbox> expectedValue = GenerateExpectedValue();

            stream.Close();
            File.Delete(filePath);

            Assert.AreEqual(expectedValue.ToString(), returnValue.ToString());
        }

        [TestMethod]
        public void DataLoader_Save()
        {
            string filePath = Path.GetRandomFileName();
            Stream stream = File.Open(filePath, FileMode.OpenOrCreate);
            DataLoader dL = new DataLoader(stream);
            dL.Save(GenerateExpectedValue());
            List<Mailbox> returnValue = dL.Load();
            List<Mailbox> expectedValue = GenerateExpectedValue();

            stream.Close();
            File.Delete(filePath);

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

        private Stream GenerateJsonFile(string filePath)
        {
            Stream source = File.Open(filePath, FileMode.OpenOrCreate);

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

        private Stream GenerateTextFile(string filePath)
        {
            Stream source = File.Open(filePath, FileMode.OpenOrCreate);

            using (StreamWriter writer = new StreamWriter(source, leaveOpen: true))
            {
                writer.WriteLine("Line of Text!!");
            }

            return source;
        }
    }
}
