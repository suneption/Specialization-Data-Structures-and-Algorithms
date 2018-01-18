using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.QuickSort3
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var vs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var result = QuickSort3(vs);

            Console.WriteLine(string.Join(" ", result));
        }

        static List<int> QuickSort3(List<int> vs)
        {
            if (!vs.Any())
            {
                return vs;
            }

            var pivot = GetPivot(vs);
            var b = vs[pivot];
            vs[pivot] = vs[vs.Count - 1];
            vs[vs.Count - 1] = b;

            var m1m2 = Partition(vs, vs.Count - 1);
            var m1 = m1m2[0];
            var m2 = m1m2[1];
            var leftPart = vs.Take(m1).ToList();
            var middlePart = vs.Skip(m1).Take(m2 - m1 + 1).ToList();
            var rightPart = vs.Skip(m2 + 1).ToList();

            var sortedLeft = QuickSort3(leftPart);
            var sortedRight = QuickSort3(rightPart);

            return sortedLeft.Concat(middlePart).Concat(sortedRight).ToList();
        }

        static int[] Partition(List<int> vs, int pivot)
        {
            var pivotValue = vs[pivot];
            var j = 0;
            var k = 0;
            for (int i = 0; i < vs.Count; i++)
            {
                if (i == pivot)
                {
                    continue;
                }

                if (vs[i] < pivotValue)
                {
                    var bi = vs[i];
                    var bk = vs[k];
                    vs[k] = vs[j];
                    vs[j] = bi;

                    if (i > k)
                    {
                        vs[i] = bk;
                    }

                    j++;
                    k++;
                }
                else if (vs[i] == pivotValue)
                {
                    var b = vs[k];
                    vs[k] = vs[i];
                    vs[i] = b;
                    k++;
                }
            }

            vs[pivot] = vs[k];
            vs[k] = pivotValue;

            return new int[] { j, k };
        }

        static int GetPivot(List<int> vs)
        {
            var first = vs.First();
            var last = vs.Last();
            var mi = vs.Count / 2;
            var middle = vs[vs.Count / 2];
            if (first < middle)
            {
                if (middle < last)
                {
                    return mi;
                }
                else if (first > last)
                {
                    return 0;
                }
                else
                {
                    return vs.Count - 1;
                }
            }
            else
            {
                if (first < last)
                {
                    return 0;
                }
                else if (middle > last)
                {
                    return mi;
                }
                else
                {
                    return vs.Count - 1;
                }
            }
        }
    }
}
