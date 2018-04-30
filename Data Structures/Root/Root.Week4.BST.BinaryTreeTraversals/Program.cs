using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.BST.BinaryTreeTraversals
{
    class Program
    {
        static void Main(string[] args)
        {
            //var fContent = Properties.Resources.Test1;
            //var fContent = Properties.Resources.Test2;

            List<List<int>> schemeOfNodes = null;
            //using (var sr = new StringReader(fContent))
            {
                //Console.SetIn(sr);

                var n = int.Parse(Console.ReadLine());

                schemeOfNodes = Enumerable.Range(0, n)
                    .Select(x => Console.ReadLine().Split(' ').Select(y => int.Parse(y)).ToList())
                    .ToList();
            }

            //var postOrder = PostOrderSecond(schemeOfNodes);
            //var result = string.Join(" ", postOrder);

            var inOrder = InOrder(schemeOfNodes);
            var preOrder = PreOrder(schemeOfNodes);
            var postOrder = PostOrderSecond(schemeOfNodes);

            var result = string.Concat(
                string.Join(" ", inOrder),
                Environment.NewLine,
                string.Join(" ", preOrder),
                Environment.NewLine,
                string.Join(" ", postOrder));

            Console.WriteLine(result);
        }

        static List<int> InOrder(List<List<int>> nodesDescription)
        {
            var result = new List<int>();
            var nextItems = new Stack<List<int>>();
            var current = nodesDescription.First();

            while (nextItems.Any() || current != null)
            {
                if (current != null)
                {
                    nextItems.Push(current);

                    var left = current[1];
                    if (left > -1)
                    {
                        current = nodesDescription[left];
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
                        current = nodesDescription[right];
                    }
                    else
                    {
                        current = null;
                    }
                }
            }

            return result;
        }

        static List<int> PostOrder(List<List<int>> nodesDescription)
        {
            var result = new List<int>();
            var nextItems = new Stack<List<int>>();
            var current = nodesDescription.First();
            List<int> lastVisitedNode = null;

            while (nextItems.Any() || current != null)
            {
                if (current != null)
                {
                    nextItems.Push(current);

                    var left = current[1];
                    if (left > -1)
                    {
                        current = nodesDescription[left];
                    }
                    else
                    {
                        current = null;
                    }
                }
                else
                {
                    var peekNode = nextItems.Peek();

                    var value = peekNode[0];
                    var right = peekNode[2];

                    List<int> rightNode = null;
                    if (right > -1)
                    {
                        rightNode = nodesDescription[right];
                    }

                    if (rightNode != null && rightNode != lastVisitedNode)
                    {
                        current = rightNode;
                    }
                    else
                    {
                        result.Add(value);
                        lastVisitedNode = nextItems.Pop();
                    }
                }
            }

            return result;
        }

        static List<int> PreOrder(List<List<int>> nodesDescription)
        {
            var result = new List<int>();
            var nextItems = new Stack<List<int>>();
            nextItems.Push(nodesDescription.First());

            while (nextItems.Any())
            {
                var current = nextItems.Pop();

                var value = current[0];
                var left = current[1];
                var right = current[2];

                result.Add(value);

                if (right > -1)
                {
                    nextItems.Push(nodesDescription[right]);
                }
                if (left > -1)
                {
                    nextItems.Push(nodesDescription[left]);
                }
            }

            return result;
        }
        
        static List<int> PostOrderSecond(List<List<int>> nodesDescription)
        {
            var result = new List<int>();
            var nextItems = new Stack<List<int>>();
            var processedItems = new Stack<List<int>>();
            nextItems.Push(nodesDescription.First());

            while (nextItems.Any())
            {
                var current = nextItems.Pop();

                var value = current[0];
                var left = current[1];
                var right = current[2];

                if (left > -1)
                {
                    var leftNode = nodesDescription[left];
                    nextItems.Push(leftNode);
                }

                if (right > -1)
                {
                    var rightNode = nodesDescription[right];
                    nextItems.Push(rightNode);
                }

                processedItems.Push(current);
            }

            return processedItems.Select(x => x[0]).ToList();
        }
    }
}
