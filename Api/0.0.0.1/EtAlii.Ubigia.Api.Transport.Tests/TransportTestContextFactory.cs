namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests;

    public class TransportTestContextFactory
    {
        public TTransportTestContext Create<TTransportTestContext>()
            where TTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>, new()
        {
            return new TTransportTestContext();
            //var container = new Container();

            //container.Register<ITransportTestContext, TTransportTestContext>();
            //container.Register<IHostTestContextFactory, TransportHostTestContextFactory>();

            //return container.GetInstance<ITransportTestContext>();
        }
    }
}
