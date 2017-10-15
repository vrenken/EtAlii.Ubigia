namespace EtAlii.Servus.Infrastructure
{
    using System;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.xTechnology.MicroContainer;

    public class SystemStorageConnectionFactory
    {
        public ISystemStorageConnection Create(ISystemStorageConnectionConfiguration configuration)
        {
            if (configuration.Infrastructure == null)
            {
                throw new NotSupportedException("A Infrastructure is required to construct a SystemStorageConnection instance");
            }
            //if (configuration.Transport == null)
            //{
            //    throw new NotSupportedException("A Transport is required to construct a SystemStorageConnection instance");
            //}

            var container = new Container();

            var scaffoldings = new EtAlii.xTechnology.MicroContainer.IScaffolding[]
            {
                //configuration.Transport.CreateScaffolding(),
                new SystemStorageConnectionScaffolding(configuration.Infrastructure, configuration.Transport),
                //new SystemConnectionScaffolding(configuration),
                //new SystemInfrastructureScaffolding(), 
                //new SystemConnectionClientsScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            var connection = container.GetInstance<ISystemStorageConnection>();
            return connection;
        }
    }
}
