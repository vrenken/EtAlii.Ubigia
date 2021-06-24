// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureFactory : IInfrastructureFactory
    {
        public IInfrastructure Create(InfrastructureConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new NotSupportedException("The name is required to construct a Infrastructure instance");
            }

            var serviceDetails = configuration.ServiceDetails.Single(sd => sd.IsSystemService);
            if (serviceDetails == null)
            {
                throw new NotSupportedException("No system service details found. These are required to construct a Infrastructure instance");
            }
            if (serviceDetails.ManagementAddress == null)
            {
                throw new NotSupportedException("The management address is required to construct a Infrastructure instance");
            }
            if (serviceDetails.DataAddress == null)
            {
                throw new NotSupportedException("The data address is required to construct a Infrastructure instance");
            }
            if (configuration.SystemConnectionCreationProxy == null)
            {
                throw new NotSupportedException("A SystemConnectionCreationProxy is required to construct a Infrastructure instance");
            }

            var container = new Container();
            var scaffoldings = new IScaffolding[]
            {
                new InfrastructureScaffolding(configuration), 
                new DataScaffolding(),
                new ManagementScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.GetExtensions<IInfrastructureExtension>())
            {
                extension.Initialize(container);
            }

            return configuration.GetInfrastructure(container);

        }
    }
}