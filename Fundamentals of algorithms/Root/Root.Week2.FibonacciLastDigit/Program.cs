using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.FibonacciLastDigit
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = int.Parse(input);

            var result = FibonacciLastDigit(n);

            Console.WriteLine(result);
        }

        static int FibonacciLastDigit(int n)
        {
            var stack = new Stack<int>();

            var fn = 0;
            for (int i = 0; i <= n; i++)
            {
                if (i <= 1)
                {
                    fn = i;
                }
                else
                {
                    var fn_1 = stack.Pop();
                    var fn_2 = stack.Pop();
                    fn = (fn_1 + fn_2) % 10;

                    stack.Push(fn_1);
                }

                stack.Push(fn);
            }

            return stack.Pop();
        }
    }
}
