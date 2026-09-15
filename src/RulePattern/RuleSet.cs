using RulePattern.Contracts;
namespace RulePattern;

public sealed class RuleSet<T>
{
    private readonly IReadOnlyList<IRule<T>> _rules;
    private readonly bool _stop;
    private readonly Action<RuleResult>? _handler;

    internal RuleSet(IEnumerable<IRule<T>> rules, bool stop, Action<RuleResult>? handler) { _rules = rules.ToArray(); _stop = stop; _handler = handler; }
    public RuleResult Evaluate(T target)
    {
        var v = new List<RuleViolation>();
        foreach (var rule in _rules)
        {
            var r = rule.Evaluate(target);
            _handler?.Invoke(r);
            if (r.IsInvalid)
            {
                v.AddRange(r.Violations);
                if (_stop) break;
            }
        }
        return RuleResult.Failure(v);
    }
    public bool IsValid(T target) => Evaluate(target).IsValid;
}
