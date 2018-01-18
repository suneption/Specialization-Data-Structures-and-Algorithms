using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.MaximizingYourSalary
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var values = Console.ReadLine().Split(' ')
                .Select(x => int.Parse(x))
                .ToArray();

            //var values = new[] { 523, 52 };
            //Resolve(values.Length, values);

            var result = MaximizeYourSalary(n, values);

            Console.WriteLine(result);

            //var perm = GetPermutations(values, n).ToList();
            //var b = perm.Select(x =>
            //{
            //    var a = long.Parse(string.Join("", x));
            //    return a;
            //}).ToList();
            //Console.WriteLine(string.Join(" / ", b));
            //Console.WriteLine($"max = {b.Max(x => x)}");

            //Foo();
        }

        static void Resolve(int n, int[] values)
        {
            var result = MaximizeYourSalary(n, values);

            var fastMax = long.Parse(result);
            Console.WriteLine($"fast max = {fastMax}");
            
            var perm = GetPermutations(values, n).ToList();
            var b = perm.Select(x =>
            {
                var a = long.Parse(string.Join("", x));
                return a;
            }).ToList();
            var simpleMax = b.Max(x => x);
            Console.WriteLine($"max = {simpleMax}");

            if (fastMax != simpleMax)
            {
                throw new Exception($"{fastMax} != {simpleMax}");
            }
        }

        static void Foo()
        {
            var random = new Random();

            while(true)
            {
                var n = random.Next(1, 5);
                Console.WriteLine(n);
                var values = Enumerable.Range(0, n).Select(x =>
                {
                    return random.Next(1, 1000);
                }).Distinct().ToArray();
                Console.WriteLine(string.Join(" ", values));
                Resolve(values.Length, values);
            }
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetCombinations(list, length - 1)
                .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            var perm = GetPermutations(list, length - 1);
            var result = perm.SelectMany(t =>
                {
                    var r = list.Where(e => !t.Contains(e));
                    return r;
                }, 
                (t1, t2) => t1.Concat(new T[] { t2 }))
                .ToList();

            return result;
        }

        static string MaximizeYourSalary(int n, int[] values)
        {
            var result = new List<int>();
            var items = values.ToList();
            while (items.Any())
            {
                var indexOfMaxItem = 0;
                var maxItem = items[indexOfMaxItem];
                for (int i = 1; i < items.Count; i++)
                {
                    var item = items[i];

                    if (item == maxItem)
                    {
                        continue;
                    }

                    var comb1 = long.Parse(string.Join("", item, maxItem));
                    var comb2 = long.Parse(string.Join("", maxItem, item));
                    if (comb1 > comb2)
                    {
                        indexOfMaxItem = i;
                        maxItem = item;
                    }
                }

                //var maxItem = items[indexOfMaxItem];
                result.Add(maxItem);

                items.RemoveAt(indexOfMaxItem);
            }

            return string.Join("", result);
        }

        //static string MaximizeYourSalary(int n, int[] values)
        //{
        //    var result = new List<int>();
        //    var items = values.ToList();
        //    while(items.Any())
        //    {
        //        var indexOfMaxItem = 0;
        //        var digitsOfMaxItem = SplitToDigits(items[indexOfMaxItem]).ToList();
        //        for (int i = 1; i < items.Count; i++)
        //        {
        //            var item = items[i];

        //            if (item == items[indexOfMaxItem])
        //            {
        //                continue;
        //            }

        //            var digits = SplitToDigits(item).ToList();

        //            var withMinLength = digits.Count < digitsOfMaxItem.Count ? digits : digitsOfMaxItem;
        //            var areSomeDigitsEqual = false;
        //            var j = 0;
        //            for (; j < withMinLength.Count; j++)
        //            {
        //                var digit = digits[j];
        //                var maxItemDigit = digitsOfMaxItem[j];
        //                if (digit == maxItemDigit)
        //                {
        //                    areSomeDigitsEqual = true;
        //                    continue;
        //                }

        //                if (digit > maxItemDigit)
        //                {
        //                    indexOfMaxItem = i;
        //                    digitsOfMaxItem = digits;
        //                }

        //                areSomeDigitsEqual = false;
        //                break;
        //            }

        //            var isLengthEqual = digits.Count == digitsOfMaxItem.Count;
        //            if (!isLengthEqual && areSomeDigitsEqual)
        //            {
        //                //(var indexOfMaxLengthItem, var digitsOfItemWithMaxLength, var indexOfItemWithMinLength, var digitsOfItemWithMinLength) = 
        //                //    digits.Count > digitsOfMaxItem.Count ?
        //                //        (i, digits, indexOfMaxItem, digitsOfMaxItem) :
        //                //        (indexOfMaxItem, digitsOfMaxItem, i, digits);

        //                //for (var k = j; k < digitsOfItemWithMaxLength.Count; k++)
        //                //{ 
        //                //    if (digitsOfItemWithMaxLength[k - 1] < digitsOfItemWithMaxLength[k])
        //                //    {
        //                //        indexOfMaxItem = indexOfMaxLengthItem;
        //                //        digitsOfMaxItem = digitsOfItemWithMaxLength;
        //                //    }
        //                //    else
        //                //    {
        //                //        indexOfMaxItem = indexOfItemWithMinLength;
        //                //        digitsOfMaxItem = digitsOfItemWithMinLength;
        //                //    }
        //                //}
                        
        //                if (digits.Count > digitsOfMaxItem.Count)
        //                {
        //                    var last = digits[j - 1];
        //                    var restPart = digits.Skip(j).ToArray();
        //                    var differentInSimilarPart = last;
        //                    var differentInRestPart = last;
        //                    if (restPart.Any(x => x != last))
        //                    {
        //                        differentInRestPart = restPart.FirstOrDefault(x => x != last);
        //                    }
        //                    else
        //                    {
        //                        var similarPart = digits.Take(j).ToArray();
        //                        if (similarPart.Any(x => x != last))
        //                        {
        //                            differentInSimilarPart = similarPart.LastOrDefault(x => x != last);
        //                        }
        //                    }
                            
        //                    if (differentInRestPart > differentInSimilarPart)
        //                    {
        //                        indexOfMaxItem = i;
        //                        digitsOfMaxItem = digits;
        //                    }
        //                }
        //                else
        //                {
        //                    var last = digits[j - 1];
        //                    var restPart = digitsOfMaxItem.Skip(j).ToArray();
        //                    var differentInSimilarPart = last;
        //                    var differentInRestPart = last;
        //                    if (restPart.Any(x => x != last))
        //                    {
        //                        differentInRestPart = restPart.FirstOrDefault(x => x != last);
        //                    }
        //                    else
        //                    {
        //                        var similarPart = digitsOfMaxItem.Take(j).ToArray();
        //                        if (similarPart.Any(x => x != last))
        //                        {
        //                            differentInSimilarPart = similarPart.LastOrDefault(x => x != last);
        //                        }
        //                    }

        //                    if (differentInRestPart < differentInSimilarPart)
        //                    {
        //                        indexOfMaxItem = i;
        //                        digitsOfMaxItem = digits;
        //                    }
        //                }
        //            }
        //        }

        //        var maxItem = items[indexOfMaxItem];
        //        result.Add(maxItem);

        //        items.RemoveAt(indexOfMaxItem);
        //    }

        //    return string.Join("", result);
        //}

        static int[] SplitToDigits(int value)
        {
            var result = new Stack<int>();
            var x = value;
            while(x > 0)
            {
                var digit = x % 10;
                result.Push(digit);
                x /= 10;
            }
            return result.ToArray();
        }
    }
}
