using System;
using System.Collections.Generic;
using System.Linq;

namespace Root.Week4.Inversions
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var vs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var result = Inversions(vs);

            Console.WriteLine(result);
        }

        static int Inversions(List<int> vs)
        {
            var result = MergeSort(vs);
            return result.Item2;
        }

        static Tuple<List<int>, int> MergeSort(List<int> vs)
        {
            if (vs.Count == 1)
            {
                return new Tuple<List<int>, int>(vs, 0);
            }

            var m = vs.Count / 2;
            var leftPart = vs.Take(m).ToList();
            var rightPart = vs.Skip(m).ToList();
            
            var leftResult = MergeSort(leftPart);
            var rightResult = MergeSort(rightPart);

            var result = Merge(leftResult.Item1, rightResult.Item1);

            var count = leftResult.Item2 + rightResult.Item2 + result.Item2;

            return new Tuple<List<int>, int>(result.Item1, count);
        }

        static Tuple<List<int>, int> Merge(List<int> B, List<int> C)
        {
            var result = new List<int>();
            var count = 0;

            var j = 0;
            var i = 0;
            while (i < B.Count && j < C.Count)
            {
                var b = B[i];
                var c = C[j];

                if (b > c)
                {
                    result.Add(c);
                    count++;
                    j++;
                }
                else
                {
                    result.Add(b);
                    i++;
                }
            }

            if (i == B.Count)
            {
                result.AddRange(C.Skip(j));
            }
            else if (j == C.Count)
            {
                var rest = B.Skip(i).ToList();
                result.AddRange(rest);
                count += (rest.Count - 1) * C.Count;
            }

            return new Tuple<List<int>, int>(result, count);
        }
    }
}
