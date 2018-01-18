using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week5.Knapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();

            var w = inputs[0];
            var n = inputs[1];

            var ws = string.Format("{0} {1}", 0, Console.ReadLine()).Split(' ').Select(x => int.Parse(x)).ToList();

            var result = Knapsack(w, ws);

            Console.WriteLine(result);
        }

        private static int Knapsack(int w, List<int> ws)
        {
            var result = new List<int>();
            var prevWs = new int[w + 1, ws.Count];
            
            Enumerable.Range(0, w + 1).ToList().ForEach(x => prevWs[x, 0] = 0);
            Enumerable.Range(0, ws.Count).ToList().ForEach(x => prevWs[0, x] = 0);

            var sw = 1;

            while (sw <= w)
            {
                var i = 1;
                while (i < ws.Count)
                {
                    var currValue = 0;
                    var wi = ws[i];
                    if (wi <= sw)
                    {
                        var prev1Value = prevWs[sw - wi, i - 1];
                        var prev2Value = prevWs[sw, i - 1];
                        currValue = Math.Max(prev1Value + wi, prev2Value);
                    }
                    else
                    {
                        currValue = prevWs[sw, i - 1];
                    }

                    prevWs[sw, i] = currValue;

                    i++;
                }

                sw++;
            }
            
            var optValue = prevWs[w, ws.Count - 1];
            return optValue;

            //var currOptKey = new Tuple<int, int>(w, ws.Count - 1);
            //while(currOptKey.Item1 > 0 && currOptKey.Item2 > 0)
            //{
            //    var prevOptKey = prevVs[currOptKey];
            //    var currOptValue = prevWs[currOptKey];
            //    var prevOptValue = prevWs[prevOptKey];
            //    if (currOptValue > prevOptValue)
            //    {
            //        var wi = ws[currOptKey.Item2];
            //        result.Add(wi);
            //    }

            //    currOptKey = prevOptKey;
            //}

            //return result;
        }

        //private static int Knapsack(int w, List<int> ws)
        //{
        //    var result = new List<int>();
        //    var prevWs = new Dictionary<Tuple<int, int>, int>();
        //    //var prevVs = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            
        //    Enumerable.Range(0, w + 1)
        //        .SelectMany(x => Enumerable.Range(0, ws.Count)
        //            .Select(y => new Tuple<int, int>(x, y)))
        //        .ToList().ForEach(z => prevWs[z] = 0);

        //    //Enumerable.Range(0, ws.Count)
        //    //    .Select(x => new Tuple<int, int>(0, x))
        //    //    .ToList().ForEach(x => prevWs[x] = 0);

        //    var sw = 1;

        //    while (sw <= w)
        //    {
        //        var i = 1;
        //        while(i < ws.Count)
        //        {
        //            //Tuple<int, int> prevKey = null;
        //            var currKey = new Tuple<int, int>(sw, i);
        //            var currValue = 0;
        //            var wi = ws[i];
        //            if (wi <= sw)
        //            {
        //                var prev1Key = new Tuple<int, int>(sw - wi, i - 1);
        //                var prev1Value = prevWs[prev1Key];
        //                var prev2Key = new Tuple<int, int>(sw, i - 1);
        //                var prev2Value = prevWs[prev2Key];
        //                currValue = Math.Max(prev1Value + wi, prev2Value);
        //                //if (currValue > prev2Value)
        //                //{
        //                //    prevKey = new Tuple<int, int>(sw - wi, i - 1);
        //                //}
        //                //else
        //                //{
        //                //    prevKey = new Tuple<int, int>(sw, i - 1);
        //                //}
        //            }
        //            else
        //            {
        //                var prev2Key = new Tuple<int, int>(sw, i - 1);
        //                int prev2Value = 0;
        //                if (prevWs.TryGetValue(prev2Key, out prev2Value))
        //                {
        //                    currValue = prev2Value;
        //                    //prevKey = prev2Key;
        //                }
        //                else
        //                {
        //                    currValue = 0;
        //                }
        //            }

        //            prevWs[currKey] = currValue;
        //            //prevVs[currKey] = prevKey;

        //            i++;
        //        }

        //        sw++;
        //    }

        //    var currOptKey = new Tuple<int, int>(w, ws.Count - 1);
        //    var optValue = prevWs[currOptKey];
        //    return optValue;

        //    //var currOptKey = new Tuple<int, int>(w, ws.Count - 1);
        //    //while(currOptKey.Item1 > 0 && currOptKey.Item2 > 0)
        //    //{
        //    //    var prevOptKey = prevVs[currOptKey];
        //    //    var currOptValue = prevWs[currOptKey];
        //    //    var prevOptValue = prevWs[prevOptKey];
        //    //    if (currOptValue > prevOptValue)
        //    //    {
        //    //        var wi = ws[currOptKey.Item2];
        //    //        result.Add(wi);
        //    //    }

        //    //    currOptKey = prevOptKey;
        //    //}

        //    //return result;
        //}
    }
}
