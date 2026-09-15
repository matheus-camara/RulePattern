using Xunit;
using RulePattern;

namespace RulePattern.Tests;

public sealed class RuleResultTests
{
    [Fact]
    public void Failure_ShouldAllowRuleWithoutMessage()
    {
        var result = RuleResult.Failure("customer.not-found");

        Assert.True(result.IsInvalid);
        var violation = Assert.Single(result.Violations);
        Assert.Equal("customer.not-found", violation.Rule);
        Assert.Null(violation.Message);
        Assert.Null(violation.Property);
    }

    [Fact]
    public void Failure_ShouldStillSupportMessageAndProperty()
    {
        var result = RuleResult.Failure("customer.invalid", "Customer is invalid.", "customer");

        var violation = Assert.Single(result.Violations);
        Assert.Equal("customer.invalid", violation.Rule);
        Assert.Equal("Customer is invalid.", violation.Message);
        Assert.Equal("customer", violation.Property);
    }
}
