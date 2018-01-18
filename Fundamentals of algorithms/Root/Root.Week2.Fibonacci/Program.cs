using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);

            var result = FibonacciFastStack(n);

            Console.WriteLine(result);
        }

        static int FibonacciFastStack(int n)
        {
            var stack = new Stack<int>();

            var currInd = 0;
            var fn = 0;

            while (currInd <= n)
            {
                if (currInd <= 1)
                {
                    fn = currInd;
                }
                else
                {
                    var fn_1 = stack.Pop();
                    var fn_2 = stack.Pop();
                    fn = fn_1 + fn_2;

                    stack.Push(fn_1);
                }

                stack.Push(fn);
                currInd++;
            }

            return stack.Pop();
        }
    }
}
