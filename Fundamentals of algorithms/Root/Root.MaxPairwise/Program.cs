using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.MaxPairwise
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var valuesStr = Console.ReadLine();
            var values = valuesStr.Split(' ')
                .Select(x => long.Parse(x))
                .ToList();
            
            var maxPairwise = MaxPairwiseProduct(values);

            Console.WriteLine(maxPairwise);
        }

        public static long MaxPairwiseProduct(List<long> values)
        {
            long max1 = 0;
            long max2 = 0;
            var max1ind = -1;
            var max2ind = -1;
            for (var i = 0; i < values.Count; i++)
            {
                var value = values[i];
                if (value > max1)
                {
                    max2ind = max1ind;
                    max1ind = i;
                }
                else if (value > max2)
                {
                    max2ind = i;
                }

                if (max1ind > -1)
                {
                    max1 = values[max1ind];
                }

                if (max2ind > -1)
                {
                    max2 = values[max2ind];
                }
            }

            var maxPairwise = max1 * max2;

            return maxPairwise;
        }
    }
}
