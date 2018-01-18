using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week1.CheckBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var result = CheckBrackets(input);

            Console.WriteLine(result == null ? "Success" : (result.Item2 + 1).ToString());
        }

        private static Tuple<char, int> CheckBrackets(string input)
        {
            Tuple<char, int> result = null;
            var opening = new char[] { '(', '{', '[' }.ToList();
            var closing = new char[] { ')', '}', ']' }.ToList();

            var stack = new Stack<Tuple<char, int>>();
            for (var i = 0; i < input.Length; i++)
            {
                var ch = input[i];
                if (opening.Contains(ch))
                {
                    stack.Push(new Tuple<char, int>(ch, i));
                }
                else if (closing.Contains(ch))
                {
                    if (!stack.Any())
                    {
                        result = new Tuple<char, int>(ch, i);
                        break;
                    }

                    var pair = stack.Pop();
                    var prev = pair.Item1;
                    var chOpeningIndex = closing.FindIndex(x => x == ch);
                    var chOpening = opening[chOpeningIndex];
                    if (prev != chOpening)
                    {
                        result = new Tuple<char, int>(ch, i);
                        break;
                    }
                }
            }

            if (result == null && stack.Any())
            {
                result = stack.Pop();
            }

            return result;
        }
    }
}
