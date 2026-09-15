using RulePattern.Contracts;
namespace RulePattern;
public abstract class Rule<T> : IRule<T>
{
    public abstract RuleResult Evaluate(T target);
    public Rule<T> And(IRule<T> other) => new CompositeRule<T>([this, other], CompositeOperator.And);
    public Rule<T> Or(IRule<T> other) => new CompositeRule<T>([this, other], CompositeOperator.Or);
    public Rule<T> Not() => new NotRule<T>(this);
    public Rule<T> When(Func<T, bool> condition) => new ConditionalRule<T>(this, condition);
}
