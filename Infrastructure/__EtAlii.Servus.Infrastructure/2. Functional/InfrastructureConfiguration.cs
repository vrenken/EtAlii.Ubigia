namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.Transport;
    using SimpleInjector;

    public class InfrastructureConfiguration : IInfrastructureConfiguration
    {
        public IInfrastructureExtension[] Extensions { get { return _extensions; } }
        private IInfrastructureExtension[] _extensions;

        public ILogicalContext Logical { get { return _logical; } }
        private ILogicalContext _logical;

        public string Name { get { return _name; } }
        private string _name;

        public string Address { get { return _address; } }
        private string _address;

        public string Account { get { return _account; } }
        private string _account;

        public string Password { get { return _password; } }
        private string _password;

        public Type[] ComponentManagerTypes { get { return _componentManagerTypes; } }
        private Type[] _componentManagerTypes;

        public ISystemConnectionCreationProxy SystemConnectionCreationProxy { get { return _systemConnectionCreationProxy; } }
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        private Func<Container, IInfrastructure> _getInfrastructure;

        public InfrastructureConfiguration(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _extensions = new IInfrastructureExtension[0];
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
            _componentManagerTypes = new Type[0];
        }

        public IInfrastructure GetInfrastructure(Container container)
        {
            return _getInfrastructure(container);
        }

        public IInfrastructureConfiguration Use(IInfrastructureExtension[] extensions)
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

        public IInfrastructureConfiguration Use(Type componentManagerType)
        {
            if (componentManagerType == null)
            {
                throw new ArgumentException(nameof(componentManagerType));
            }

            _componentManagerTypes = _componentManagerTypes
                .Concat(new [] { componentManagerType })
                .Distinct()
                .ToArray();
            return this;
        }

        public IInfrastructureConfiguration Use(string name, string address, string account, string password)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }
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

            _name = name;
            _address = address;
            _account = account;
            _password = password;
            return this;
        }

        public IInfrastructureConfiguration Use(ILogicalContext logical)
        {
            if (logical == null)
            {
                throw new ArgumentException(nameof(logical));
            }

            _logical = logical;

            return this;
        }

        public IInfrastructureConfiguration Use<TInfrastructure>()
            where TInfrastructure : class, IInfrastructure
        {
            if (_getInfrastructure != null)
            {
                throw new InvalidOperationException("GetInfrastructure already set.");
            }

            _getInfrastructure = container =>
            {
                container.Register<IInfrastructure, TInfrastructure>(Lifestyle.Singleton);
                return container.GetInstance<IInfrastructure>();
            };

            return this;
        }

    }
}
