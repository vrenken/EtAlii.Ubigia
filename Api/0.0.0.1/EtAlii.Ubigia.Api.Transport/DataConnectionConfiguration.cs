namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Linq;

    public class DataConnectionConfiguration : IDataConnectionConfiguration
    {
        public IDataConnectionExtension[] Extensions => _extensions;
        private IDataConnectionExtension[] _extensions;

        public ITransportProvider TransportProvider => _transportProvider;
        private ITransportProvider _transportProvider;

        public Func<IDataConnection> FactoryExtension => _factoryExtension;
        private Func<IDataConnection> _factoryExtension;

        public string Address => _address;
        private string _address;

        public string AccountName => _accountName;
        private string _accountName;

        public string Password => _password;
        private string _password;

        public string Space => _space;
        private string _space;

        public DataConnectionConfiguration()
        {
            _extensions = new IDataConnectionExtension[0];
        }

        public IDataConnectionConfiguration Use(IDataConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IDataConnectionConfiguration Use(Func<IDataConnection> factoryExtension)
        {
            _factoryExtension = factoryExtension;
            return this;
        }

        public IDataConnectionConfiguration Use(ITransportProvider transportProvider)
        {
            if (_transportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this DataConnectionConfiguration",
                    nameof(transportProvider));
            }
            if (transportProvider == null)
            {
                throw new ArgumentNullException(nameof(transportProvider));
            }

            _transportProvider = transportProvider;

            return this;
        }

        public IDataConnectionConfiguration Use(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }
            if (_address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this DataConnectionConfiguration");
            }

            _address = address;
            return this;
        }

        public IDataConnectionConfiguration Use(string accountName, string space, string password)
        {
            if (String.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (_accountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this DataConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password));
            //}
            if (_password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this DataConnectionConfiguration");
            }
            if (String.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException(nameof(space));
            }
            if (_space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this DataConnectionConfiguration");
            }

            _accountName = accountName;
            _password = password;
            _space = space;
            return this;
        }
    }
}
