namespace DDI_Checker.Core.Models
{
    public abstract class AInteraction
    {
        public Drug Drug1 { get; }
        public Drug Drug2 { get; }
        public ESeverity Severity { get; }

        protected AInteraction(Drug drug1, Drug drug2, ESeverity severity)
        {
            Drug1 = drug1;
            Drug2 = drug2;
            Severity = severity;
        }

        protected abstract char Separator { get; }

        protected string[] Split(string line) => line.Split(Separator)
                                                      .Select(c => c.Trim())
                                                      .ToArray();

        public Tuple<Drug, Drug> GetDrugs() => new(Drug1, Drug2);
        public abstract override string ToString();

        public override bool Equals(object? obj)
        {
            if (obj is not AInteraction other) return false;

            var thisId1 = Drug1.DrugBankId;
            var thisId2 = Drug2.DrugBankId;
            var otherId1 = other.Drug1.DrugBankId;
            var otherId2 = other.Drug2.DrugBankId;

            bool sameDrugs = (thisId1 == otherId1 && thisId2 == otherId2) ||
                             (thisId1 == otherId2 && thisId2 == otherId1);

            bool sameSource = this.GetType() == other.GetType();

            return sameDrugs && sameSource;
        }

        public override int GetHashCode()
        {
            int drugHash = (Drug1.DrugBankId?.GetHashCode() ?? 0) ^
                           (Drug2.DrugBankId?.GetHashCode() ?? 0);

            return HashCode.Combine(drugHash, this.GetType());
        }
    }
}
