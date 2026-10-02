namespace Section02_Orders;

// Part 2: same idea, but with the built-in Func<>
public static class FuncPricing
{
    public static decimal CalculateOrderPrice(Order order, Func<Order, decimal> calculator)
    {
        return calculator(order);
    }
}
