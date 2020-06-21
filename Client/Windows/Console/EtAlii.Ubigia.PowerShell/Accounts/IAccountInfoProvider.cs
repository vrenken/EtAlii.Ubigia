namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IAccountInfoProvider 
    {
        string AccountName { get; }
        Account Account { get; }
        Guid AccountId { get; }

        Storage TargetStorage { get; }
    }
}
