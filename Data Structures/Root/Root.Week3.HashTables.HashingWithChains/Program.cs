using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.HashTables.HashingWithChains
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = int.Parse(Console.ReadLine());
            var n = int.Parse(Console.ReadLine());
            var queries = Enumerable.Range(0, n).Select(x => Console.ReadLine()).ToList();

            var result = Compute(m, queries);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static List<string> Compute(int m, List<string> queries)
        {
            var dict = Enumerable.Range(0, m).Select(x => new LinkedList<string>()).ToList();
            var result = new List<string>();

            foreach (var query in queries)
            {
                var splitted = query.Split(' ');
                var command = splitted[0];

                switch (command)
                {
                    case "add":
                        {
                            var str = splitted[1];
                            var hash = Hash(str, m);
                            var chain = dict[hash];

                            if (!chain.Contains(str))
                            {
                                chain.AddFirst(str);
                            }

                            break;
                        }
                    case "del":
                        {
                            var str = splitted[1];
                            var hash = Hash(str, m);
                            var chain = dict[hash];

                            if (chain.Contains(str))
                            {
                                chain.Remove(str);
                            }

                            break;
                        }
                    case "find":
                        {
                            var str = splitted[1];
                            var hash = Hash(str, m);
                            var chain = dict[hash];

                            if (chain.Contains(str))
                            {
                                result.Add("yes");
                            }
                            else
                            {
                                result.Add("no");
                            }

                            break;
                        }
                    case "check":
                        {
                            var i = int.Parse(splitted[1]);
                            var chain = dict[i];

                            result.Add(string.Join(" ", chain));

                            break;
                        }
                    default:
                        throw new NotImplementedException();
                }
            }

            return result;
        }

        private static int Hash(string s, int m)
        {
            var x = 263;
            var p = 1000000007;
            
            var result = checked(s.Select((ch, i) => ch * PowMod(x, i, p)).Sum());
            result %= p;
            result %= m;

            return (int)result;
        }

        private static long PowMod(int x, int pow, int p)
        {
            if (pow == 0)
            {
                return 1;
            }

            var result = Enumerable.Range(0, pow)
                .Select(i => (long)x)
                .Aggregate((acc, i) =>
                {
                    var r = checked(acc * i);
                    if (r > p)
                    {
                        return r % p;
                    }
                    return r;
                });

            //long result = 1;
            //for (var i = 0; i < pow; i++)
            //{
            //    result = result * x;
            //    if (result > p)
            //    {
            //        result %= p;
            //    }
            //}
            //result %= p;
            return result;
        }
    }
}
