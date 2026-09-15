namespace RulePattern.Contracts;
public interface IRule<in T> { RuleResult Evaluate(T target); }
