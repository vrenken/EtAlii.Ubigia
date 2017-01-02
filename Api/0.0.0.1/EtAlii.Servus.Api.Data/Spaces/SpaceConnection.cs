namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    public class SpaceConnection : ConnectionBase
    {
        internal new Storage CurrentStorage { get { return base.CurrentStorage; } }

        internal Space CurrentSpace { get; private set; }
        internal Account CurrentAccount { get; private set; }

        public Roots Roots { get { return _roots; } }
        private readonly Roots _roots;

        public Entries Entries { get { return _entries; } }
        private readonly Entries _entries; 

        public SpaceConnection()
            : base()
        {
            _roots = new Roots(this);
            _entries = new Entries(this);
        }

        public void Open(string address, string accountName, string password, string spaceName)
        {
            base.Open(address, accountName, password);

            if (CurrentAccount != null || CurrentSpace != null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.SpaceAlreadyOpen);
            }

            CurrentAccount = GetAccount(accountName);
            if (CurrentAccount == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectUsingAccount);
            }

            CurrentSpace = GetSpace(spaceName);
            if (CurrentSpace == null)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.UnableToConnectToSpace);
            }
        }

        public override void Close()
        {
            if (CurrentAccount == null || CurrentSpace == null)
            {
                throw new InvalidInfrastructureOperationException("The connection is already closed");
            }
            CurrentAccount = null;
            CurrentSpace = null;

            base.Close();
        }

        private Space GetSpace(string spaceName)
        {
            var address = AddressFactory.Create(CurrentStorage, Spaces.RelativePath, UriParameter.AccountId, CurrentAccount.Id.ToString());
            var spaces = Infrastructure.Get<IEnumerable<Space>>(address);
            return spaces.FirstOrDefault(s => s.Name == spaceName);
        }

        private Account GetAccount(string accountName)
        {
            var address = AddressFactory.Create(CurrentStorage, Accounts.RelativePath, UriParameter.AccountName, accountName);
            var account = Infrastructure.Get<Account>(address);
            if (account == null)
            {
                string message = String.Format("Unable to connect using the specified account({0})", accountName);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return account;
        }
    }
}
