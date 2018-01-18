using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.LastDigitOfFibonacciSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var n = long.Parse(input);

            var fibonacciModuloM = HugeFibonacciModuloM(n + 2);
            var result = fibonacciModuloM == 0 ? fibonacciModuloM + 10 - 1 : fibonacciModuloM - 1;

            Console.WriteLine(result);
        }

        static long FibonacciLastDigit(long n)
        {
            var stack = new Stack<long>();

            long fn = 0;
            for (long i = 0; i <= n; i++)
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

        public static Decimal HugeFibonacciModuloM(long n)
        {
            var periodValues = new List<Decimal>();
            var hasPeriod = false;

            for (long i = 0; i <= n; i++)
            {
                var f = FibonacciFastStack(i);
                var mod = f % 10;

                if (i < 2)
                {
                    periodValues.Add(mod);
                    continue;
                }

                if (periodValues.Last() == 0 && mod == 1)
                {
                    hasPeriod = true;
                    periodValues.Add(mod);
                    break;
                }
                periodValues.Add(mod);
            }

            var periodLength = hasPeriod ? periodValues.Count - 2 : periodValues.Count;

            var r = hasPeriod ? n % periodLength : n;
            var fr = FibonacciFastStack(r);
            var result = fr % 10;

            return result;
        }

        static Decimal FibonacciFastStack(long n)
        {
            var stack = new Stack<Decimal>();

            var currInd = 0;
            Decimal fn = 0;

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
                    fn = checked(fn_1 + fn_2);

                    stack.Push(fn_1);
                }

                stack.Push(fn);
                currInd++;
            }

            return stack.Pop();
        }
    }
}
