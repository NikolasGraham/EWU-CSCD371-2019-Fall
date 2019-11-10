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

            for(int i=0; i < expected; i++)
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
    }
}
