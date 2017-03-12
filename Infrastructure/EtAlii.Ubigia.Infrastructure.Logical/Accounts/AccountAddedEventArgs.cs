namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public class AccountAddedEventArgs : EventArgs
    {
        public AccountAddedEventArgs(Account account, AccountTemplate template)
        {
            Account = account;
            Template = template;
        }

        public Account Account { get; }
        public AccountTemplate Template { get; }
    }
}