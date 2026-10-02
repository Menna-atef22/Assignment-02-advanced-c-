namespace Section02_Orders;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Filled in by OrderService after pricing
    public decimal Total { get; set; }
}
