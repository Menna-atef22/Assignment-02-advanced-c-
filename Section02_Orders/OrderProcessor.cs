namespace Section02_Orders;

// Part 4: behavior is passed in, this method never changes
public static class OrderProcessor
{
    public static void ProcessOrder(Order order, Action<Order> action)
    {
        action(order);
    }
}
