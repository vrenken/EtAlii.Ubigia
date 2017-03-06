namespace EtAlii.Ubigia.Api.Transport.WebApi
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
