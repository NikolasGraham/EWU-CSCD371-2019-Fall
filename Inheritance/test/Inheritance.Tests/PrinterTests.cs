using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Inheritance.Tests
{
    [TestClass]
    public class PersonPrinterTests
    {
        [TestMethod]
        public void PersonGetsPrinted()
        {
            // Arrange
            var item = new TestItem { Name = "Test Item" };

            using (var stream = new MemoryStream()) {
                using (var writer = new StreamWriter(stream))
                {
                    // Act
                    Printer.Print(item, writer);
                    writer.Flush();

                    stream.Position = 0;
                    stream.Seek(0, SeekOrigin.Begin);

                    // Assert
                    using (var reader = new StreamReader(stream))
                    {
                        var lineWritten = reader.ReadLine();
                        Assert.AreEqual("Test Item", lineWritten);
                    }
                }
            }
        }

        [TestMethod]
        public void Food_OverridesPrintInfoInItemCorrectly()
        {
            // Arrange
            Item item = new Food { Upc = "12345", Brand = "InsertNonCopyrightedNameHere" };

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    // Act
                    Printer.Print(item, writer);
                    writer.Flush();

                    stream.Position = 0;
                    stream.Seek(0, SeekOrigin.Begin);

                    // Assert
                    using (var reader = new StreamReader(stream))
                    {
                        var lineWritten = reader.ReadLine();
                        Assert.AreEqual("12345 - InsertNonCopyrightedNameHere", lineWritten);
                    }
                }
            }
        }

        [TestMethod]
        public void Television_OverridesPrintInfoInItemCorrectly()
        {
            // Arrange
            Item item = new Television { Size = "40in", Manufacturer = "InsertNonCopyrightedNameHere" };

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    // Act
                    Printer.Print(item, writer);
                    writer.Flush();

                    stream.Position = 0;
                    stream.Seek(0, SeekOrigin.Begin);

                    // Assert
                    using (var reader = new StreamReader(stream))
                    {
                        var lineWritten = reader.ReadLine();
                        Assert.AreEqual("InsertNonCopyrightedNameHere - 40in", lineWritten);
                    }
                }
            }
        }
    }

    public class TestItem : Item {
        public string Name { get; set; }

        public override string PrintInfo()
        {
            return Name;
        }
    }
}
