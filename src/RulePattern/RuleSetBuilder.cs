using RulePattern.Contracts;
namespace RulePattern;
public sealed class RuleSetBuilder<T>
{
    private readonly List<IRule<T>> _rules=[]; private bool _stop; private Action<RuleResult>? _handler;
    public RuleSetBuilder<T> Add(IRule<T> rule){ArgumentNullException.ThrowIfNull(rule);_rules.Add(rule);return this;}
    public RuleSetBuilder<T> Add(params IRule<T>[] rules){foreach(var r in rules)Add(r);return this;}
    public RuleSetBuilder<T> Add(string rule,Func<T,bool> predicate,string message,string? property=null)=>Add(new PredicateRule<T>(rule,predicate,message,property));
    public RuleSetBuilder<T> StopOnFirstFailure(bool enabled=true){_stop=enabled;return this;}
    public RuleSetBuilder<T> OnResult(Action<RuleResult> handler){_handler=handler;return this;}
    public RuleSet<T> Build()=>new(_rules,_stop,_handler);
}
