using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.MaximizeRevenue
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var aValues = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var bValues = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            var result = MaximizeRevenueInOnlineAdPlacement(n, aValues, bValues);

            Console.WriteLine(result);
        }

        public static long MaximizeRevenueInOnlineAdPlacement(int n, int[] aValues, int[] bValues)
        {
            var orderedAs = aValues.OrderByDescending(x => x).ToArray();
            var orderedBs = bValues.OrderByDescending(x => x).ToArray();

            var result = Enumerable.Range(0, n).Sum(x => (long) orderedAs[x] * orderedBs[x]);

            return result;
        }
    }
}
