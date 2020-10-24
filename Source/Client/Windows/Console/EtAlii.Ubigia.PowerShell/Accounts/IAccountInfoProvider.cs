﻿namespace EtAlii.Ubigia.PowerShell.Accounts
{
    using System;

    public interface IAccountInfoProvider 
    {
        string AccountName { get; }
        Account Account { get; }
        Guid AccountId { get; }

        Storage TargetStorage { get; }
    }
}