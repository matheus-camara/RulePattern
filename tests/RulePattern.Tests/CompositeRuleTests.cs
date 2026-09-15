using RulePattern;
using Xunit;

namespace RulePattern.Tests;

public sealed class CompositeRuleTests
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

    [Fact]
    public void And_ShouldEvaluateEveryRuleAndAggregateViolations()
    {
        var evaluated = new List<string>();
        var first = new TrackingRule("first", RuleResult.Failure("first", "First failed."), evaluated);
        var second = new TrackingRule("second", RuleResult.Failure("second", "Second failed."), evaluated);

        var result = first.And(second).Evaluate(new object());

        Assert.True(result.IsInvalid);
        Assert.Equal(["first", "second"], evaluated);
        Assert.Equal(2, result.Violations.Count);
    }

    [Fact]
    public void RuleSet_StopOnFirstFailure_ShouldStopEvaluation()
    {
        var evaluated = new List<string>();
        var first = new TrackingRule("first", RuleResult.Failure("first", "First failed."), evaluated);
        var second = new TrackingRule("second", RuleResult.Success(), evaluated);

        var result = new RuleSetBuilder<object>()
            .Add(first)
            .Add(second)
            .StopOnFirstFailure()
            .Build()
            .Evaluate(new object());

        Assert.True(result.IsInvalid);
        Assert.Equal(["first"], evaluated);
    }

    [Fact]
    public void RuleSet_Default_ShouldEvaluateAllRules()
    {
        var evaluated = new List<string>();
        var first = new TrackingRule("first", RuleResult.Failure("first", "First failed."), evaluated);
        var second = new TrackingRule("second", RuleResult.Success(), evaluated);

        var result = new RuleSetBuilder<object>()
            .Add(first)
            .Add(second)
            .Build()
            .Evaluate(new object());

        Assert.True(result.IsInvalid);
        Assert.Equal(["first", "second"], evaluated);
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
