﻿
using System;
using System.Collections.Generic;

namespace ApplicationHelper.Responses
{
    public class ScriptVariablesResponse
    {
        public DateTime[] Times { get; set; }
        public string[] Currencies { get; set; }
        public string[] Indicators { get; set; }
        public Dictionary<DateTime, Dictionary<string, Dictionary<string, decimal>>> Values { get; set; }
    }
}
