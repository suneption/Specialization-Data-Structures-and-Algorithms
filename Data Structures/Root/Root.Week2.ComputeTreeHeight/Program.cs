﻿using System;
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

            if (root == null)
            {
                return 0;
            }

            var height = 1;
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            var nextLevelQueue = new Queue<Node>();
            
            while (queue.Any())
            {
                var curr = queue.Dequeue();

                if (curr.Children.Any())
                {
                    curr.Children.ForEach(x => nextLevelQueue.Enqueue(x));
                }

                if (!queue.Any() && nextLevelQueue.Any())
                {
                    queue = nextLevelQueue;
                    nextLevelQueue = new Queue<Node>();
                    height++;
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
