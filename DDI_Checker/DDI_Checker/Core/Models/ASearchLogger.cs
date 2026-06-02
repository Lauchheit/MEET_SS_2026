using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DDI_Checker.Core.Models
{
    public abstract class ASearchLogger
    {
        // Parameter-Speicher, damit die abstrakten Methoden ohne Argumente auskommen
        protected IEnumerable<AInteraction>? CurrentInteractions { get; private set; }
        protected string? CurrentDrug1 { get; private set; }
        protected string? CurrentDrug2 { get; private set; }
        protected DateTime CurrentTimestamp { get; private set; }
        protected IPAddress? CurrentUserId { get; private set; }

        // Öffentliche Einstiegspunkte — hier ruft der Aufrufer rein
        public void LogResult(IEnumerable<AInteraction>? interactions, DateTime timestamp, IPAddress? userId)
        {
            CurrentInteractions = interactions;
            CurrentTimestamp = timestamp;
            CurrentUserId = userId;
            LogResult();
        }

        public void LogSearch(string drug1, string? drug2, DateTime timestamp, IPAddress? userId)
        {
            CurrentDrug1 = drug1;
            CurrentDrug2 = drug2;
            CurrentTimestamp = timestamp;
            CurrentUserId = userId;
            LogSearch();
        }

        // Hilfsmethoden für Kindklassen
        private string GetBaseMessage() =>
            $"[{CurrentTimestamp}]\nUser: {CurrentUserId?.ToString() ?? "unknown"}";

        protected string GetResultMessage()
        {
            var list = CurrentInteractions?.ToList() ?? new();
            string summary = $"Result: {list.Count} interaction{(list.Count == 1 ? "" : "s")}";
            string detail  = list.Count == 0
                ? "No interactions were found."
                : string.Join("\n---\n", list);

            return $"{GetBaseMessage()}\n{summary}\n{detail}";
        }

        protected string GetSearchMessage()
        {
            string drugs = CurrentDrug1?.ToString() ?? "?";
            if (CurrentDrug2 != null)
                drugs += $" ↔ {CurrentDrug2}";

            return $"{GetBaseMessage()}\nSearched for: {drugs}";
        }

        protected abstract void LogResult();
        protected abstract void LogSearch();
    }
}
