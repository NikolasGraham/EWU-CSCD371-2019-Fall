using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ShoppingList.Tests
{
    [TestClass]
    public class CrossOffConverterTests
    {
        [TestMethod]
        public void Convert_CrossOff_CrossesOffItem()
        {
            CrossOffConverter COC = new CrossOffConverter();

            Item item = new Item("Pineapple", true);

            var crossedOff = COC.Convert(item.CrossedOff);

            Assert.AreEqual(TextDecorations.Strikethrough, crossedOff);
        }

        [TestMethod]
        public void Convert_CrossOff_UnCrossesOffItem()
        {
            CrossOffConverter COC = new CrossOffConverter();

            Item item = new Item("Pineapple", false);

            var crossedOff = COC.Convert(item.CrossedOff);

            Assert.AreEqual(null, crossedOff);
        }
    }
}
