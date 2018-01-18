using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.MaximizingTheValueOfLoot
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputs = input.Split(' ').ToArray();
            var n = int.Parse(inputs[0]);
            var W = int.Parse(inputs[1]);
            var items = Enumerable.Range(0, n).Select(x =>
            {
                var iInput = Console.ReadLine();
                var iInputs = iInput.Split(' ').ToArray();
                var v = int.Parse(iInputs[0]);
                var w = int.Parse(iInputs[1]);
                return new Item(v, w);
            }).ToArray();

            var result = MaximizingTheValueOfLoot(n, W, items);

            Console.WriteLine(result);
        }

        static double MaximizingTheValueOfLoot(int n, int W, Item[] items)
        {
            var result = 0.0;
            var orderedItems = new Stack<Item>(items.OrderBy(x => x.Utility));
            var restW = W;

            while (restW > 0)
            {
                if (!orderedItems.Any())
                {
                    break;
                }

                var item = orderedItems.Pop();

                var partOfItem = Math.Min(restW, item.W);
                restW -= partOfItem;

                result += partOfItem * item.Utility;
            }
            
            return result;
        }

        public class Item
        {
            private readonly double _utility;

            public int V { get; }
            public int W { get; }
            public double Utility => _utility;

            public Item(int v, int w)
            {
                V = v;
                W = w;
                _utility = (double)V / W;
            }
        }
    }
}
