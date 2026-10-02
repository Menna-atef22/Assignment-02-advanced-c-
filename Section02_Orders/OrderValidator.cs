namespace Section02_Orders;

// Part 3: validation with Predicate<>
public static class OrderValidator
{
    public static bool ValidateOrder(Order order, Predicate<Order> validationRule)
    {
        return validationRule(order);
    }

    // True only if the order passes every rule
    public static bool ValidateAll(Order order, IEnumerable<Predicate<Order>> rules)
    {
        return rules.All(rule => ValidateOrder(order, rule));
    }
}
