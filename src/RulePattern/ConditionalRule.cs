using RulePattern.Contracts;

namespace RulePattern;
public sealed class ConditionalRule<T> : Rule<T>
{
    private readonly IRule<T> _inner; private readonly Func<T,bool> _condition;
    internal ConditionalRule(IRule<T> inner, Func<T,bool> condition) { _inner=inner; _condition=condition; }
    public override RuleResult Evaluate(T target) => _condition(target) ? _inner.Evaluate(target) : RuleResult.Success();
}
