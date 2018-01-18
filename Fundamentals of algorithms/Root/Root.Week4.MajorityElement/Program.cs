using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.MajorityElement
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var vs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var result = MajorityElement(vs);

            Console.WriteLine(result != -1 ? 1 : 0);
        }

        static int MajorityElement(List<int> vs)
        {
            if (vs.Count == 1)
            {
                return vs.First();
            }

            var m = vs.Count / 2;
            var leftPart = vs.Take(m).ToList();
            var leftElement = MajorityElement(leftPart);

            var rightPart = vs.Skip(m).ToList();
            var rightElement = MajorityElement(rightPart);

            var result = -1;
            if (leftElement != -1 && leftElement == rightElement)
            {
                result = leftElement;
            }
            else
            {
                var rightCount = vs.Count(x => x == rightElement);
                if (rightCount > m)
                {
                    return rightElement;
                }
                var leftCount = vs.Count(x => x == leftElement);
                if (leftCount > m)
                {
                    return leftElement;
                }
            }

            return result;
        }
    }
}
