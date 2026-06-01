using DDI_Checker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;

namespace DDI_Checker.Core.Models
{
    public class CompositeSearchLogger : ASearchLogger
    {
        private readonly List<ASearchLogger> _loggers = new();

        public CompositeSearchLogger(params ASearchLogger[] loggers)
        {
            _loggers.AddRange(loggers);
        }

        public void Add(ASearchLogger logger) => _loggers.Add(logger);

        protected override void LogResult()
        {
            foreach (var logger in _loggers)
                logger.LogResult(CurrentInteractions, CurrentTimestamp, CurrentUserId);
        }

        protected override void LogSearch()
        {
            foreach (var logger in _loggers)
                logger.LogSearch(CurrentDrug1!, CurrentDrug2, CurrentTimestamp, CurrentUserId);
        }
    }
}
