using RulePattern.Contracts;

namespace RulePattern;

internal enum CompositeOperator { And, Or }

public sealed class CompositeRule<T> : Rule<T>
{
    private readonly IReadOnlyList<IRule<T>> _rules;
    private readonly CompositeOperator _operator;

    internal CompositeRule(IEnumerable<IRule<T>> rules, CompositeOperator op)
    {
        _rules = rules.ToArray();
        _operator = op;

        if (_rules.Count == 0)
            throw new ArgumentException("At least one rule is required.");
    }

    public override RuleResult Evaluate(T target)
    {
        if (_operator == CompositeOperator.And)
        {
            var result = RuleResult.Success();

            foreach (var rule in _rules)
                result = result.Merge(rule.Evaluate(target));

            return result;
        }

        var violations = new List<RuleViolation>();

        foreach (var rule in _rules)
        {
            var result = rule.Evaluate(target);

            if (result.IsValid)
                return RuleResult.Success();

            violations.AddRange(result.Violations);
        }

        return RuleResult.Failure(violations);
    }
}
