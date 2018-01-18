using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.DifferentSummands
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var result = DifferentSummands(n);

            Console.WriteLine(result.Count);
            Console.WriteLine(string.Join(" ", result));
        }

        static List<int> DifferentSummands(int n)
        {
            var result = new List<int>();
            var l = 1;
            var k = n;

            while(k > 0)
            {
                var doubleL = 2 * l;
                if (k <= doubleL)
                {
                    result.Add(k);
                    break;
                }
                else
                {
                    result.Add(l);
                    k -= l;
                    l++;
                }
            }

            return result;
        }
    }
}
