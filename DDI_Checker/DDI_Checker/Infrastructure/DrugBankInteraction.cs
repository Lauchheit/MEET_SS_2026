using DDI_Checker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDI_Checker.Infrastructure
{
    internal class DrugBankInteraction : AInteraction
    {
        public const char SEPARATOR = '\t';
        public const string FILE_PATH = "Resources/DDIs/drugbank5-interactions-NLM-R01-drugs.tsv";

        protected override char Separator => SEPARATOR;
        private readonly string _description;

        public DrugBankInteraction(string csvLine)
            : this(csvLine.Split(SEPARATOR).Select(c => c.Trim()).ToArray()) { }

        public DrugBankInteraction(string[] columns)
            : base(new Drug(columns[0], columns[1]),
                   new Drug(columns[2], columns[3]),
                   ESeverity.Unknown)
        {
            _description = columns.Length > 4 ? columns[4] : string.Empty;
        }

        public override string ToString() =>
            $"[DrugBank] {Drug1.Name} ↔ {Drug2.Name}\n" +
            $"Severity:    {Severity}\n" +
            $"Description: {_description}";
    }
}
