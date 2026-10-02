namespace Section02_Orders;

// Bonus: pricing strategies supplied from outside as Func<Order, decimal>
public static class PricingStrategies
{
    public static readonly Func<Order, decimal> Normal = o => o.Price * o.Quantity;
    public static readonly Func<Order, decimal> TenPercentOff = o => o.Price * o.Quantity * 0.90m;
    public static readonly Func<Order, decimal> TwentyPercentOff = o => o.Price * o.Quantity * 0.80m;
    public static readonly Func<Order, decimal> Vip = o => o.Price * o.Quantity * 0.70m;
}
