using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LINQtoXMLpractice
{
    static class RunLinqExample
    {
        public static void Run()
        {
            Console.WriteLine("LINQ examples:");
            string[] names = { "Momi", "Baby", "Puppy", "Paipai", "Caiji", "Kurosaki", "Hanano", "Smith" };

            Console.Write("Example string array:");
            foreach (string n in names)
            {
                Console.Write(n);
                Console.Write(", ");
            }
            Console.WriteLine();
            Console.WriteLine("Fluent Syntax - Length more than 4");
            IEnumerable<string> filteredNames =
                Enumerable.Where(names, n => n.Length > 4);
            foreach (string n in filteredNames)
            {
                Console.WriteLine(n);
            }


            Console.WriteLine("Fluent Syntax - Element Contains \"a\"");
            IEnumerable<string> filteredNamesSimple = names.Where(n => n.Contains("a"));
            foreach (string n in filteredNamesSimple)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("Query Syntax - Element Contains \"a\"");
            IEnumerable<string> filteredNamesQS1 = from n in names
                                                   where n.Contains("a")
                                                   select n;


            foreach (string n in filteredNamesQS1)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("Fluent Syntax - Element Contains \"a\"");
            IEnumerable<string> filteredNamesFS1 = names
                .Where(n => n.Contains("a"))
                .Where(n => n.Length > 4)
                .OrderBy(n => n.Length)
                .Select(n => string.Format("<customer name=\"{0}\"></customer>", n));




            foreach (string n in filteredNamesFS1)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("Fluent Syntax - using method in delegate");
            string[] names2 = { "Gaoqiu", "Linchong", "Likui", "Songjiang", "Chaijin", "Gonsunsheng", "Wusong" };
            foreach (string n in
            ToUpperString(names2))
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("Query Syntax 1");

            IEnumerable<string> Query1 =
                from n in names
                where n.Contains("a")
                orderby n.Length
                select string.Format("<customer name=\"{0}\"></customer>", n.ToUpper());

            foreach (string n in Query1)
            {
                Console.WriteLine(n);
            }
        }
        private static IEnumerable<string> ToUpperString(string[] arg)
        {
            IEnumerable<string> filteredNamesFS1 = arg
                .Where(n => n.Contains("a"))
                .Where(n => n.Length > 4)
                .OrderBy(n => n.Length)
                .Select(n => string.Format("<customer name=\"{0}\"></customer>", n));
            return filteredNamesFS1;
        }
    }
}
