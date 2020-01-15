using System;
using System.Collections.Generic;

namespace Onset.Depset
{
    public class Test
    {
        public static void Main(string[] args)
        {
            List<Plugin> plugins = new List<Plugin>
            {
                new Plugin("test0", "test1"),
                new Plugin("test1", "test2"), new Plugin("test2"), new Plugin("test3", "test2"), new Plugin("test4"), new Plugin("test5", "test3")
            };
            ISorter<Plugin> sorter = DepsetFactory.CreateSorter<Plugin>(Algorithm.PriorValuing);
            foreach (Plugin plugin in sorter.Sort(plugins))
            {
                Console.WriteLine(plugin.Identifier);
            }

            Console.ReadLine();
        }

        public class Plugin : IDepsetHandle
        {
            public string Identifier { get; }

            public List<string> Dependencies { get; }

            public Plugin(string id, params string[] dependencies)
            {
                Identifier = id;
                Dependencies = new List<string>();
                Dependencies.AddRange(dependencies);
            }
        }
    }
}
