using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            var n = int.Parse(inputs[0]);
            var sortedValues = inputs.Skip(1).Select(x => int.Parse(x)).ToArray();

            inputs = Console.ReadLine().Split(' ');
            var k = int.Parse(inputs[0]);
            var bs = inputs.Skip(1).Select(x => int.Parse(x)).ToArray();

            var result = BinarySearch(sortedValues, bs);

            Console.WriteLine(string.Join(" ", result));
        }

        private static int[] BinarySearch(int[] sortedValues, int[] bs)
        {
            var result = new List<int>();

            foreach (var b in bs)
            {
                var i = BinarySearch(sortedValues, b, 0, sortedValues.Length);
                result.Add(i);
            }

            return result.ToArray();
        }

        private static int BinarySearch(int[] sortedValues, int b, int left, int right)
        {
            if (left >= right)
            {
                return -1;
            }

            var m = (left + right) / 2;
            var mValue = sortedValues[m];
            var result = 0;
            if (b == mValue)
            {
                result = m;
            }
            else if (b < mValue)
            {
                result = BinarySearch(sortedValues, b, left, m);
            }
            else if (b > mValue)
            {
                result = BinarySearch(sortedValues, b, m + 1, right);
            }
            return result;
        }
    }
}
