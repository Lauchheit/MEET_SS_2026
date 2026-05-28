using DDI_Checker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Infrastructure
{
    internal abstract class AInteraction
    {
        protected Drug Drug1;
        protected Drug Drug2;
        protected ESeverity Severity;

        protected AInteraction(Drug drug1, Drug drug2, ESeverity severity)
        {
            Drug1 = drug1;
            Drug2 = drug2;
            Severity = severity;
        }

        protected abstract char Separator { get; }

        public static AInteraction FromCsvLine(string line)
            => throw new NotImplementedException(); 

        protected string[] Split(string line) => line.Split(Separator)
                                                      .Select(c => c.Trim())
                                                      .ToArray();

        public Tuple<Drug, Drug> GetDrugs() => new(Drug1, Drug2);
        public abstract override string ToString();
    }
}
