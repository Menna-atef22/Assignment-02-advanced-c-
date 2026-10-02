using Section02_Orders;

Order order = new Order { Id = 1, CustomerName = "Ahmed", Price = 100m, Quantity = 3 };

// ---------- Part 1: user-defined delegate ----------
Console.WriteLine("=== Part 1: PriceCalculator ===");
Console.WriteLine(DelegatePricing.CalculateOrderPrice(order, PricingMethods.CalculateTotal));
Console.WriteLine(DelegatePricing.CalculateOrderPrice(order, PricingMethods.CalculateTotalWithDiscount));

// ---------- Part 2: Func<> with lambdas ----------
Console.WriteLine("\n=== Part 2: Func<Order, decimal> ===");
Console.WriteLine(FuncPricing.CalculateOrderPrice(order, x => x.Price * x.Quantity));
Console.WriteLine(FuncPricing.CalculateOrderPrice(order, x => x.Price * x.Quantity * 0.9m));

// ---------- Part 3: Predicate<> ----------
Console.WriteLine("\n=== Part 3: Predicate<Order> ===");
Predicate<Order> hasQuantity = o => o.Quantity > 0;
Predicate<Order> hasPrice = o => o.Price > 0;
Predicate<Order> hasCustomer = o => !string.IsNullOrWhiteSpace(o.CustomerName);

Console.WriteLine($"Quantity > 0 : {OrderValidator.ValidateOrder(order, hasQuantity)}");
Console.WriteLine($"Price > 0    : {OrderValidator.ValidateOrder(order, hasPrice)}");
Console.WriteLine($"Has customer : {OrderValidator.ValidateOrder(order, hasCustomer)}");

// ---------- Part 4: Action<> ----------
Console.WriteLine("\n=== Part 4: Action<Order> ===");
Action<Order> printOrder = o => Console.WriteLine($"Order {o.Id} processed.");
Action<Order> sendConfirmation = o => Console.WriteLine($"Confirmation sent to {o.CustomerName}.");
Action<Order> writeAudit = o => Console.WriteLine($"Audit: order {o.Id} written to log.");

OrderProcessor.ProcessOrder(order, printOrder);
OrderProcessor.ProcessOrder(order, sendConfirmation);
OrderProcessor.ProcessOrder(order, writeAudit);

// ---------- Parts 5 and 6: events ----------
Console.WriteLine("\n=== Parts 5 & 6: Events (subscribe / unsubscribe) ===");
OrderService service = new OrderService(PricingStrategies.Normal);

service.OrderProcessed += OrderHandlers.PrintMessage;
service.OrderProcessed += OrderHandlers.SendNotification;
service.OrderProcessed += OrderHandlers.WriteAudit;

Console.WriteLine("All three handlers subscribed:");
service.ProcessOrder(order);

service.OrderProcessed -= OrderHandlers.PrintMessage;
Console.WriteLine("After unsubscribing Handler1:");
service.ProcessOrder(order);

// ---------- Part 7: final flow ----------
// Order -> Validate -> Calculate Price -> Process -> OrderProcessed -> Handlers
Console.WriteLine("\n=== Part 7: Final flow ===");
List<Predicate<Order>> rules = new List<Predicate<Order>> { hasQuantity, hasPrice, hasCustomer };

List<Order> orders = new List<Order>
{
    new Order { Id = 10, CustomerName = "Sara", Price = 50m, Quantity = 2 },
    new Order { Id = 11, CustomerName = "", Price = 50m, Quantity = 2 },   // invalid: no customer
    new Order { Id = 12, CustomerName = "Omar", Price = 20m, Quantity = 0 } // invalid: quantity 0
};

OrderService finalService = new OrderService(PricingStrategies.Normal);
finalService.OrderProcessed += OrderHandlers.PrintMessage;
finalService.OrderProcessed += OrderHandlers.SendNotification;
finalService.OrderProcessed += OrderHandlers.WriteAudit;

// Only valid orders continue (no if/else needed)
orders
    .Where(o => OrderValidator.ValidateAll(o, rules))
    .ToList()
    .ForEach(finalService.ProcessOrder);

// ---------- Bonus: pricing strategies at runtime ----------
Console.WriteLine("\n=== Bonus: Pricing strategies ===");
Dictionary<string, Func<Order, decimal>> strategies = new Dictionary<string, Func<Order, decimal>>
{
    { "Normal", PricingStrategies.Normal },
    { "10% Discount", PricingStrategies.TenPercentOff },
    { "20% Discount", PricingStrategies.TwentyPercentOff },
    { "VIP", PricingStrategies.Vip }
};

foreach (var strategy in strategies)
{
    OrderService s = new OrderService(strategy.Value);
    s.OrderProcessed += o => Console.WriteLine($"  {strategy.Key,-13}: total = {o.Total:F2}");
    s.ProcessOrder(new Order { Id = 99, CustomerName = "Mona", Price = 100m, Quantity = 2 });
}
