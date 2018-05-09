using Graphs.W1.Decomposition1.Reachability.Properties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.W1.Decomposition1.Reachability
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Reachability_TestCase1_Results1()
        {
            var testCase = Resources._1;
            var resultStr = new StringBuilder();
            using (var input = new StringReader(testCase))
            using (var output = new StringWriter(resultStr))
            {
                Console.SetIn(input);
                Console.SetOut(output);

                Program.Main(new string[] { });

                var result = resultStr.ToString();
                Assert.That(result.Trim() == "1");
            }
        }

        [Test]
        public void Reachability_TestCase2_Results0()
        {
            var testCase = Resources._2;
            var resultStr = new StringBuilder();
            using (var input = new StringReader(testCase))
            using (var output = new StringWriter(resultStr))
            {
                Console.SetIn(input);
                Console.SetOut(output);

                Program.Main(new string[] { });

                var result = resultStr.ToString();
                Assert.That(result.Trim() == "0");
            }
        }

        [Test]
        public void Reachability_TestCase3_Results0()
        {
            var testCase = Resources._3;
            var resultStr = new StringBuilder();
            using (var input = new StringReader(testCase))
            using (var output = new StringWriter(resultStr))
            {
                Console.SetIn(input);
                Console.SetOut(output);

                Program.Main(new string[] { });

                var result = resultStr.ToString();
                Assert.That(result.Trim() == "0");
            }
        }
    }
}
