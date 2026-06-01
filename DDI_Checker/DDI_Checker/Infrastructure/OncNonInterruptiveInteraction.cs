using DDI_Checker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Infrastructure
{
    public class OncNonInterruptiveInteraction : AInteraction
    {
        public const char SEPARATOR = '$';
        public const string FILE_PATH = "Resources/DDIs/ONC_Non_Interuptive_Mapped.csv";

        protected override char Separator => SEPARATOR;

        public OncNonInterruptiveInteraction(string csvLine)
            : this(csvLine.Split(SEPARATOR).Select(c => c.Trim()).ToArray()) { }

        public OncNonInterruptiveInteraction(string[] columns)
            : base(new Drug(columns[0], columns[1]),
                   new Drug(columns[2], columns[3]),
                   ESeverity.NonInterruptive)
        { }

        public override string ToString() =>
            $"[DrugBank] {Drug1.Name} [{Drug1.DrugBankId}] ↔ {Drug2.Name} [{Drug2.DrugBankId}]\n" +
            $"Severity: {Severity}";
    }
}
