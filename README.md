# Assignment 02 advanced c#

Two console projects in one solution.

| Project | Content |
|---|---|
| `Section01_Books` | Book / BookFunctions / ProcessBooks with delegates, Func, anonymous method, lambda |
| `Section02_Orders` | Order processing: delegates, Func, Predicate, Action, events, bonus strategies |

## Run

```
dotnet run --project Section01_Books
dotnet run --project Section02_Orders
```

Requires the .NET 8 SDK.

---

## Section 02 – Answers

**Q1. `PriceCalculator` vs `Func<Order, decimal>`**
Both describe "takes an Order, returns a decimal". `PriceCalculator` is a custom type we have to declare ourselves.
`Func<Order, decimal>` is built into .NET, so no extra declaration is needed. They are different types, so one
cannot be assigned to the other directly, although the same method or lambda can be converted to either.

**What problem does `Func<>` solve?**
It removes the need to write a new delegate type for every signature. Less code, and everyone already knows what it means.

**Q2. `Action<Order>` vs `Func<Order, decimal>`**
`Action<Order>` returns nothing (`void`); it is used to *do* something (print, log, send).
`Func<Order, decimal>` returns a value; it is used to *compute* something.

**Q3. Why does `Predicate<T>` return `bool`?**
It represents a yes/no question about an object: "does this item satisfy a condition?"
Typical uses are validation rules, filtering and searching (`List<T>.Find`, `RemoveAll`, ...).

**Why `Predicate<Order>` is more expressive than `Func<Order, bool>`**
The name tells the reader the delegate is a condition/rule. `Func<Order, bool>` could be anything that happens to
return a bool, so the intent is less clear.

**Q4. Delegate vs event**
A delegate is a type that holds a reference to one or more methods. An event is a member built on a delegate that
restricts access: outside code can only subscribe (`+=`) or unsubscribe (`-=`), not invoke it or replace it.

**Q5. Why can't external code invoke an event declared in another class?**
Outside the declaring class, an event only exposes `add` and `remove`. Only the class that owns the event can raise it.
This stops other code from firing fake notifications.

**Q6. Multiple handlers on the same event**
The event holds a multicast delegate. When it is raised, all handlers are called in the order they were subscribed.

**Q7. `orderService.OrderProcessed += HandleOrderProcessed;`**
- `orderService.OrderProcessed` – the event on that service object.
- `+` / `=` together (`+=`) – add a handler to the event's invocation list without removing existing ones.
- `HandleOrderProcessed` – the method that will be called when the event is raised (its signature must match `Action<Order>`).

**Q8. `Action<Order>` vs `event Action<Order>`**
If we expose a plain `Action<Order>` field, any outside code can call it directly, overwrite it with `=` and remove
all other subscribers. With `event`, outside code can only use `+=` and `-=`; only the owner class can raise it.
So an event protects the publisher and keeps the notification logic correct.

---

## Bonus

`OrderService` receives a `Func<Order, decimal>` in its constructor and never knows how the price is calculated.
The caller picks one of `PricingStrategies.Normal`, `TenPercentOff`, `TwentyPercentOff` or `Vip`.
New strategies can be added without changing `OrderService`.
