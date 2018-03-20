using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Root.Week3.HashTables.PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var queries = Enumerable.Range(0, n).Select(x => Console.ReadLine()).ToList();

            var result = PhoneBook(queries);

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static List<string> PhoneBook(List<string> queries)
        {
            var phoneBook = new Dictionary<string, string>();
            var result = new List<string>();

            foreach (var query in queries)
            {
                var vs = query.Split();

                var command = vs[0];

                switch (command)
                {
                    case "add":
                        {
                            var number = vs[1];
                            var name = vs[2];
                            if (phoneBook.ContainsKey(number))
                            {
                                phoneBook[number] = name;
                            }
                            else
                            {
                                phoneBook.Add(number, name);
                            }
                            break;
                        }
                    case "del":
                        {
                            var number = vs[1];
                            if (phoneBook.ContainsKey(number))
                            {
                                phoneBook.Remove(number);
                            }
                            break;
                        }
                    case "find":
                        {
                            var number = vs[1];
                            var name = "";
                            if(!phoneBook.TryGetValue(number, out name))
                            {
                                name = "not found";
                            }
                            result.Add(name);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            return result;
        }
    }
}
