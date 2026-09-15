# RulePattern

A small, composable Rule Pattern for .NET domain rules.

The core deliberately treats rules as first-class domain objects rather than reproducing a FluentValidation-style property validation DSL.

```csharp
public sealed class CustomerIsActiveRule : Rule<Order>
{
    public override RuleResult Evaluate(Order order) =>
        order.Customer.IsActive
            ? RuleResult.Success()
            : RuleResult.Failure("customer.inactive", "Customer is not active.");
}
```

## Composition

```csharp
var paymentRule =
    new CustomerHasCreditRule()
        .Or(new CustomerHasValidPaymentMethodRule());

var orderRule =
    new CustomerIsActiveRule()
        .And(paymentRule)
        .And(new OrderHasItemsRule());
```

Also available: `Not()` and `When(predicate)`.

## RuleSetBuilder

```csharp
var rules = new RuleSetBuilder<Order>()
    .Add(new CustomerIsActiveRule())
    .Add(new CustomerHasCreditRule())
    .Add(new OrderHasItemsRule())
    .StopOnFirstFailure()
    .Build();

var result = rules.Evaluate(order);
```

Predicate rules exist as a convenience, but dedicated rule classes are preferred for domain behavior.

## Optional Notify integration

The core has no Notify dependency. An optional adapter can map `RuleViolation` to a notification context.
