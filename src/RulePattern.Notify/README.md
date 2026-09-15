# RulePattern.Notify

Optional adapter that bridges `RulePattern` results into `Notifiable.Contracts.INotificationContext`.

The adapter package is deliberately separate from `RulePattern` so consumers that do not use Notify do not take a dependency on it.
