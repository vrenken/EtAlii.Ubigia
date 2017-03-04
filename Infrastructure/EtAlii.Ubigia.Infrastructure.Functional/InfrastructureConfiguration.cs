namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureConfiguration : IInfrastructureConfiguration
    {
        public IInfrastructureExtension[] Extensions => _extensions;
        private IInfrastructureExtension[] _extensions;

        public ILogicalContext Logical => _logical;
        private ILogicalContext _logical;

        public string Name => _name;
        private string _name;

        public string Address => _address;
        private string _address;

        public string Account => _account;
        private string _account;

        public string Password => _password;
        private string _password;

        public Func<Container, Func<Container, object>[], object>[] ComponentManagerFactories => _componentManagerFactories;
        private Func<Container, Func<Container, object>[], object>[] _componentManagerFactories;

        public Func<Container, object>[] ComponentFactories => _componentFactories;
        private Func<Container, object>[] _componentFactories;

        public ISystemConnectionCreationProxy SystemConnectionCreationProxy => _systemConnectionCreationProxy;
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        private Func<Container, IInfrastructure> _getInfrastructure;

        public InfrastructureConfiguration(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _extensions = new IInfrastructureExtension[0];
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
            _componentManagerFactories = new Func<Container, Func<Container, object>[], object>[0];
            _componentFactories = new Func<Container, object>[0];
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

        public IInfrastructureConfiguration Use(Func<Container, Func<Container, object>[], object> componentManagerFactory)
        {
            if (componentManagerFactory == null)
            {
                throw new ArgumentException(nameof(componentManagerFactory));
            }

            _componentManagerFactories = _componentManagerFactories
                .Concat(new[] { componentManagerFactory })
                .Distinct()
                .ToArray();
            return this;
        }

        public IInfrastructureConfiguration Use<TComponentInterface, TComponent>()
            where TComponent: class, TComponentInterface
        {
            _componentFactories = _componentFactories
                .Concat(new[] {
                    new Func<Container, object>((container) =>
                    {
                        container.Register<TComponentInterface, TComponent>();
                        return container.GetInstance<TComponentInterface>();
                    })
                })
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
                container.Register<IInfrastructure, TInfrastructure>();
                return container.GetInstance<IInfrastructure>();
            };

            return this;
        }

    }
}
