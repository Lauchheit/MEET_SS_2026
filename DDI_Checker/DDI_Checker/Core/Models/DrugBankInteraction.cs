namespace DDI_Checker.Core.Models
{
    public class DrugBankInteraction : AInteraction
    {
        public const char SEPARATOR = '\t';
        public const string FILE_PATH = "Resources/DDIs/drugbank5-interactions-NLM-R01-drugs.tsv";

        protected override char Separator => SEPARATOR;

        public string Description { get; }

        public DrugBankInteraction(string csvLine)
            : this(csvLine.Split(SEPARATOR).Select(c => c.Trim()).ToArray()) { }

        public DrugBankInteraction(string[] columns)
            : base(new Drug(columns[0], columns[1]),
                   new Drug(columns[2], columns[3]),
                   ESeverity.Unknown)
        {
            Description = columns.Length > 4 ? columns[4] : string.Empty;
        }

        public override string ToString() =>
            $"[DrugBank] {Drug1.Name} [{Drug1.DrugBankId}] ↔ {Drug2.Name} [{Drug2.DrugBankId}]\n" +
            $"Severity:    {Severity}\n" +
            $"Description: {Description}";
    }
}
