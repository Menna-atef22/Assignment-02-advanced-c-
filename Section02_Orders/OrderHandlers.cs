namespace Section02_Orders;

// Named handlers so they can be subscribed and unsubscribed
public static class OrderHandlers
{
    public static void PrintMessage(Order order)
    {
        Console.WriteLine($"  [Handler1] Order {order.Id} processed. Total = {order.Total:F2}");
    }

    public static void SendNotification(Order order)
    {
        Console.WriteLine($"  [Handler2] Notification sent to {order.CustomerName}.");
    }

    public static void WriteAudit(Order order)
    {
        Console.WriteLine($"  [Handler3] Audit: order {order.Id} completed at {DateTime.Now:HH:mm:ss}.");
    }
}
