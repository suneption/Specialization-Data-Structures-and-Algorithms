using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.Lcm
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var vals = input.Split(' ');
            var a = int.Parse(vals.First());
            var b = int.Parse(vals.Last());

            var lcm = Lcm(a, b);

            Console.WriteLine(lcm);
        }

        public static long Lcm(int a, int b)
        {
            var gcd = Gcd(a, b);
            long x = a / gcd;
            long y = b / gcd;

            var lcm = x * b;

            return lcm;
        }

        public static int Gcd(int a, int b)
        {
            var x = b > a ? b : a;
            var y = b > a ? a : b;

            var r = x % y;

            while (r != 0)
            {
                x = y;
                y = r;
                r = x % y;
            }

            return y;
        }
    }
}
