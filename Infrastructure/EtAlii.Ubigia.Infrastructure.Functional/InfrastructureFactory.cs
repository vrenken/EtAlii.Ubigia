namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureFactory : IInfrastructureFactory
    {
        public IInfrastructure Create(IInfrastructureConfiguration configuration)
        {
            if (String.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new NotSupportedException("The name is required to construct a Infrastructure instance");
            }
            if (configuration.Address == null)
            {
                throw new NotSupportedException("The address is required to construct a Infrastructure instance");
            }
            if (configuration.SystemConnectionCreationProxy == null)
            {
                throw new NotSupportedException("A SystemConnectionCreationProxy is required to construct a Infrastructure instance");
            }

            var container = new Container(true); // TODO: Injecting the container itself should not be done.
            
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

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return configuration.GetInfrastructure(container);

        }
    }
}