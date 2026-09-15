using Xunit;
using RulePattern;
using RulePattern.Contracts;

namespace RulePattern.Tests;

public sealed class OrShortCircuitTests
{
    [Fact]
    public void Or_ShouldStopAfterFirstSuccessfulRule()
    {
        var evaluated = new List<string>();
        var first = new TrackingRule("first", RuleResult.Success(), evaluated);
        var second = new TrackingRule("second", RuleResult.Success(), evaluated);

        var result = first.Or(second).Evaluate(new object());

        Assert.True(result.IsValid);
        Assert.Equal(["first"], evaluated);
    }

    [Fact]
    public void Or_ShouldEvaluateAllRulesWhenAllFail()
    {
        var evaluated = new List<string>();
        var first = new TrackingRule("first", RuleResult.Failure("first", "First failed."), evaluated);
        var second = new TrackingRule("second", RuleResult.Failure("second", "Second failed."), evaluated);

        var result = first.Or(second).Evaluate(new object());

        Assert.True(result.IsInvalid);
        Assert.Equal(["first", "second"], evaluated);
        Assert.Equal(2, result.Violations.Count);
    }

    private sealed class TrackingRule(string name, RuleResult result, List<string> evaluated) : Rule<object>
    {
        public override RuleResult Evaluate(object target)
        {
            evaluated.Add(name);
            return result;
        }
    }
}
