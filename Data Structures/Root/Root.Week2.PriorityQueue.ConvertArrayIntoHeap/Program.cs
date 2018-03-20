using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.PriorityQueue.ConvertArrayIntoHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var aVs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var swaps = BuildHeap(aVs);
            //Assert(aVs);

            Console.WriteLine(swaps.Count);

            var result = string.Join(Environment.NewLine, swaps.Select(x => x.Item1 + " " + x.Item2).ToList());
            Console.Write(result);
        }

        private static void Assert(List<int> heap)
        {
            var n = heap.Count;
            for (var i = 0; i < n/2; i++)
            {
                var value = heap[i];

                var leftI = Left(i);
                var rightI = Right(i);

                var left = heap[leftI];
                var right = heap[rightI];

                Contract.Assert(value <= left && value <= right);
            }
        }

        private static List<Tuple<int, int>> BuildHeap(List<int> aVs)
        {
            var result = new List<Tuple<int, int>>();
            var n = aVs.Count;
            for (var i = n/2; i >= 0; i--)
            {
                var swap = SiftDown(i, aVs);
                if (swap.Any())
                {
                    result.AddRange(swap);
                }
            }
            return result;
        }

        private static Stack<Tuple<int, int>> SiftDown(int i, List<int> values)
        {
            var n = values.Count;
            var value = values[i];

            var leftChildIndex = Left(i);
            var rightChildIndex = Right(i);

            if (leftChildIndex > n - 1)
            {
                return new Stack<Tuple<int, int>>();
            }

            if (rightChildIndex > n - 1)
            {
                rightChildIndex = leftChildIndex;
            }

            var leftChild = values[leftChildIndex];
            var rightChild = values[rightChildIndex];

            var minChildIndex = leftChild < rightChild ? leftChildIndex : rightChildIndex;
            var minChild = values[minChildIndex];

            if (value > minChild)
            {
                values[i] = minChild;
                values[minChildIndex] = value;
                var swap = new Tuple<int, int>(i, minChildIndex);
                var nextSwaps = SiftDown(minChildIndex, values);
                nextSwaps.Push(swap);
                return nextSwaps;
            }

            return new Stack<Tuple<int, int>>();
        }

        private static int Left(int i)
        {
            return 2 * i + 1;
        }

        private static int Right(int i)
        {
            return 2 * i + 2;
        }
    }
}
