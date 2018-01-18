using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.ComputeTreeHeight
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var vs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var result = ComputeTreeHeight(n, vs);

            Console.WriteLine(result);
        }

        private static int ComputeTreeHeight(int n, List<int> vs)
        {
            var root = CreateTree(n, vs);

            var height = 1;
            var stack = new Stack<Node>();
            stack.Push(root);
            
            while (stack.Any())
            {
                var curr = stack.Pop();

                if (curr.Children.Any())
                {
                    height++;
                    curr.Children.ForEach(x => stack.Push(x));
                }
            }

            return height;
        }

        private static Node CreateTree(int n, List<int> vs)
        {
            var nodes = Enumerable.Range(0, n).Select(x => new Node { Value = x }).ToList();

            foreach (var node in nodes)
            {
                var parentIndex = vs[node.Value];

                if (parentIndex == -1)
                {
                    continue;
                }

                var parent = nodes[parentIndex];
                node.Parent = parent;
                parent.Children.Add(node);
            }

            var rootIndex = vs.IndexOf(-1);
            var root = nodes[rootIndex];

            return root;
        }

        public class Node
        {
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }
            public int Value { get; set; }

            public Node()
            {
                Children = new List<Node>();
            }
        }
    }
}
