using Graphs.W2.Decomposition2.Acyclicity.Properties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.W2.Decomposition2.Acyclicity
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void Acyclicity()
        {
            var cases = new List<Tuple<string, string>>
            {
                new Tuple<string, string>(Resources._1, Resources._1A),
                new Tuple<string, string>(Resources._2, Resources._2a),
                new Tuple<string, string>(Resources._3, Resources._3a)
            };

            foreach (var c in cases)
            {
                TestCase(c.Item1, c.Item2);
            }
        }

        private void TestCase(string testCase, string expected)
        {
            var resultStr = new StringBuilder();
            using (var input = new StringReader(testCase))
            using (var output = new StringWriter(resultStr))
            {
                Console.SetIn(input);
                Console.SetOut(output);

                Program.Main(new string[] { });

                var result = resultStr.ToString();
                Assert.That(string.Equals(result.Trim(), expected));
            }
        }
    }
}
