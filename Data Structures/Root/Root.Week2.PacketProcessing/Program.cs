using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week1.PacketProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            var s = inputs[0];
            var n = inputs[1];

            var packets = Enumerable.Range(0, n)
                .Select(x =>
                {
                    var pair = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
                    return new Packet { A = pair[0], P = pair[1] };
                })
                .ToList();

            var result = ProcessPackets(s, packets);

            Console.WriteLine(string.Join(Environment.NewLine, result.Select(x => x.A)));
        }

        private static IList<ProcessedPacket> ProcessPackets(int s, IEnumerable<Packet> packets)
        {
            var result = new List<ProcessedPacket>();
            var buffer = new Buffer(s);

            foreach (var packet in packets)
            {
                var processed = buffer.Add(packet);
                result.Add(processed);
            }

            return result;
        }
    }

    public class Buffer
    {
        public enum States
        {
            Empty,
            Contain,
            Full
        }

        public States State { get; private set; }
        public Deque<ProcessedPacket> Queue { get; private set; }
        public int Size { get { return Queue.Count; } }
        public int Capacity { get; set; }

        public Buffer(int capacity)
        {
            State = States.Empty;
            Queue = new Deque<ProcessedPacket>();
            Capacity = capacity;
        }

        public ProcessedPacket Add(Packet packet)
        {
            var last = Queue.Any() ? (ProcessedPacket?)Queue.Last : null;

            var currentTime = packet.A;
            ClearBuffer(currentTime);

            ProcessedPacket processed;

            if (State == States.Empty)
            {
                var finishTime = 0;
                var startTime = 0;
                if (last.HasValue)
                {
                    startTime = last.Value.F;
                    finishTime = startTime + packet.P;
                }
                else
                {
                    startTime = packet.A;
                    finishTime = startTime + packet.P;
                }

                processed = new ProcessedPacket
                {
                    Initial = packet,
                    A = startTime,
                    F = finishTime
                };
                Queue.Enqueue(processed);

                if (Size == Capacity)
                {
                    State = States.Full;
                }
                else
                {
                    State = States.Contain;
                }
            }
            else if (State == States.Contain)
            {
                var startTime = Queue.Last.F;
                processed = new ProcessedPacket
                {
                    Initial = packet,
                    A = startTime,
                    F = startTime + packet.P
                };
                Queue.Enqueue(processed);

                if (Size == Capacity)
                {
                    State = States.Full;
                }
            }
            else if (State == States.Full)
            {
                processed = new ProcessedPacket
                {
                    Initial = packet,
                    A = -1,
                    F = -1
                };
            } 
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            return processed;
        }

        private void ClearBuffer(int currentTime)
        {
            if (State == States.Empty)
            {
                return;
            }

            var first = Queue.Peek();

            while (first.F <= currentTime)
            {
                var item = Queue.Dequeue();
                if (!Queue.Any())
                {
                    break;
                }
                first = Queue.Peek();
            }

            if (Size == 0)
            {
                State = States.Empty;
            }
            else if (Size < Capacity)
            {
                State = States.Contain;
            }
            else if (Size == Capacity)
            {
                State = States.Full;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class Deque<T> : IEnumerable<T>
    {
        private Queue<T> _inner = new Queue<T>();

        public T Last { get; private set; }

        public int Count { get { return _inner.Count; } }

        public void Enqueue(T item)
        {
            _inner.Enqueue(item);
            Last = item;
        }

        public T Dequeue()
        {
            var item = _inner.Dequeue();
            if (_inner.Count == 0)
            {
                Last = default(T);
            }
            return item;
        }

        public T Peek()
        {
            var item = _inner.Peek();
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }

    public struct Packet
    {
        public int A { get; set; }
        public int P { get; set; }
    }

    public struct ProcessedPacket
    {
        public Packet Initial { get; set; }
        public int A { get; set; }
        public int F { get; set; }
    }
}
