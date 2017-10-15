namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using SimpleInjector;

    public class InfrastructureFactory : IInfrastructureFactory
    {
        public InfrastructureFactory()
        {
        }

        public IInfrastructure Create(IInfrastructureConfiguration configuration)
        {
            if (String.IsNullOrWhiteSpace(configuration.Name))
            {
                throw new NotSupportedException("The name is required to construct a Infrastructure instance");
            }
            if (String.IsNullOrWhiteSpace(configuration.Address))
            {
                throw new NotSupportedException("The address is required to construct a Infrastructure instance");
            }
            if (String.IsNullOrWhiteSpace(configuration.Account))
            {
                throw new NotSupportedException("The account is required to construct a Infrastructure instance");
            }
            if (String.IsNullOrWhiteSpace(configuration.Password))
            {
                throw new NotSupportedException("The password is required to construct a Infrastructure instance");
            }
            if (configuration.SystemConnectionCreationProxy == null)
            {
                throw new NotSupportedException("A SystemConnectionCreationProxy is required to construct a Infrastructure instance");
            }

            var container = new Container();
            //container.ResolveUnregisteredType += (sender, args) => { throw new InvalidOperationException("Unregistered type found: " + args.UnregisteredServiceType.FullName); };
            

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