using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.CollectingSignatures
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var segments = Enumerable.Range(0, n)
                .Select(x =>
                {
                    var values = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    return new Segment(values[0], values[1]);
                })
                .ToArray();

            var result = CollectingSignatures(n, segments);

            Console.WriteLine(result.Count);
            Console.WriteLine(string.Join(" ", result));
        }

        public static List<int> CollectingSignatures(int n, Segment[] segments)
        {
            var orderedByB = segments.OrderBy(x => x.B).ToArray();
            var restSegments = orderedByB;
            var result = new List<int>();

            while (restSegments.Any())
            {
                var minB = restSegments.First();
                result.Add(minB.B);
                var appropriateSegments = restSegments
                    .Where(x => x.A <= minB.B && minB.B <= x.B)
                    .ToArray();
                restSegments = restSegments.Except(appropriateSegments).ToArray();
            }

            return result;
        }

        public class Segment
        {
            public int A { get; }
            public int B { get; }

            public Segment(int a, int b)
            {
                A = a; B = b;
            }
        }
    }
}
