using RulePattern.Contracts;
namespace RulePattern;
internal enum CompositeOperator { And, Or }
public sealed class CompositeRule<T> : Rule<T>
{
    private readonly IReadOnlyList<IRule<T>> _rules; private readonly CompositeOperator _operator;
    internal CompositeRule(IEnumerable<IRule<T>> rules, CompositeOperator op) { _rules = rules.ToArray(); _operator = op; if (_rules.Count == 0) throw new ArgumentException("At least one rule is required."); }
    public override RuleResult Evaluate(T target)
    {
        if (_operator == CompositeOperator.And) { var r = RuleResult.Success(); foreach (var rule in _rules) r = r.Merge(rule.Evaluate(target)); return r; }
        var results = _rules.Select(r => r.Evaluate(target)).ToArray();
        return results.Any(r => r.IsValid) ? RuleResult.Success() : RuleResult.Failure(results.SelectMany(r => r.Violations));
    }
}
