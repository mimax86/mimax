using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimax.Performance.Threading
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new [] {3, 7, 8, 5, 2, 1, 9, 5, 4};
            QuickSorter.QuickSort(array);
        }
    }

    public class QuickSorter
    {
        public static void QuickSort<T>(T[] items) where T : IComparable<T>
        {
            QuickSort(items, 0, items.Length);
        }

        private static void QuickSort<T>(T[] items, int left, int right) where T : IComparable<T>
        {
            if (left == right) return;
            int pivot = Partition(items, left, right);
            QuickSort(items, left, pivot);
            QuickSort(items, pivot + 1, right);
        }

        private static int Partition<T>(T[] items, int left, int right) where T : IComparable<T>
        {
            int pivotPos = new Random().Next(left, right); //often a random index between left and right is used
            T pivotValue = items[pivotPos];
            Swap(ref items[right - 1], ref items[pivotPos]);
            int store = left;
            for (int i = left; i < right - 1; ++i)
            {
                if (items[i].CompareTo(pivotValue) < 0)
                {
                    Swap(ref items[i], ref items[store]);
                    ++store;
                }
            }

            Swap(ref items[right - 1], ref items[store]);
            return store;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}