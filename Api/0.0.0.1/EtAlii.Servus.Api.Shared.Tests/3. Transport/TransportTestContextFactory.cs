namespace EtAlii.Servus.Api.Transport.Tests
{
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class TransportTestContextFactory
    {
        public ITransportTestContext Create()
        {
            var container = new Container();

            container.Register<ITransportTestContext, TransportTestContext>();
            container.Register<IHostTestContextFactory, HostTestContextFactory>();

            return container.GetInstance<ITransportTestContext>();
        }
    }
}
