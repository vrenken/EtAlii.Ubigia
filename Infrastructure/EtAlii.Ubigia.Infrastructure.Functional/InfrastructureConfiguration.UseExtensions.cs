namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public static class InfrastructureConfigurationUseExtensions
    {
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