﻿using EnmuHelper;
using System;


namespace ApplicationHelper.Responses
{
    public class LineResponse
    {
        public string TargetId { get; set; }
        public string IndicatorId { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public string UserId { get; set; }
        public decimal? Value { get; set; }
        public decimal? AverageBuy { get; set; }
        public decimal? AverageSell { get; set; }
        public DateTime Time { get; set; }
    }
}
