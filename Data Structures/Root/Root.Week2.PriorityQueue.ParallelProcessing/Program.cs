using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week2.PriorityQueue.ParallelProcessing
{
    class Program
    {
        private static string run_cmd(string cmd, string[] args)
        {
            var pythonInterpreter = @"C:\Program Files\Python36\python.exe";
            var start = new ProcessStartInfo()
            {
                FileName = pythonInterpreter
            };
            var arguments = string.Format("{0} {1}", cmd, args);
            start.Arguments = string.Format(arguments);
            start.UseShellExecute = false;
            start.RedirectStandardInput = true;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (var writer = process.StandardInput)
                {
                    foreach (var arg in args)
                    {
                        writer.WriteLine(arg);
                    }
                }

                using (StreamReader reader = process.StandardOutput)
                {
                    
                    string result = reader.ReadToEnd();
                    return result;
                }
            }

            throw new Exception();
        }

        static void Main(string[] args)
        {
            //Tests();

            var nMstr = Console.ReadLine();
            var nM = nMstr.Split(' ').Select(x => int.Parse(x)).ToList();

            var n = nM[0];
            var m = nM[1];

            var jobsStr = Console.ReadLine();
            var jobs = jobsStr.Split(' ').Select(x => int.Parse(x)).ToList();

            //var pythonScript = @"E:\module1.py";
            //var inputs = new[] { nMstr, jobsStr };
            //var pythonFastSolution = run_cmd(pythonScript, inputs);
            //Console.WriteLine(pythonFastSolution);
            var result = FastSolution(n, jobs);
            //var simple = SimpleSolution(n, jobs);
            //for (int i = 0; i < simple.Count; i++)
            //{
            //    Contract.Assert(result[i].Item1 == simple[i].Item1);
            //    Contract.Assert(result[i].Item2 == simple[i].Item2);
            //}
            
            Console.WriteLine(string.Join(Environment.NewLine, result.Select(x => x.Item1 + " " + x.Item2)));
        }

        private static void Tests()
        {
            var rand = new Random();
            while(true)
            {
                var n = rand.Next(1, 1);
                var m = rand.Next(1, 10);

                var jobs = Enumerable.Range(0, m).Select(x => rand.Next(1000000000)).ToList();
                var fastResult = FastSolution(n, jobs);
                var fastResultStr = string.Join(Environment.NewLine, fastResult.Select(x => x.Item1 + " " + x.Item2)) + Environment.NewLine;
                var pythonResultStr = CallPythonFastSolution(n + " " + m, string.Join(" ", jobs));
                Console.WriteLine($"{n} {m}{Environment.NewLine}{string.Join(" ", jobs)}");
                Contract.Assert(fastResultStr == pythonResultStr);
                Console.WriteLine("solutions coincide");
            }
        }

        private static void Test(int n, List<int> jobs)
        {
            while (true)
            {
                var result = FastSolution(n, jobs);
                var simple = SimpleSolution(n, jobs);
                for (int i = 0; i < simple.Count; i++)
                {
                    Contract.Assert(result[i].Item1 == simple[i].Item1);
                    Contract.Assert(result[i].Item2 == simple[i].Item2);
                }
            }
        }

        private static string CallPythonFastSolution(string nMstr, string jobsStr)
        {
            var pythonScript = @"E:\module1.py";
            var inputs = new[] { nMstr, jobsStr };
            var pythonFastSolution = run_cmd(pythonScript, inputs);
            return pythonFastSolution;
        }

        private static List<Tuple<int, long>> FastSolution(int n, List<int> jobDurations)
        {
            //var availableThreads = new IntervalHeap<int>(n);
            //availableThreads.AddAll(Enumerable.Range(0, n).ToList());

            var availableThreads = new MinHeap<int>(Enumerable.Range(0, n).ToList());

            //var busyThreads = new IntervalHeap<Tuple<int, int>>(n, new ThreadByNearestTime());
            var busyThreads = new MinHeap<Tuple<int, long>>(new ThreadByNearestTime());

            var result = new List<Tuple<int, long>>();
            long currentTime = 0;
            foreach (var jobDuration in jobDurations)
            {
                if (!availableThreads.Any())
                {
                    var busyThread = busyThreads.ExtractDominating();
                    var busyThreadId = busyThread.Item1;
                    var releaseTime = busyThread.Item2;
                    currentTime = releaseTime;
                    availableThreads.Add(busyThreadId);
                }

                Contract.Assert(!availableThreads.IsEmpty);

                var endTime = checked(currentTime + jobDuration);
                var thread = 0;
                if (jobDuration > 0)
                {
                    thread = availableThreads.ExtractDominating();
                    busyThreads.Add(new Tuple<int, long>(thread, endTime));
                }
                else
                {
                    thread = availableThreads.GetMin();
                }
                
                result.Add(new Tuple<int, long>(thread, currentTime));
            }

            return result;
        }

        private static List<Tuple<int, int>> SimpleSolution(int n, List<int> jobDurations)
        {
            var assignedWorkers = Enumerable.Range(0, jobDurations.Count).Select(x => (int?)null).ToList();
            var startTimes = Enumerable.Range(0, jobDurations.Count).Select(x => (int?)null).ToList();
            var nextFreeTime = Enumerable.Range(0, n).Select(x => 0).ToList();
            for (int i = 0; i < jobDurations.Count; i++)
            {
                var nextWorker = 0;
                for (int j = 0; j < n; j++)
                {
                    if (nextFreeTime[j] < nextFreeTime[nextWorker])
                    {
                        nextWorker = j;
                    }
                }

                assignedWorkers[i] = nextWorker;
                startTimes[i] = nextFreeTime[nextWorker];
                nextFreeTime[nextWorker] += jobDurations[i];
            }

            var result = new List<Tuple<int, int>>();
            for (int i = 0; i < jobDurations.Count; i++)
            {
                result.Add(new Tuple<int, int>(assignedWorkers[i] ?? int.MaxValue, startTimes[i] ?? int.MaxValue));
            }

            return result;
        }

        private class ThreadByNearestTime : Comparer<Tuple<int, long>>, IComparer<Tuple<int, long>>
        {
            public override int Compare(Tuple<int, long> x, Tuple<int, long> y)
            {
                var compareByTime = x.Item2.CompareTo(y.Item2);
                if (compareByTime != 0)
                {
                    return compareByTime;
                }
                return x.Item1.CompareTo(y.Item1);
            }
        }
    }

    public abstract class Heap<T> : IEnumerable<T>
    {
        private const int InitialCapacity = 0;
        private const int GrowFactor = 2;
        private const int MinGrow = 1;

        private int _capacity = InitialCapacity;
        private T[] _heap = new T[InitialCapacity];
        private int _tail = 0;

        public int Count { get { return _tail; } }
        public int Capacity { get { return _capacity; } }

        protected Comparer<T> Comparer { get; private set; }
        public bool IsEmpty { get { return Count == 0; } }
        protected abstract bool Dominates(T x, T y);

        protected Heap() : this(Comparer<T>.Default)
        {
        }

        protected Heap(Comparer<T> comparer) : this(Enumerable.Empty<T>(), comparer)
        {
        }

        protected Heap(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (comparer == null) throw new ArgumentNullException("comparer");

            Comparer = comparer;

            foreach (var item in collection)
            {
                if (Count == Capacity)
                    Grow();

                _heap[_tail++] = item;
            }

            for (int i = Parent(_tail - 1); i >= 0; i--)
                BubbleDown(i);
        }

        public void Add(T item)
        {
            if (Count == Capacity)
                Grow();

            _heap[_tail++] = item;
            BubbleUp(_tail - 1);
        }

        private void BubbleUp(int i)
        {
            if (i == 0 || Dominates(_heap[Parent(i)], _heap[i]))
                return; //correct domination (or root)

            Swap(i, Parent(i));
            BubbleUp(Parent(i));
        }

        public T GetMin()
        {
            if (Count == 0) throw new InvalidOperationException("Heap is empty");
            return _heap[0];
        }

        public T ExtractDominating()
        {
            if (Count == 0) throw new InvalidOperationException("Heap is empty");
            T ret = _heap[0];
            _tail--;
            Swap(_tail, 0);
            BubbleDown(0);
            return ret;
        }

        private void BubbleDown(int i)
        {
            int dominatingNode = Dominating(i);
            if (dominatingNode == i) return;
            Swap(i, dominatingNode);
            BubbleDown(dominatingNode);
        }

        private int Dominating(int i)
        {
            int dominatingNode = i;
            dominatingNode = GetDominating(YoungChild(i), dominatingNode);
            dominatingNode = GetDominating(OldChild(i), dominatingNode);

            return dominatingNode;
        }

        private int GetDominating(int newNode, int dominatingNode)
        {
            if (newNode < _tail && !Dominates(_heap[dominatingNode], _heap[newNode]))
                return newNode;
            else
                return dominatingNode;
        }

        private void Swap(int i, int j)
        {
            T tmp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = tmp;
        }

        private static int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        private static int YoungChild(int i)
        {
            return (i + 1) * 2 - 1;
        }

        private static int OldChild(int i)
        {
            return YoungChild(i) + 1;
        }

        private void Grow()
        {
            int newCapacity = _capacity * GrowFactor + MinGrow;
            var newHeap = new T[newCapacity];
            Array.Copy(_heap, newHeap, _capacity);
            _heap = newHeap;
            _capacity = newCapacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heap.Take(Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MaxHeap<T> : Heap<T>
    {
        public MaxHeap()
            : this(Comparer<T>.Default)
        {
        }

        public MaxHeap(Comparer<T> comparer)
            : base(comparer)
        {
        }

        public MaxHeap(IEnumerable<T> collection, Comparer<T> comparer)
            : base(collection, comparer)
        {
        }

        public MaxHeap(IEnumerable<T> collection) : base(collection)
        {
        }

        protected override bool Dominates(T x, T y)
        {
            return Comparer.Compare(x, y) >= 0;
        }
    }

    public class MinHeap<T> : Heap<T>
    {
        public MinHeap()
            : this(Comparer<T>.Default)
        {
        }

        public MinHeap(Comparer<T> comparer)
            : base(comparer)
        {
        }

        public MinHeap(IEnumerable<T> collection) : base(collection)
        {
        }

        public MinHeap(IEnumerable<T> collection, Comparer<T> comparer)
            : base(collection, comparer)
        {
        }

        protected override bool Dominates(T x, T y)
        {
            return Comparer.Compare(x, y) <= 0;
        }
    }
}
