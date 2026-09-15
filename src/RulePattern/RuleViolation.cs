namespace RulePattern;
public sealed record RuleViolation(string Rule, string Message, string? Property = null);
