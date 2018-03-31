namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureConfiguration : IInfrastructureConfiguration
    {
        public IInfrastructureExtension[] Extensions { get; private set; }

        public ILogicalContext Logical { get; private set; }

        public string Name { get; private set; }

        public Uri Address { get; private set; }

        public Func<Container, Func<Container, object>[], object>[] ComponentManagerFactories { get; private set; }

        public Func<Container, object>[] ComponentFactories { get; private set; }

        public ISystemConnectionCreationProxy SystemConnectionCreationProxy { get; }

        private Func<Container, IInfrastructure> _getInfrastructure;

        public InfrastructureConfiguration(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            Extensions = new IInfrastructureExtension[0];
            SystemConnectionCreationProxy = systemConnectionCreationProxy;
            ComponentManagerFactories = new Func<Container, Func<Container, object>[], object>[0];
            ComponentFactories = new Func<Container, object>[0];
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

            Extensions = extensions
                .Concat(Extensions)
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

            ComponentManagerFactories = ComponentManagerFactories
                .Concat(new[] { componentManagerFactory })
                .Distinct()
                .ToArray();
            return this;
        }

        public IInfrastructureConfiguration Use<TComponentInterface, TComponent>()
            where TComponent: class, TComponentInterface
        {
            ComponentFactories = ComponentFactories
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

        public IInfrastructureConfiguration Use(string name, Uri address)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

			Name = name;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        public IInfrastructureConfiguration Use(ILogicalContext logical)
        {
	        Logical = logical ?? throw new ArgumentException(nameof(logical));

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
