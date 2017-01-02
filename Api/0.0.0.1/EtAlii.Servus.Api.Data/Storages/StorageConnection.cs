namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class StorageConnection : ConnectionBase
    {
        internal new Storage CurrentStorage { get { return base.CurrentStorage; } }

        public Storages Storages { get { return _storages; } }
        private readonly Storages _storages;

        public Accounts Accounts { get { return _accounts; } }
        private readonly Accounts _accounts;

        public Spaces Spaces { get { return _spaces; } }
        private readonly Spaces _spaces;

        public StorageConnection()
            : base()
        {
            _storages = new Storages(this);
            _accounts = new Accounts(this);
            _spaces = new Spaces(this);
        }

        public new void Open(string address, string accountName, string password)
        {
            base.Open(address, accountName, password);
        }
    }
}
