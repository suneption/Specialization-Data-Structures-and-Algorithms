using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace Root.Week2.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = Console.ReadLine();
            //var n = int.Parse(input);

            //var result = Fibonacci(n);
            //var result1 = FibonacciFast(n);
            //var fibonacciFastStack = FibonacciFastStack(n);
            //var fibonacciLastDigit = FibonacciLastDigit(n);
            //var gcd = Gcd(6, 3);
            //var lcm = Lcm(1000000000, 999999999);
            var hugeFibonacciModuloM = HugeFibonacciModuloM(1, 239);
            //var hugeFibonacciModuloM = HugeFibonacciModuloM(2816213588, 30524);

            //Console.WriteLine(result);
            //Console.WriteLine(result1);
            //Console.WriteLine(fibonacciFastStack);
            //Console.WriteLine(fibonacciLastDigit);
            //Console.WriteLine(gcd);
            //Console.WriteLine(lcm);
            Console.WriteLine(hugeFibonacciModuloM);
        }

        static int Fibonacci(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            var result = Fibonacci(n - 1) + Fibonacci(n - 2);
            return result;
        }

        static int FibonacciFast(int n)
        {
            var arr = new List<int>();
            
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
                    var fn_1 = arr[currInd - 1];
                    var fn_2 = arr[currInd - 2];
                    fn = fn_1 + fn_2;
                }

                arr.Add(fn);
                currInd++;
            }

            return arr.Last();
        }

        static BigInteger FibonacciFastStack(long n)
        {
            var stack = new Stack<BigInteger>();

            var currInd = 0;
            BigInteger fn = 0;

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

        public static long Lcm(int a, int b)
        {
            var gcd = Gcd(a, b);
            long x = a / gcd;
            long y = b / gcd;

            var lcm = checked(x * b);

            return lcm;
        }

        public static BigInteger HugeFibonacciModuloM(long n, long m)
        {
            var periodValues = new List<BigInteger>();
            var hasPeriod = false;

            for (int i = 0; i <= n; i++)
            {
                var f = FibonacciFastStack(i);
                var mod = f % m;
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
            var result = fr % m;

            return result;
        }
    }
}
