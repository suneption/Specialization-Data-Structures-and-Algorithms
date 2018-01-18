using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.ChangingMoney
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var m = int.Parse(input);

            var result = ChangingMoney(m);

            Console.WriteLine(result);
        }

        static int ChangingMoney(int m)
        {
            var coins = new List<int> { 10, 5, 1 };
            var maxCoin = coins.Max();

            var rest = m;
            var resultCoins = new List<int>();
            while (rest > 0)
            {
                var coin = coins.First(x => x <= rest);
                resultCoins.Add(coin);
                rest -= coin;
            }

            return resultCoins.Count;
        }
    }
}
