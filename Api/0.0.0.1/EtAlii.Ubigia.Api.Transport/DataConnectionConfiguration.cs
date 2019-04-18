namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Linq;

    public class DataConnectionConfiguration : IDataConnectionConfiguration
    {
        public IDataConnectionExtension[] Extensions { get; private set; }

        public ITransportProvider TransportProvider { get; private set; }

        public Func<IDataConnection> FactoryExtension { get; private set; }

        public Uri Address { get; private set; }

        public string AccountName { get; private set; }

        public string Password { get; private set; }

        public string Space { get; private set; }

        public DataConnectionConfiguration()
        {
            Extensions = new IDataConnectionExtension[0];
        }

        public IDataConnectionConfiguration Use(IDataConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

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
            if (String.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this DataConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password))
            //}
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this DataConnectionConfiguration");
            }
            if (String.IsNullOrWhiteSpace(space))
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
