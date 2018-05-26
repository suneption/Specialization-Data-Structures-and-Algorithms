using System;
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
            var visited = new HashSet<int>();
            var numbers = new List<int[]>();
            Enumerable.Range(1, digraph.VsCount).ToList().ForEach(x => numbers.Add(new int [2]));
            var last = 1;

            foreach (var vertex in digraph.Vertecis())
            {
                if (visited.Contains(vertex.Id))
                {
                    continue;
                }

                var nextVertices = new Stack<Node>();
                nextVertices.Push(vertex);
                Node current = null;

                while (nextVertices.Any())
                {
                    if (current != null)
                    {
                        var next = nextVertices.Peek();
                        if (current.Parent == next.Parent)
                        {
                            current = nextVertices.Pop();
                        }
                        else
                        {
                            if (current.Parent == null)
                            {
                                break;
                            }

                            current = current.Parent;
                        }
                    }
                    else
                    {
                        current = nextVertices.Pop();
                    }

                    var prePost = numbers[current.Id];
                    var pre = prePost[0];
                    var post = prePost[1];

                    if (pre == 0)
                    {
                        pre = last;
                    }
                    else
                    {
                        post = last;
                    }
                    numbers[current.Id] = new int[] { pre, post };
                    last++;

                    visited.Add(current.Id);

                    var adjs = digraph.Adj(current.Id);
                    var visitedAdjs = adjs.Where(x => visited.Contains(x.Id)).ToList();
                    if (visitedAdjs.Any())
                    {
                        foreach (var visitedNode in visitedAdjs)
                        {
                            prePost = numbers[visitedNode.Id];
                            pre = prePost[0];
                            post = prePost[1];
                            if (pre > 0 && post == 0)
                            {
                                return true;
                            }
                        }
                    }

                    var notVisitedAdjs = digraph.Adj(current.Id);
                    foreach (var notVisitedAdj in notVisitedAdjs)
                    {
                        nextVertices.Push(notVisitedAdj);
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
                .ForEach(x => _adjacencyLists.Add(null));
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
        
        public int VsCount { get; }
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
            if (headNode == null)
            {
                headNode = new Node(head);
                _adjacencyLists[head] = headNode;
            }

            var tailNode = _adjacencyLists[tail];
            if (tailNode == null)
            {
                tailNode = new Node(tail, headNode);
                _adjacencyLists[tail] = tailNode;
            }
            else
            {
                if (tailNode.Parent == null)
                {
                    tailNode.SetParent(headNode);
                }
            }

            headNode.Add(tailNode);
        }
    }

    class Node
    {
        private readonly List<Node> _adjacencies;

        public int Id { get; }
        public Node Parent { get; private set; }
        public IReadOnlyList<Node> Adjacencies { get { return _adjacencies; } }

        public Node(int id)
            : this(id, null, new List<Node>())
        { }

        public Node(int id, Node parent)
            : this(id, parent, new List<Node>())
        { }
        
        public Node(int id, Node parent, List<Node> adjacencies)
        {
            Id = id;
            Parent = parent;
            _adjacencies = adjacencies;
        }

        public void Add(Node adjacency)
        {
            _adjacencies.Add(adjacency);
        }

        public void SetParent(Node parent)
        {
            if (Parent != null)
            {
                throw new InvalidOperationException();
            }

            Parent = parent;
        }
    }
}
