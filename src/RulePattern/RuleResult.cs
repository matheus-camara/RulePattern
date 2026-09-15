namespace RulePattern;

public sealed class RuleResult
{
    private RuleResult(bool valid, IReadOnlyList<RuleViolation> violations)
    {
        IsValid = valid;
        Violations = violations;
    }

    public bool IsValid { get; }
    public bool IsInvalid => !IsValid;
    public IReadOnlyList<RuleViolation> Violations { get; }

    public static RuleResult Success() => new(true, []);

    public static RuleResult Failure(string rule, string? message = null, string? property = null) =>
        new(false, [new(rule, message, property)]);

    public static RuleResult Failure(IEnumerable<RuleViolation> violations)
    {
        var v = violations.ToArray();
        return new(v.Length == 0, v);
    }

    public RuleResult Merge(RuleResult other) =>
        IsValid && other.IsValid
            ? Success()
            : Failure(Violations.Concat(other.Violations));
}
