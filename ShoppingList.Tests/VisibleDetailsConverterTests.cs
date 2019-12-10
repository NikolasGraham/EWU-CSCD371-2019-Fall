using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingList.Tests
{
    [TestClass]
    public class VisibleDetailsConverterTests
    {
        [TestMethod]
        public void Convert_Item_ReturnVisibleObject()
        {
            VisibleDetailsConverter VDC = new VisibleDetailsConverter();

            Item item = new Item("Pineapple");

            string visibility = (string)VDC.Convert(item);

            Assert.AreEqual("Visible", visibility);
        }
    }
}