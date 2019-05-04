namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public class DataConnectionConfiguration : Configuration<IDataConnectionExtension, DataConnectionConfiguration>, IDataConnectionConfiguration
    {
        public ITransportProvider TransportProvider { get; private set; }

        public Func<IDataConnection> FactoryExtension { get; private set; }

        public Uri Address { get; private set; }

        public string AccountName { get; private set; }

        public string Password { get; private set; }

        public string Space { get; private set; }
        
        public IDataConnectionConfiguration Use(Func<IDataConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        public IDataConnectionConfiguration Use(ITransportProvider transportProvider)
        {
            if (TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this DataConnectionConfiguration",
                    nameof(transportProvider));
            }
            if (transportProvider == null)
            {
                throw new ArgumentNullException(nameof(transportProvider));
            }

            TransportProvider = transportProvider;

            return this;
        }

        public IDataConnectionConfiguration Use(Uri address)
        {
			if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this DataConnectionConfiguration");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        public IDataConnectionConfiguration Use(string accountName, string space, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this DataConnectionConfiguration");
            }
            //if [string.IsNullOrWhiteSpace[password]]
            //[
            //    throw new ArgumentException(nameof(password))
            //]
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this DataConnectionConfiguration");
            }
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException(nameof(space));
            }
            if (Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this DataConnectionConfiguration");
            }

            AccountName = accountName;
            Password = password;
            Space = space;
            return this;
        }
    }
}
