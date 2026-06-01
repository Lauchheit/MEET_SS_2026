using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

using DDI_Checker.Core.Models;

namespace DDI_Checker.Infrastructure
{
    public abstract class ASpecificDataClient<T>  where T : AInteraction
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

    public class DrugBankClient : ASpecificDataClient<DrugBankInteraction>
    {
        protected override DrugBankInteraction ParseLine(string line)
            => new DrugBankInteraction(line);
    }

    public class OncHighPriorityClient : ASpecificDataClient<OncHighPriorityInteraction>
    {
        protected override OncHighPriorityInteraction ParseLine(string line)
            => new OncHighPriorityInteraction(line);
    }

    public class OncNonInterruptiveClient : ASpecificDataClient<OncNonInterruptiveInteraction>
    {
        protected override OncNonInterruptiveInteraction ParseLine(string line)
            => new OncNonInterruptiveInteraction(line);
    }
}
