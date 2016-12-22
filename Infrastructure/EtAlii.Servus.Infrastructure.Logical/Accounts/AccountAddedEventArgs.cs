namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

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