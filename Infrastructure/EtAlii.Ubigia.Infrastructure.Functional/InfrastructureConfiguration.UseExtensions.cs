namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public static class InfrastructureConfigurationUseExtensions
    {
        // TODO: These two methods should be typed.
        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration>(this TInfrastructureConfiguration configuration, Func<Container, Func<Container, object>[], object> componentManagerFactory)
            where TInfrastructureConfiguration : InfrastructureConfiguration
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;

            if (componentManagerFactory == null)
            {
                throw new ArgumentException(nameof(componentManagerFactory));
            }

            editableConfiguration.ComponentManagerFactories = editableConfiguration.ComponentManagerFactories
                .Concat(new[] { componentManagerFactory })
                .Distinct()
                .ToArray();
            return configuration;
        }

        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration, TComponentInterface, TComponent>(this TInfrastructureConfiguration configuration)
            where TInfrastructureConfiguration : InfrastructureConfiguration
            where TComponent: class, TComponentInterface
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;

            editableConfiguration.ComponentFactories = editableConfiguration.ComponentFactories
                .Concat(new[] {
                    new Func<Container, object>((container) =>
                    {
                        container.Register<TComponentInterface, TComponent>();
                        return container.GetInstance<TComponentInterface>();
                    })
                })
                .Distinct()
                .ToArray();

            return configuration;
        }

        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration>(this TInfrastructureConfiguration configuration, string name, Uri address)
            where TInfrastructureConfiguration : InfrastructureConfiguration
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            editableConfiguration.Name = name;
            editableConfiguration.Address = address ?? throw new ArgumentNullException(nameof(address));
            return configuration;
        }

        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration>(this TInfrastructureConfiguration configuration, ILogicalContext logical)
            where TInfrastructureConfiguration : InfrastructureConfiguration
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;
	        
            editableConfiguration.Logical = logical ?? throw new ArgumentException(nameof(logical));

            return configuration;
        }

        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration, TInfrastructure>(this TInfrastructureConfiguration configuration)
            where TInfrastructureConfiguration : InfrastructureConfiguration
            where TInfrastructure : class, IInfrastructure
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;
            
            if (editableConfiguration.GetInfrastructure != null)
            {
                throw new InvalidOperationException("GetInfrastructure already set.");
            }

            editableConfiguration.GetInfrastructure = container =>
            {
                container.Register<IInfrastructure, TInfrastructure>();
                return container.GetInstance<IInfrastructure>();
            };

            return configuration;
        }

    }
}