// Optional adapter source. Add a ProjectReference/PackageReference to your Notify package,
// then uncomment the implementation below.
//
// using Notifiable.Contracts;
// namespace RulePattern.Notify;
// public static class RuleNotificationExtensions
// {
//     public static RuleResult AddTo(this RuleResult result, INotificationContext context)
//     {
//         foreach (var v in result.Violations)
//             context.AddNotification(v.Property, v.Rule, v.Message);
//         return result;
//     }
// }
