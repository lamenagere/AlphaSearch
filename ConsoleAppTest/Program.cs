using AlphaSearch;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie<string> tree;

            Console.WriteLine("Please enter a source to feed tree: (0: simple list | 1: file)");
            string source = Console.ReadLine();
            if (source == "0") tree = CreateTrieFromList();
            else tree = CreateTrieFromDocument(Directory.GetCurrentDirectory() + "\\Sample.sql");


            string searchString = string.Empty;
            do
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    List<string> result = new List<string>();
                    result = tree.GetAllStrings(searchString, result);
                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item} ");
                    }
                }
                Console.WriteLine("Please enter a string to search:");
                searchString = Console.ReadLine();
            }
            while (searchString != "exit");


        }

        private static Trie<string> CreateTrieFromList()
        {
            Trie<string> trie = new Trie<string>(false, true);
            trie.InsertRange(new List<string>()
            {
                "A Display of My Dark Power",
                "A Good Thing",
                "A Reckoning Approaches",
                "Abandon Hope",
                "Abandon Reason",
                "Abandoned Outpost",
                "Abandoned Sarcophagus",
                "Abattoir Ghoul",
                "Abbey Gargoyles",
                "Abbey Griffin",
                "Abbey Matron",
                "Abbot of Keral Keep",
                "Abduction",
                "Aberrant Researcher // Perfected Form",
                "Abeyance",
                "Abhorrent Overlord",
                "Abomination",
                "Abomination",
                "Abomination",
                "Abomination"
            });

            return trie;
        }

        private static Trie<string> CreateTrieFromDocument(string path)
        {
            if (!File.Exists(path)) return null;

            var stream = new StreamReader(path);
            string line;
            var trie = new Trie<string>(false, true);

            while((line = stream.ReadLine()) != null)
            {
                if (!line.StartsWith("INSERT")) continue;
                int start = line.IndexOf(", N'");
                if (start < 0) continue;
                int length = (line.Substring(start + 4, line.Length - 5 - start)).IndexOf(", N'");
                string name = line.Substring(start + 4, length - 1);
                trie.Insert(name);
            }

            return trie;
        }
    }
}
