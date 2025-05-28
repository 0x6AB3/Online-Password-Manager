using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerUtilities
{
    public class MergeSort
    {
        public static void Sort(ref int[] array)
        {
            if (array.Length < 2)
            {
                return;
            }

            int[] leftArray = SplitArray(array)[0];
            int[] rightArray = SplitArray(array)[1];

            Sort(ref leftArray);
            Sort(ref rightArray);

            int arrayIndex = 0;
            int leftIndex = 0;
            int rightIndex = 0;

            while (leftIndex < leftArray.Length && rightIndex < rightArray.Length)
            {
                if (leftArray[leftIndex] < rightArray[rightIndex])
                {
                    array[arrayIndex] = leftArray[leftIndex];
                    leftIndex++;
                }
                else
                {
                    array[arrayIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                arrayIndex++;
            }

            while (leftIndex < leftArray.Length)
            {
                array[arrayIndex] = leftArray[leftIndex];
                leftIndex++;
                arrayIndex++;
            }

            while (rightIndex < rightArray.Length)
            {
                array[arrayIndex] = rightArray[rightIndex];
                rightIndex++;
                arrayIndex++;
            }
        }
        private static List<int[]> SplitArray(int[] array)
        {
            int mid = (array.Length / 2);

            int[] leftArray = new int[mid];
            int[] rightArray = new int[array.Length - mid];

            for (int i = 0; i < mid; i++)
            {
                leftArray[i] = array[i];
            }

            for (int i = 0; i < array.Length - mid; i++)
            {
                rightArray[i] = array[mid + i];
            }

            return new List<int[]>() { leftArray, rightArray };
        }
    }
}