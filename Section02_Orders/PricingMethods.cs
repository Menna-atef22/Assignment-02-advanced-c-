namespace Section02_Orders;

// Named methods that match the PriceCalculator delegate
public static class PricingMethods
{
    public static decimal CalculateTotal(Order order)
    {
        return order.Price * order.Quantity;
    }

    public static decimal CalculateTotalWithDiscount(Order order)
    {
        decimal discount = 5m; // fixed discount
        return order.Price * order.Quantity - discount;
    }
}
