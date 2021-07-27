// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using Microsoft.Extensions.Configuration;

    public static class InfrastructureConfigurationUseExtensions
    {
        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration>(
            this TInfrastructureConfiguration configuration,
            IConfiguration configurationRoot,
            string name,
            ServiceDetails[] serviceDetails)
            where TInfrastructureConfiguration : InfrastructureConfiguration
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("No name specified", nameof(name));
            }

            if (!serviceDetails.Any())
            {
                throw new InvalidOperationException("No service details specified during infrastructure configuration");
            }
            if (serviceDetails.All(sd => !sd.IsSystemService))
            {
                throw new InvalidOperationException("No system service details specified during infrastructure configuration");
            }

            editableConfiguration.Root = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
            editableConfiguration.Name = name;
            editableConfiguration.ServiceDetails = serviceDetails;
            return configuration;
        }

        public static TInfrastructureConfiguration Use<TInfrastructureConfiguration>(this TInfrastructureConfiguration configuration, ILogicalContext logical)
            where TInfrastructureConfiguration : InfrastructureConfiguration
        {
            var editableConfiguration = (IEditableInfrastructureConfiguration) configuration;

            editableConfiguration.Logical = logical ?? throw new ArgumentException("No logical context specified", nameof(logical));

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
