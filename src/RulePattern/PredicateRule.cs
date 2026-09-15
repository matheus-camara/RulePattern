namespace RulePattern;
public sealed class PredicateRule<T> : Rule<T>
{
    private readonly Func<T,bool> _predicate; private readonly string _rule,_message; private readonly string? _property;
    public PredicateRule(string rule, Func<T,bool> predicate, string message, string? property=null) { _rule=rule; _predicate=predicate; _message=message; _property=property; }
    public override RuleResult Evaluate(T target) => _predicate(target) ? RuleResult.Success() : RuleResult.Failure(_rule,_message,_property);
}
