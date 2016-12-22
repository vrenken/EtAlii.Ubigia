namespace EtAlii.Servus.Provisioning
{
    using System;
    using System.Linq;

    public class ProviderConfiguration : IProviderConfiguration
    {
        public IProviderExtension[] Extensions { get { return _extensions; } }
        private IProviderExtension[] _extensions;

        public ProviderComponent[] Components { get { return _components;  } }
        private ProviderComponent[] _components;

        //public IStorage Storage { get { return _storage; } }
        //private IStorage _storage;

        public string Address { get { return _address; } }
        private string _address;

        public string Account { get { return _account; } }
        private string _account;
        public string Password { get { return _password; } }
        private string _password;

        public ProviderConfiguration()
        {
            _extensions = new IProviderExtension[0];
            _components = new ProviderComponent[0];
        }

        public IProviderConfiguration Use(IProviderExtension[] extensions)
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

        public IProviderConfiguration Use(string address, string account, string password)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }
            if (String.IsNullOrWhiteSpace(account))
            {
                throw new ArgumentException(nameof(account));
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password));
            }

            _address = address;
            _account = account;
            _password = password;
            return this;
        }

        public IProviderConfiguration Use(ProviderComponent[] components)
        {
            if (components == null)
            {
                throw new ArgumentException(nameof(components));
            }
            _components = components;

            return this;
        }


        //public IProviderConfiguration Use(IStorage storage)
        //{
        //    if (storage == null)
        //    {
        //        throw new ArgumentException(nameof(storage));
        //    }

        //    _storage = storage;

        //    return this;
        //}
    }
}
