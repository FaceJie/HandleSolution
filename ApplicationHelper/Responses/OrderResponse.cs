
using EnmuHelper;
using System;

namespace ApplicationHelper.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public string UserId { get; set; }
        public string CurrencyId { get; set; }
        public decimal Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }
}
