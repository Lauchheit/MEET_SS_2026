using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Infrastructure
{
    internal class DataClient<T> where T : AInteraction
    {
        private readonly Func<string, T> _factory;
        public List<T> Interactions { get; } = new();

        public DataClient(Func<string, T> factory)
        {
            _factory = factory;
        }

        public void Fetch(string filepath)
        {
            foreach (var line in File.ReadLines(filepath).Skip(1)) // Header überspringen
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    Interactions.Add(_factory(line));
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Skipping malformed line: {line}");
                }
            }
        }
    }
}
