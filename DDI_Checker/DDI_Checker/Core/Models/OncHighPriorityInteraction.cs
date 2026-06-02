using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Core.Models
{
    public class OncHighPriorityInteraction : AInteraction
    {
        public const char SEPARATOR = '$';
        public const string FILE_PATH = "Resources/DDIs/ONC_High_Priority_Mapped.csv";

        protected override char Separator => SEPARATOR;

        public OncHighPriorityInteraction(string csvLine)
            : this(csvLine.Split(SEPARATOR).Select(c => c.Trim()).ToArray()) { }

        public OncHighPriorityInteraction(string[] columns)
            : base(new Drug(columns[0], columns[1]),
                   new Drug(columns[2], columns[3]),
                   ESeverity.HighPriority)
        { }

        public override string ToString() =>
            $"[DrugBank] {Drug1.Name} [{Drug1.DrugBankId}] ↔ {Drug2.Name} [{Drug2.DrugBankId}]\n" +
            $"Severity: {Severity}";
    }
}
