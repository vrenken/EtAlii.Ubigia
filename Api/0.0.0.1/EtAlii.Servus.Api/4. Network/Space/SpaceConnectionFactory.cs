namespace EtAlii.Servus.Api.Transport
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    public class SpaceConnectionFactory : ISpaceConnectionFactory
    {
        public ISpaceConnection Create(ISpaceConnectionConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = configuration.Transport
                .CreateScaffolding()
                .Concat(new IScaffolding[]
            {
                new SpaceConnectionScaffolding(configuration.Transport),
                new InfrastructureScaffolding(configuration.Client),
            })
            .ToArray();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            // No extensions on the Space connection (yet).
            //foreach (var extension in configuration.Extensions)
            //{
            //    extension.Initialize(container);
            //}

            var connection = container.GetInstance<ISpaceConnection>();
            return connection;
        }
    }
}
