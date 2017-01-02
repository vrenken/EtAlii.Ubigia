namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class AccountConnection
    {
        public Account Account { get; private set; }

        private readonly Infrastructure _infrastructure;

        public AccountConnection(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
        }
        public void Open(string address, string accountName, string password)
        {
            Account account = null;

            Account = null;
            Account = account;
        }
    }
}
