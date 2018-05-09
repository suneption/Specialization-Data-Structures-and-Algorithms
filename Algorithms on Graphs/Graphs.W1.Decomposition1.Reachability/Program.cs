using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.W1.Decomposition1.Reachability
{
    class Program
    {
        public static void Main(string[] args)
        {
            var nm = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var n = int.Parse(nm[0]);
            var m = int.Parse(nm[1]);

            var scheme = Enumerable.Range(0, m)
                .Select(x => 
                    Console.ReadLine()
                        .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(y => int.Parse(y)).ToList())
                .ToList();

            var startEnd = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var start = int.Parse(startEnd[0]);
            var end = int.Parse(startEnd[1]);

            var result = Path(scheme, start, end);

            Console.WriteLine(result);
        }

        public static int Path(List<List<int>> scheme, int start, int end)
        {
            var nodes = new Dictionary<int, Node>();
            foreach (var edge in scheme)
            {
                var source = edge[0];
                var target = edge[1];

                Node sourceNode = null;
                if (!nodes.TryGetValue(source, out sourceNode))
                {
                    sourceNode = new Node { Value = source, Refs = new List<Node>() };
                    nodes.Add(source, sourceNode);
                }

                Node targetNode = null;
                if (!nodes.TryGetValue(target, out targetNode))
                {
                    targetNode = new Node { Value = target, Refs = new List<Node>() };
                    nodes.Add(target, targetNode);
                }

                Contract.Assert(!sourceNode.Refs.Any(x => x.Value == targetNode.Value));
                sourceNode.Refs.Add(targetNode);

                Contract.Assert(!targetNode.Refs.Any(x => x.Value == sourceNode.Value));
                targetNode.Refs.Add(sourceNode);
            }

            var result = 0;
            if (!nodes.ContainsKey(start))
            {
                return result;
            }

            var startNode = nodes[start];
            startNode.IsVisited = true;
            var nextNodes = new Stack<Node>();
            nextNodes.Push(startNode);
            while (nextNodes.Any())
            {
                var current = nextNodes.Pop();

                if (current.Value == end)
                {
                    result = 1;
                    break;
                }

                current.IsVisited = true;

                var notVisited = current.Refs.Where(x => !x.IsVisited).ToList();
                notVisited.ForEach(x => nextNodes.Push(x));
            }

            return result;
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public List<Node> Refs { get; set; }
        public bool IsVisited { get; set; }
    }
}
