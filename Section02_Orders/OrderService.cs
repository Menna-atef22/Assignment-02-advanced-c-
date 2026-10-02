namespace Section02_Orders;

// Parts 5 and Bonus: the service does not know how the price is calculated
public class OrderService
{
    private readonly Func<Order, decimal> _pricingStrategy;

    // Part 5: event raised when an order is completed
    public event Action<Order> OrderProcessed;

    public OrderService(Func<Order, decimal> pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;
    }

    public void ProcessOrder(Order order)
    {
        order.Total = _pricingStrategy(order);
        OrderProcessed?.Invoke(order);
    }
}
