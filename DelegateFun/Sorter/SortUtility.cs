using System;

namespace Sorter
{
    public delegate bool intComparer(int left, int right);
    public class SortUtility
    {
        // Sort method should be implemented here
        // It should accept an int[] and a delegate you define that performs the actual comparison
        public void selectionSort(int[] ara, intComparer compare)
        {
            if(ara == null)
            {
                throw new ArgumentNullException(nameof(ara));
            }

            for(int i=0; i < ara.Length-1; i++)
            {
                int min = i;
                for(int k=i+1; k < ara.Length; k++)
                {
                    if(compare(ara[k], ara[min]))
                    {
                        min = k;
                    }
                }
                int temp = ara[min];
                ara[min] = ara[i];
                ara[i] = temp;
            }
        }
    }
}
