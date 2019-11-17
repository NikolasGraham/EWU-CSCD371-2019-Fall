using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sorter.Tests
{
    [TestClass]
    public class SortUtilityTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SortUtility_NullArray_ThrowsException()
        {
            SortUtility sortingUtils = new SortUtility();

            sortingUtils.selectionSort(null, (int left, int right) => left > right);
        }

        [TestMethod]
        [DataRow(new int[] {1,3,5,7,9,2,4,6,8}, new int[] {1,2,3,4,5,6,7,8,9})]
        [DataRow(new int[] {1,9,2,8,3,7,4,6,5}, new int[] {1,2,3,4,5,6,7,8,9})]
        public void SortUtility_ShouldSortAscending_UsingAnAnonymousMethod(int[] toSort, int[] expected)
        {
            SortUtility sortingUtils = new SortUtility();
            intComparer comparer = delegate (int left, int right)
            {
                return left < right;
            };

            sortingUtils.selectionSort(toSort, comparer);

            CollectionAssert.AreEqual(expected, toSort);
        }

        [TestMethod]
        [DataRow(new int[] {1,63,632,727,247,472}, new int[] {727,632,472,247,63,1})]
        [DataRow(new int[] {1,2,3,4,5,6,7,8,9}, new int[] {9,8,7,6,5,4,3,2,1})]
        public void SortUtility_ShouldSortDescending_UsingLambdaExpression(int[] toSort, int[] expected)
        {
            SortUtility sortingUtils = new SortUtility();

            sortingUtils.selectionSort(toSort, (int left, int right) => left > right);

            CollectionAssert.AreEqual(expected, toSort);
        }

        [TestMethod]
        [DataRow(new int[] {1,2,3,4,5,6,7,8,9}, new int[] {4,3,2,1,5,6,7,8,9})]
        [DataRow(new int[] {1,9,2,8,3,7,4,6,5}, new int[] {4,3,2,1,5,6,7,8,9})]
        [DataRow(new int[] {9,8,7,6,5}, new int[] {5,6,7,8,9})]
        [DataRow(new int[] {1,2,3,4}, new int[] {4,3,2,1})]
        public void SortUtility_ShouldSortDescendingUnderFiveThenAscendToMax_UsingLambdaStatement(int[] toSort, int[] expected)
        {
            SortUtility sortingUtils = new SortUtility();

            sortingUtils.selectionSort(toSort, (int left, int right) => {
                if(left > 4 || right > 4) { return left < right; }
                return left > right;
            });

            CollectionAssert.AreEqual(expected, toSort);
        }
    }
}
