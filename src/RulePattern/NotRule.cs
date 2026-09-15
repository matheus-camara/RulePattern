using RulePattern.Contracts;

namespace RulePattern;
public sealed class NotRule<T> : Rule<T>
{
    private readonly IRule<T> _inner; internal NotRule(IRule<T> inner) => _inner = inner;
    public override RuleResult Evaluate(T target) => _inner.Evaluate(target).IsValid ? RuleResult.Failure("rule.negated", "The negated rule was satisfied.") : RuleResult.Success();
}
