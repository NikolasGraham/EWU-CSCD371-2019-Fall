using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingList.Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void Constructor_ItemNameCreates()
        {
            Item item = new Item("TestItem");

            Assert.IsTrue(item is Item);
        }

        [TestMethod]
        public void Constructor_ItemNameCheckedCreates()
        {
            Item item = new Item("TestItem", true);

            Assert.IsTrue(item.CrossedOff == true);
        }

        [TestMethod]
        public void Compare_DoesntMatch()
        {
            Item item = new Item("TestItem", true);
            Item item2 = new Item("NotATestItem", true);

            int test = item.CompareTo(item2);

            Assert.IsTrue(test == 1 || test == -1); // 1/-1 means names are not equal
        }

        [TestMethod]
        public void Compare_DoesMatch()
        {
            Item item = new Item("TestItem", true);
            Item item2 = new Item("TestItem");

            int test = item.CompareTo(item2);

            Assert.IsTrue(test == 0); // 0 means names are equal
        }
    }
}
