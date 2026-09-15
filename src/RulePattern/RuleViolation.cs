namespace RulePattern;

public sealed record RuleViolation(string Rule, string? Message = null, string? Property = null);
