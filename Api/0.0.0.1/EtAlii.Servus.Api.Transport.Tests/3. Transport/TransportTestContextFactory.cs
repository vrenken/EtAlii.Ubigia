namespace EtAlii.Servus.Api.Transport.Tests
{
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class TransportTestContextFactory
    {
        public ITransportTestContext Create<TTransportTestContext>()
            where TTransportTestContext : ITransportTestContext
        {
            var container = new Container();

            container.Register<ITransportTestContext, TTransportTestContext>();
            container.Register<IHostTestContextFactory, HostTestContextFactory>();

            return container.GetInstance<ITransportTestContext>();
        }
    }
}
