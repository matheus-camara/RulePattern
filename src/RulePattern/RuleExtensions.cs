using RulePattern.Contracts;
namespace RulePattern;
public static class RuleExtensions
{
    public static Rule<T> And<T>(this IRule<T> first, IRule<T> second)=>new CompositeRule<T>([first,second],CompositeOperator.And);
    public static Rule<T> Or<T>(this IRule<T> first, IRule<T> second)=>new CompositeRule<T>([first,second],CompositeOperator.Or);
    public static Rule<T> Not<T>(this IRule<T> rule)=>new NotRule<T>(rule);
    public static Rule<T> When<T>(this IRule<T> rule,Func<T,bool> condition)=>new ConditionalRule<T>(rule,condition);
}
