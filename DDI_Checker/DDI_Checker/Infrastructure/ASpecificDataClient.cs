using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Infrastructure
{
    internal abstract class ASpecificDataClient<T> where T : AInteraction
    {
        public List<T> Interactions { get; } = new();

        protected abstract T ParseLine(string line);
        public void Fetch(string filepath)
        {
            foreach (var line in File.ReadLines(filepath).Skip(1)) // Skip header 
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    Interactions.Add(ParseLine(line));
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Skipping malformed line: {line}");
                }
            }
        }
    }

    internal class DrugBankClient : ASpecificDataClient<DrugBankInteraction>
    {
        protected override DrugBankInteraction ParseLine(string line)
            => new DrugBankInteraction(line);
    }

    internal class OncHighPriorityClient : ASpecificDataClient<OncHighPriorityInteraction>
    {
        protected override OncHighPriorityInteraction ParseLine(string line)
            => new OncHighPriorityInteraction(line);
    }

    internal class OncNonInterruptiveClient : ASpecificDataClient<OncNonInterruptiveInteraction>
    {
        protected override OncNonInterruptiveInteraction ParseLine(string line)
            => new OncNonInterruptiveInteraction(line);
    }
}
