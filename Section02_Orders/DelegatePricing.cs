namespace Section02_Orders;

// Part 1: uses the user-defined delegate
public static class DelegatePricing
{
    public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
    {
        return calculator(order);
    }
}
