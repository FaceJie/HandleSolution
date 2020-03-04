using System;
using System.Collections.Generic;
using System.Text;

namespace EnmuHelper
{
    public enum IndicatorType
    {
        CurrencyIndicator,
        UserIndicator,
        WatcherIndicator,
        OrderIndicator
    }
    public enum OrderType
    {
        BuyLimit,
        SellLimit,
        BuyMarket,
        SellMarket
    }

    public enum WatcherStatus
    {
        Buy,
        Sell,
        Hold
    }

    public enum OrderStatus
    {
        Pending,
        Filled,
        Cancelled
    }
}
