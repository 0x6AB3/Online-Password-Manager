using System;
using System.Collections.Generic;
using PasswordManagerUtilities;

namespace MergeTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Array size:\t");
            int[] arrayToSort = new int[int.Parse(Console.ReadLine())];

            DateTime initialisationStart = DateTime.Now;
            Random rng = new Random();
            DateTime initialisationFinish = DateTime.Now;

            DateTime rngStart = DateTime.Now;
            for (int i = 0; i < arrayToSort.Length; i++)
            {
                arrayToSort[i] = rng.Next(Int32.MinValue, Int32.MaxValue);
                /*
                if ((i+1) % 100000 == 0)
                {
                    Console.Write($"Adding element {i+1}\n");
                }
                */
            }
            DateTime rngFinish = DateTime.Now;

            /*
            int[] arrayToSort = new int[0];
            List<int> inputBuffer = new List<int>();
            while (true)
            {
                Console.Write("Input number (type sort for mergesort):\t");
                string input = Console.ReadLine();
                try
                {
                    inputBuffer.Add(int.Parse(input)); ;
                }
                catch
                {
                    if (input.ToLower() == "sort")
                    {
                        break;
                    }
                }
            }
            
            arrayToSort = inputBuffer.ToArray();
            */
            Console.WriteLine("Sorting...");
            DateTime sortStart = DateTime.Now;
            MergeSort.Sort(ref arrayToSort);
            DateTime sortFinish = DateTime.Now;
            Console.WriteLine("Sorted list:");
            /*
            foreach (int number in arrayToSort)
            {
                Console.WriteLine(number);
            }
            */
            Console.Write($"\nInitialisation:\t{(initialisationFinish-initialisationStart).TotalMilliseconds}ms\n" +
                $"Number generation:\t{(rngFinish-rngStart).TotalMilliseconds}ms\nMerge sort:\t{(sortFinish-sortStart).TotalMilliseconds}ms");

            Console.ReadLine();

        }

    }
}
