using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week5.PrimitiveCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var result = Calculate(n);
            result.Reverse();

            Console.WriteLine(result.Count - 1);
            Console.WriteLine(string.Join(" ", result));
        }

        private static List<int> Calculate(int n)
        {
            var counts = new int[3];
            var prevs = new int[3] { 1, 1, 1 };
            var prevValue = 0;
            var result = 0;
            var count = 0;
            var vs = new List<int>() { n };
            var prevVs = Enumerable.Range(0, n + 1).Select(x => new Tuple<int, int>(0, 0)).ToList();
            for (int i = 2; i <= n; i++)
            {
                var result3 = 0;
                var result2 = 0;
                var result1 = 0;
                //var count3 = int.MaxValue;
                //var count2 = int.MaxValue;
                //var count1 = int.MaxValue;
                var ts = new List<Tuple<int, int>>();
                if (i % 3 == 0)
                {
                    result3 = i / 3;
                    var prev = prevVs[result3];
                    var count3 = prev.Item2;
                    count3++;
                    ts.Add(new Tuple<int, int>(result3, count3));
                }

                if (i % 2 == 0)
                {
                    result2 = i / 2;
                    var prev = prevVs[result2];
                    var count2 = prev.Item2;
                    count2++;
                    ts.Add(new Tuple<int, int>(result2, count2));
                }
                
                {
                    result1 = i - 1;
                    var prev = prevVs[result1];
                    var count1 = prev.Item2;
                    count1++;
                    ts.Add(new Tuple<int, int>(result1, count1));
                }

                var r = ts.OrderBy(x => x.Item2).First();
                
                vs.Add(r.Item1);
                prevVs[i] = r;
            }

            var results = new List<int>() { n };
            var j = n;
            while (j > 1)
            {
                var prev = prevVs[j];
                results.Add(prev.Item1);
                j = prev.Item1;
            }

            return results;
        }
    }
}