﻿using EnmuHelper;

namespace ApplicationHelper.Responses
{
    public class WatcherResponse
    {
        public string WatcherId { get; set; }
        public string UserId { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public string TargetId { get; set; }
        public string IndicatorId { get; set; }
        public decimal Value { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        public decimal? AverageBuy { get; set; }
        public decimal? AverageSell { get; set; }
        public WatcherStatus Status { get; set; }
        public bool Enabled { get; set; }
    }
}
