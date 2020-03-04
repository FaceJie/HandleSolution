﻿


namespace ApplicationHelper.Responses
{
    public class CurrencyResponse
    {
        public string CurrencyId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public short Rank { get; set; }
        public decimal Price { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Volume24H { get; set; }
        public decimal PercentageChange24H { get; set; }
    }
}
