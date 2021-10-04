// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Logical;

    public static class InfrastructureOptionsUseExtensions
    {
        public static TInfrastructureOptions Use<TInfrastructureOptions>(
            this TInfrastructureOptions options,
            string name,
            ServiceDetails[] serviceDetails)
            where TInfrastructureOptions : InfrastructureOptions
        {
            var editableOptions = (IEditableInfrastructureOptions) options;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("No name specified", nameof(name));
            }

            if (!serviceDetails.Any())
            {
                throw new InvalidOperationException("No service details specified during infrastructure configuration");
            }

            editableOptions.Name = name;
            editableOptions.ServiceDetails = serviceDetails;
            return options;
        }

        public static TInfrastructureOptions Use<TInfrastructureOptions>(this TInfrastructureOptions options, ILogicalContext logical)
            where TInfrastructureOptions : InfrastructureOptions
        {
            var editableOptions = (IEditableInfrastructureOptions) options;

            editableOptions.Logical = logical ?? throw new ArgumentException("No logical context specified", nameof(logical));

            return options;
        }

        public static TInfrastructureOptions Use<TInfrastructureOptions, TInfrastructure>(this TInfrastructureOptions options)
            where TInfrastructureOptions : InfrastructureOptions
            where TInfrastructure : class, IInfrastructure
        {
            var editableOptions = (IEditableInfrastructureOptions) options;

            if (editableOptions.RegisterInfrastructureService != null)
            {
                throw new InvalidOperationException("RegisterInfrastructureService already set.");
            }

            editableOptions.RegisterInfrastructureService = container => container.Register<IInfrastructure, TInfrastructure>();

            return options;
        }
    }
}
