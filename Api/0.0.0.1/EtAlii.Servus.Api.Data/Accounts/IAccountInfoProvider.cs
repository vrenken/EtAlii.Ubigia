﻿namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;

    public interface IAccountInfoProvider 
    {
        string AccountName { get; }
        Account Account { get; }
        Guid AccountId { get; }

        Storage TargetStorage { get; }
    }
}
