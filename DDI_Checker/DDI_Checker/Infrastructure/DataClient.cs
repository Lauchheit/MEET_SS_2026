using DDI_Checker.Infrastructure;
using FuzzySharp;
using DDI_Checker.Core.Models;
namespace DDI_Checker.Infrastructure
{
    public class DataClient
    {
        private readonly DrugBankClient _drugBankClient = new();
        private readonly OncHighPriorityClient _oncHighClient = new();
        private readonly OncNonInterruptiveClient _oncNonInterruptiveClient = new();


        public IEnumerable<AInteraction> AllInteractions = Enumerable.Empty<AInteraction>();

        public IReadOnlyList<DrugBankInteraction> DrugBankInteractions
            => _drugBankClient.Interactions;
        public IReadOnlyList<OncHighPriorityInteraction> OncHighPriorityInteractions
            => _oncHighClient.Interactions;
        public IReadOnlyList<OncNonInterruptiveInteraction> OncNonInterruptiveInteractions
            => _oncNonInterruptiveClient.Interactions;

        public void FetchAll()
        {
            _drugBankClient.Fetch(DrugBankInteraction.FILE_PATH);
            _oncHighClient.Fetch(OncHighPriorityInteraction.FILE_PATH);
            _oncNonInterruptiveClient.Fetch(OncNonInterruptiveInteraction.FILE_PATH);

            this.AllInteractions =
                _drugBankClient.Interactions
                .Concat<AInteraction>(_oncHighClient.Interactions)
                .Concat(_oncNonInterruptiveClient.Interactions);
        }
        public void InvalidateCache()
        {
            AllInteractions = Enumerable.Empty<AInteraction>();
        }
        public IEnumerable<AInteraction> GetAllInteractions()
        {
            if (!AllInteractions.Any())
                FetchAll();
            return AllInteractions;
        }

        public IEnumerable<AInteraction> SearchInteractionWithDrug(string drugQuery, int minScore = 70)
        {
            return GetAllInteractions().Where(interaction =>
            {
                var (drug1, drug2) = interaction.GetDrugs();
                int score1 = Fuzz.Ratio(drugQuery.ToLower(), drug1.Name.ToLower());
                int score2 = Fuzz.Ratio(drugQuery.ToLower(), drug2.Name.ToLower());
                return score1 >= minScore || score2 >= minScore;
            });
        }

        public IEnumerable<AInteraction> SearchInteractionBetweenDrugs(
            string drugQuery1, string drugQuery2, int minScore = 90)
        {
            return GetAllInteractions().Where(interaction =>
            {
                var drugs = interaction.GetDrugs();
                int d1Score1 = Fuzz.Ratio(drugQuery1.ToLower(), drugs.Item1.Name.ToLower());
                int d1Score2 = Fuzz.Ratio(drugQuery1.ToLower(), drugs.Item2.Name.ToLower());
                int d2Score1 = Fuzz.Ratio(drugQuery2.ToLower(), drugs.Item1.Name.ToLower());
                int d2Score2 = Fuzz.Ratio(drugQuery2.ToLower(), drugs.Item2.Name.ToLower());

                return (d1Score1 >= minScore && d2Score2 >= minScore) ||
                       (d1Score2 >= minScore && d2Score1 >= minScore);
            }).Distinct();
        }
    }
}
