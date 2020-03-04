﻿

namespace ApplicationHelper.Responses
{
    public class HealthResponse
    {
        public bool IsEverythingOk { get; set; }
        public bool IsConnectionToDatabaseOk { get; set; }
        public bool IsConnectionToCoinMarketCapOk { get; set; }
        public bool IsResponseTimeAcceptable { get; set; }
    }
}
