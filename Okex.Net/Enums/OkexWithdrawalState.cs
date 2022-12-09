namespace Okex.Net.Enums;

public enum OkexWithdrawalState
{
    PendingCancel,
    Canceled,
    Failed,
    Pending,
    Sending,
    Sent,
    AwaitingEmailVerification,
    AwaitingManualVerification,
    AwaitingIdentityVerification,
}