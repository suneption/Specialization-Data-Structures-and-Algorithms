using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.PartialSumOfFibonacciNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var values = input.Split(' ');
            var n = long.Parse(values[1]);
            var m = long.Parse(values[0]);

            var x = HugeFibonacciModuloM(n + 2);
            var fnSum = x == 0 ? x + 10 - 1 : x - 1 ;
            var y = HugeFibonacciModuloM(m + 1);
            var fm_1Sum = y == 0 ? y + 10 - 1 : y - 1;
            
            var sub = fnSum - fm_1Sum;
            var result = fnSum < fm_1Sum ? sub + 10 : sub;

            Console.WriteLine(result);
        }

        public static Decimal HugeFibonacciModuloM(long n)
        {
            var periodValues = new List<Decimal>();
            var hasPeriod = false;

            for (int i = 0; i <= n; i++)
            {
                var f = FibonacciFastStack(i);
                var mod = f % 10;
                periodValues.Add(mod);

                if (i < 2)
                {
                    continue;
                }

                if (periodValues[i - 1] == 0 && periodValues[i] == 1)
                {
                    hasPeriod = true;
                    break;
                }
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
