using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.BST.IsItBST
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fContent = "0";
            //var fContent = Properties.Resources.Test1;
            //var fContent = Properties.Resources.Test2;
            //var fContent = Properties.Resources.Test3;
            //var fContent = Properties.Resources.Test4;
            //var fContent = Properties.Resources.Test5;
            //var fContent = Properties.Resources.Test6;

            List<List<int>> schemeOfNodes = null;
            using (var sr = new StringReader(fContent))
            {
                //Console.SetIn(sr);

                var n = int.Parse(Console.ReadLine());

                schemeOfNodes = Enumerable.Range(0, n)
                    .Select(x => Console.ReadLine().Split(' ').Select(y => int.Parse(y)).ToList())
                    .ToList();
            }

            var result = IsItBst(schemeOfNodes);

            Console.WriteLine(result);
        }

        private static string IsItBst(List<List<int>> schemeOfNodes)
        {
            var result = "CORRECT";
            if (!schemeOfNodes.Any())
            {
                return result;
            }

            var orderedTree = InOrder(schemeOfNodes);
            var previousItem = orderedTree.First();

            foreach (var item in orderedTree.Skip(1))
            {
                if (item < previousItem)
                {
                    result = "INCORRECT";
                    break;
                }
                previousItem = item;
            }

            return result;
        }

        static List<int> InOrder(List<List<int>> schemeOfNodes)
        {
            var result = new List<int>();
            var nextItems = new Stack<List<int>>();
            var current = schemeOfNodes.First();

            while (nextItems.Any() || current != null)
            {
                if (current != null)
                {
                    nextItems.Push(current);

                    var left = current[1];
                    if (left > -1)
                    {
                        current = schemeOfNodes[left];
                    }
                    else
                    {
                        current = null;
                    }
                }
                else
                {
                    current = nextItems.Pop();

                    var value = current[0];
                    var right = current[2];
                    result.Add(value);

                    if (right > -1)
                    {
                        current = schemeOfNodes[right];
                    }
                    else
                    {
                        current = null;
                    }
                }
            }

            return result;
        }
    }
}
