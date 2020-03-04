﻿

using EnmuHelper;

namespace ApplicationHelper.Responses
{
    public class IndicatorResponse
    {
        public string IndicatorId { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
        public string[] Dependencies { get; set; }
    }
}
