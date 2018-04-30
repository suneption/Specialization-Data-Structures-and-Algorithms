using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week4.BST.IsItBSTHard
{
    class Program
    {
        private const string CORRECT = "CORRECT";
        private const string INCORRECT = "INCORRECT";

        static void Main(string[] args)
        {
            //var props = typeof(Properties.Resources).GetProperties();
            //var cases = props
            //    .Where(x => x.Name.StartsWith("_"))
            //    .Select(x => (string)x.GetValue(null, null))
            //    .ToList();

            //var vs = new Dictionary<string, string>
            //{
            //    { Properties.Resources._1, CORRECT },
            //    { Properties.Resources._2, INCORRECT },
            //    { Properties.Resources._3, CORRECT }
            //};

            var fContent = @"";
            //var fContent = cases.First();
            //var fContent = Properties.Resources._1;
            //var fContent = Properties.Resources._2;
            //var fContent = Properties.Resources._3;
            //var fContent = Properties.Resources._4;
            //var fContent = Properties.Resources._5;
            //var fContent = Properties.Resources._6;
            //var fContent = Properties.Resources._7;
            //var fContent = Properties.Resources._8;

            List<List<int>> schemeOfNodes = null;
            using (var sr = new StringReader(fContent))
            {
                if (!string.IsNullOrEmpty(fContent))
                {
                    Console.SetIn(sr);
                }

                var n = int.Parse(Console.ReadLine());

                schemeOfNodes = Enumerable.Range(0, n)
                    .Select(x => Console.ReadLine().Split(' ').Select(y => int.Parse(y)).ToList())
                    .ToList();
            }

            var result = IsBst(schemeOfNodes);

            Console.WriteLine(result);
        }

        private static string IsBst(List<List<int>> nodesScheme)
        {
            var result = CORRECT;
            if (!nodesScheme.Any())
            {
                return result;
            }

            var root = ToTree(nodesScheme);
            var current = root;
            var inOrder = new List<Node>();

            while (current != null)
            {
                if (current.Left != null && !current.Left.IsVisited.HasValue)
                {
                    current = current.Left;
                    continue;
                }

                if (!current.IsVisited.HasValue)
                {
                    inOrder.Add(current);
                    current.IsVisited = true;
                }

                if (current.Right != null && !current.Right.IsVisited.HasValue)
                {
                    current = current.Right;
                    continue;
                }
                
                current = current.Parent;
            }

            var previousNode = inOrder.First();
            foreach (var node in inOrder.Skip(1))
            {
                if (node.Value < previousNode.Value)
                {
                    result = INCORRECT;
                    break;
                }

                if (node.Value == previousNode.Value)
                {
                    if (previousNode.Level > node.Level)
                    {
                        result = INCORRECT;
                        break;
                    }
                }

                previousNode = node;
            }

            return result;
        }

        enum InOrderStates
        {
            Start = 0,
            Left,
            Right,
            Root,
            End
        }

        private static List<Node> InOrder(Node root)
        {
            var state = InOrderStates.Start;
            var current = root;
            var inOrder = new List<Node>();

            while (state != InOrderStates.End)
            {
                switch (state)
                {
                    case InOrderStates.Start:
                        
                    case InOrderStates.Left:
                        break;
                    case InOrderStates.Right:
                        break;
                    case InOrderStates.End:
                        break;
                    default:
                        break;
                }
            }

            while (current != null)
            {
                if (current.Left != null && !current.Left.IsVisited.HasValue)
                {
                    current = current.Left;
                    continue;
                }

                if (!current.IsVisited.HasValue)
                {
                    inOrder.Add(current);
                    current.IsVisited = true;
                }

                if (current.Right != null && !current.Right.IsVisited.HasValue)
                {
                    current = current.Right;
                    continue;
                }

                current = current.Parent;
            }

            return inOrder;
        }

        //private static List<Node> InOrder(Node root)
        //{
        //    var current = root;
        //    var inOrder = new List<Node>();

        //    while (current != null)
        //    {
        //        if (current.Left != null && !current.Left.IsVisited.HasValue)
        //        {
        //            current = current.Left;
        //            continue;
        //        }

        //        if (!current.IsVisited.HasValue)
        //        {
        //            inOrder.Add(current);
        //            current.IsVisited = true;
        //        }

        //        if (current.Right != null && !current.Right.IsVisited.HasValue)
        //        {
        //            current = current.Right;
        //            continue;
        //        }

        //        current = current.Parent;
        //    }

        //    return inOrder;
        //}

        private static Node ToTree(List<List<int>> nodesScheme)
        {
            var nodes = Enumerable.Range(0, nodesScheme.Count).Select(x => (Node)null).ToList();

            for (var i = 0; i < nodesScheme.Count; i++)
            {
                var nodeScheme = nodesScheme[i];
                var value = nodeScheme[0];
                var node = new Node { Value = value };
                nodes[i] = node;
            }

            for (var i = 0; i < nodesScheme.Count; i++)
            {
                var nodeScheme = nodesScheme[i];
                var left = nodeScheme[1];
                var right = nodeScheme[2];
                var node = nodes[i];

                if (left > -1)
                {
                    var leftNode = nodes[left];
                    node.Left = leftNode;
                    leftNode.Parent = node;
                }

                if (right > -1)
                {
                    var rightNode = nodes[right];
                    node.Right = rightNode;
                    rightNode.Parent = node;
                }
            }

            var root = nodes[0];
            SetLevel(root);
                        
            return root;
        }

        private static void SetLevel(Node root)
        {
            var currentLevelItems = new Queue<Node>();
            var nextLevelItems = new Queue<Node>();
            currentLevelItems.Enqueue(root);
            var level = 0;
            while (currentLevelItems.Any())
            {
                var currentNode = currentLevelItems.Dequeue();
                currentNode.Level = level;
                if (currentNode.Left != null)
                {
                    nextLevelItems.Enqueue(currentNode.Left);
                }

                if (currentNode.Right != null)
                {
                    nextLevelItems.Enqueue(currentNode.Right);
                }

                if (!currentLevelItems.Any())
                {
                    level++;
                    currentLevelItems = nextLevelItems;
                    nextLevelItems = new Queue<Node>();
                }
            }
        }
    }

    internal class Node
    {
        public int Value { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Level { get; set; }
        public bool? IsVisited { get; set; }
    }
}
