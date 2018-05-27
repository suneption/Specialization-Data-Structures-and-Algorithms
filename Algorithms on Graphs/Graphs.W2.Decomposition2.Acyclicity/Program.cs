﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.W2.Decomposition2.Acyclicity
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

            var digraph = new Digraph(n, m, scheme);
            var result = HasCycle(digraph);

            Console.WriteLine(result ? 1 : 0);
        }

        private static bool HasCycle(Digraph digraph)
        {
            var numbers = new List<int[]>();
            Enumerable.Range(0, digraph.VsCount + 1).ToList().ForEach(x => numbers.Add(new int [2]));
            var last = 1;

            var vertecis = digraph.Vertecis()
                .Select(x => new ExtendedNode { Inner = x })
                .ToDictionary(x => x.Inner.Id);

            foreach (var vertex in vertecis.Values)
            {
                if (vertex.IsVisited)
                {
                    continue;
                }

                var nextVertices = new Stack<ExtendedNode>();
                nextVertices.Push(vertex);

                while (nextVertices.Any())
                {
                    var current = nextVertices.Peek();

                    if (!current.IsVisited)
                    {
                        current.Pre = last;
                    }
                    else
                    {
                        current.Post = last;
                    }
                    last++;

                    var adjs = digraph.Adj(current.Inner.Id).Select(x => vertecis[x.Id]).ToList();
                    var visitedAdjs = adjs.Where(x => x.IsVisited).ToList();
                    if (visitedAdjs.Any(x => x.IsPartiallyVisited))
                    {
                        return true;
                    }

                    var notVisitedAdjs = adjs.Where(x => !x.IsVisited).ToList();

                    if (notVisitedAdjs.Any())
                    {
                        notVisitedAdjs.ForEach(x => nextVertices.Push(x));
                    }
                    else
                    {
                        current.Post = last;
                        last++;
                        nextVertices.Pop();
                    }
                }
            }
            
            return false;
        }
    }

    class Digraph
    {
        private readonly List<Node> _adjacencyLists;

        public Digraph(int vsCount, int esCount)
        {
            VsCount = vsCount;
            EsCount = esCount;

            _adjacencyLists = new List<Node>(vsCount);
            Enumerable.Range(0, vsCount + 1).ToList()
                .ForEach(x => _adjacencyLists.Add(new Node(x)));
        }

        public Digraph(int vsCount, int esCount, List<List<int>> scheme)
            : this(vsCount, esCount)
        {
            foreach (var edge in scheme)
            {
                var head = edge[0];
                var tail = edge[1];

                AddEdge(head, tail);
            }
        }
        
        public int VsCount { get; private set; }
        public int EsCount { get; private set; }

        public IEnumerable<Node> Vertecis()
        {
            return _adjacencyLists.Skip(1);
        }

        public IReadOnlyCollection<Node> Adj(int v)
        {
            return _adjacencyLists[v].Adjacencies;
        }

        private void AddEdge(int head, int tail)
        {
            var headNode = _adjacencyLists[head];
            var tailNode = _adjacencyLists[tail];

            headNode.Add(tailNode);
        }
    }

    class Node
    {
        private readonly List<Node> _adjacencies;

        public int Id { get; private set; }
        public IReadOnlyList<Node> Adjacencies { get { return _adjacencies; } }

        public Node(int id)
            : this(id, new List<Node>())
        { }
        
        public Node(int id, List<Node> adjacencies)
        {
            Id = id;
            _adjacencies = adjacencies;
        }

        public void Add(Node adjacency)
        {
            _adjacencies.Add(adjacency);
        }
    }

    class ExtendedNode
    {
        public Node Inner { get; set; }
        public int Pre { get; set; }
        public int Post { get; set; }

        public bool IsVisited { get { return Pre > 0; } }
        public bool IsPartiallyVisited { get { return Pre > 0 && Post == 0; } }
    }
}
