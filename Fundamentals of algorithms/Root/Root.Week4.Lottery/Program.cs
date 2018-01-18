using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.Lottery
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            var s = inputs[0];
            var p = inputs[1];

            var ranges = new List<Tuple<int, int>>();
            for (int i = 0; i < s; i++)
            {
                var range = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                ranges.Add(new Tuple<int, int>(range[0], range[1]));
            }

            var points = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var result = OrganizeLottery(ranges, points);

            Console.WriteLine(string.Join(" ", result));
        }

        private static List<int> OrganizeLottery(List<Tuple<int, int>> ranges, List<int> points)
        {
            var list = ranges
                .SelectMany(x => 
                    new[] { new Tuple<int, Types, int?>(x.Item1, Types.Left, null), new Tuple<int, Types, int?>(x.Item2, Types.Right, null) })
                .ToList();
            list.AddRange(points.Select((x, i) => new Tuple<int, Types, int?>(x, Types.Point, i)));
            
            var sorted = list
                .OrderBy(x => x.Item1, new RangeComparer2())
                .ThenBy(x => x.Item2, new TypesComparer())
                .ToList();

            var result = Enumerable.Range(0, points.Count).ToList();
            var count = 0;
            foreach (var item in sorted)
            {
                switch(item.Item2)
                {
                    case Types.Left:
                        count++;
                        break;
                    case Types.Right:
                        count--;
                        break;
                    case Types.Point:
                        var i = (int) item.Item3;
                        result[i] = count;
                        break;
                }
            }

            return result;
        }

        private class TypesComparer : IComparer<Types>
        {
            public int Compare(Types x, Types y)
            {
                if (x == Types.Left)
                {
                    if (y == Types.Left)
                    {
                        return 0;
                    }

                    return -1;
                }
                else if (x == Types.Right)
                {
                    if (y == Types.Right)
                    {
                        return 0;
                    }

                    return 1;
                }
                else if (x == Types.Point)
                {
                    if (y == Types.Left)
                    {
                        return 1;
                    }
                    else if (y == Types.Right)
                    {
                        return -1;
                    }
                    else if (y == Types.Point)
                    {
                        return 0;
                    }
                }

                throw new ArgumentNullException();
            }
        }

        private class RangeComparer2 : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x.CompareTo(y);
            }
        }

        private class RangeComparer : IComparer<Tuple<int, Types, int?>>
        {
            public int Compare(Tuple<int, Types, int?> x, Tuple<int, Types, int?> y)
            {
                var result = x.Item1.CompareTo(y.Item1);
                //if (result == 0)
                //{
                //    if (x.Item2 == Types.Left)
                //    {
                //        result = -1;
                //    }
                //    else if (x.Item2 == Types.Right)
                //    {
                //        result = 1;
                //    }
                //    else if (x.Item2 == Types.Point)
                //    {
                //        if (y.Item2 == Types.Left)
                //        {
                //            result = 1;
                //        }
                //        else if (y.Item2 == Types.Right)
                //        {
                //            result = -1;
                //        }
                //        else if (y.Item2 == Types.Point)
                //        {
                //            result = 0;
                //        }
                //    }

                //    //switch (x.Item2)
                //    //{
                //    //    case Types.Left:
                //    //        result = -1;
                //    //        break;
                //    //    case Types.Right:
                //    //        result = 1;
                //    //        break;
                //    //    case Types.Point:
                //    //        switch (y.Item2)
                //    //        {
                //    //            case Types.Left:
                //    //                result = 1;
                //    //                break;
                //    //            case Types.Right:
                //    //                result = -1;
                //    //                break;
                //    //            case Types.Point:
                //    //                result = 0;
                //    //                break;
                //    //            default:
                //    //                throw new ArgumentException();
                //    //        }
                //    //        break;
                //    //    default:
                //    //        throw new ArgumentException();
                //    //}
                //}
                return result;
            }
        }

        private enum Types
        {
            Left = 0,
            Right = 1,
            Point = 2
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
