using Assignment6;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void Constructor_InvalidLength()
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(0);
        }

        [TestMethod]
        public void Contructor_ValidLength()
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCount_AddNull()
        {
            ArrayCollection<string> collection = new ArrayCollection<string>(1);

            collection.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddCount_AddMoreVariablesThanSize()
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(1);

            for (int i = 0; i < 2; i++)
            {
                collection.Add(i);
            }
        }

        [TestMethod]
        [DataRow(4, 2)]
        [DataRow(8, 3)]
        public void AddCount_AddItemsAndCountThem(int size, int expected)
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(size);

            for (int i = 0; i < expected; i++)
            {
                collection.Add(i);
            }

            Assert.AreEqual(expected, collection.Count);
        }

        [TestMethod]
        public void Indexer_InRange()
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(5);
            ArrayCollection<int> collectionExpected = new ArrayCollection<int>(5);

            for (int i = 0; i < 5; i++)
            {
                collection[i] = i;
            }

            for (int i = 0; i < 5; i++)
            {
                collectionExpected.Add(i);
            }

            Assert.AreEqual(collectionExpected.ToString(), collection.ToString());
        }

        [TestMethod]
        public void Collection_ForEach()
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(5);
            ArrayCollection<int> collectionExpected = new ArrayCollection<int>(5);

            for (int i = 0; i < 5; i++)
            {
                collection.Add(i);
            }

            for (int i = 5; i < 10; i++)
            {
                collectionExpected.Add(i);
            }

            int counter = 0;
            foreach (int item in collection)
            {
                collectionExpected[counter] = item;
                counter++;
            }

            Assert.AreEqual(collectionExpected.ToString(), collection.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Contains_InvalidData_ThrowsException()
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(5);

            for (int i = 0; i < 5; i++)
            {
                collection.Add(i);
            }

            bool found = collection.Contains(5);
        }

        [TestMethod]
        [DataRow(4, 1)]
        [DataRow(8, 4)]
        [DataRow(1240, 1239)]
        [DataRow(1, 0)]
        [DataRow(5, 0)]
        public void Contains_ValidData(int size, int toFind)
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(size);

            for (int i = 0; i < size; i++)
            {
                collection.Add(i);
            }

            Assert.IsTrue(collection.Contains(toFind));
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(8)]
        public void Clear_ClearsCollection(int size)
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(size);

            for (int i = 0; i < size; i++)
            {
                collection.Add(i);
            }

            collection.Clear();

            Assert.IsTrue(collection.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_NullItem_ItemDoesNotExistInCollection()
        {
            ArrayCollection<string> collection = new ArrayCollection<string>(5);

            for (int i = 0; i < 5; i++)
            {
                collection.Add(i + "");
            }

            Assert.IsFalse(collection.Remove(null));
        }

        [TestMethod]
        [DataRow(5, 2)]
        [DataRow(14, 3)]
        [DataRow(2, 1)]
        [DataRow(12521, 12123)]
        public void Remove_ValidItem(int size, int toRemove)
        {
            ArrayCollection<int> collection = new ArrayCollection<int>(5);

            for (int i = 0; i < 5; i++)
            {
                collection.Add(i);
            }

            bool removed = false;

            try
            {
                collection.Remove(toRemove);
                collection.Contains(toRemove);
            }
            catch (ArgumentException) //This catch throws if the item is no longer in the collection
            {
                removed = true;
            }

            Assert.IsTrue(removed);
        }

        [TestMethod]
        [DataRow(null, 0)]
        [DataRow(new[] {"","","",""}, 0)] //Not enough space
        [DataRow(new[] { "", "", "", ""}, 3)] //Not enough space from specified index
        public void CopyTo_InvalidInput(string[] array, int index)
        {
            bool failed = false;
            ArrayCollection<string> collection = new ArrayCollection<string>(5);

            for (int i = 0; i < 5; i++)
            {
                collection.Add(i + "");
            }

            try
            {
                collection.CopyTo(array, index);
            }
            catch (ArgumentNullException)
            {
                failed = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                failed = true;
            }

            Assert.IsTrue(failed);
        }

        [TestMethod]
        [DataRow(5, 5, 0)]
        [DataRow(15, 2, 13)]
        [DataRow(1, 1, 0)]
        public void CopyTo_ValidInput(int arrayLength, int collectionSize, int index)
        {
            bool success = true;
            ArrayCollection<string> collection = new ArrayCollection<string>(collectionSize);
            string[] array = new string[arrayLength];

            for (int i = 0; i < collectionSize; i++)
            {
                collection.Add(i + "");
            }

            try
            {
                collection.CopyTo(array, index);
            }
            catch (ArgumentNullException)
            {
                success = false;
            }
            catch (ArgumentOutOfRangeException)
            {
                success = false;
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void CopyTo_ValidInput_CorrectDataInArray()
        {
            ArrayCollection<string> collection = new ArrayCollection<string>(7);
            string[] array = new string[7];
            string[] expectedValue = { "0", "1", "2", "3", "4", "5", "6" };

            for (int i = 0; i < 7; i++)
            {
                collection.Add(i + "");
            }

            collection.CopyTo(array, 0);

            Assert.AreEqual(expectedValue.ToString(), array.ToString());
        }
    }
}
