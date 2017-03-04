namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Linq;

    public class SpaceConnectionConfiguration : ISpaceConnectionConfiguration
    {
        public ISpaceTransport Transport => _transport;
        private ISpaceTransport _transport;

        public string Address => _address;
        private string _address;
        public string AccountName => _accountName;
        private string _accountName;
        public string Password => _password;
        private string _password;
        public string Space => _space;
        private string _space;

        public ISpaceConnectionExtension[] Extensions => _extensions;
        private ISpaceConnectionExtension[] _extensions;

        public SpaceConnectionConfiguration()
        {
            _extensions = new ISpaceConnectionExtension[0];
        }

        public ISpaceConnectionConfiguration Use(ISpaceConnectionExtension[] extensions)
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

        public ISpaceConnectionConfiguration Use(ISpaceTransport transport)
        {
            if (transport == null)
            {
                throw new ArgumentException(nameof(transport));
            }
            if (_transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this SpaceConnectionConfiguration");
            }

            _transport = transport;
            return this;
        }

        public ISpaceConnectionConfiguration Use(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }
            if (_address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this SpaceConnectionConfiguration");
            }

            _address = address;
            return this;
        }

        public ISpaceConnectionConfiguration Use(string accountName, string space, string password)
        {
            if (String.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (_accountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this SpaceConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password));
            //}
            if (_password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this SpaceConnectionConfiguration");
            }
            if (String.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException(nameof(space));
            }
            if (_space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this SpaceConnectionConfiguration");
            }

            _accountName = accountName;
            _password = password;
            _space = space;
            return this;
        }

    }
}