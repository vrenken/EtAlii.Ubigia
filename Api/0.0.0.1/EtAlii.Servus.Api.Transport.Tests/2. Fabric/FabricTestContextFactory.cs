namespace EtAlii.Servus.Api.Fabric.Tests
{
    using EtAlii.Servus.Api.Transport.Tests;
    using EtAlii.xTechnology.MicroContainer;

    public class FabricTestContextFactory 
    {
        public IFabricTestContext Create()
        {
            var container = new Container();

            container.Register<IFabricTestContext, FabricTestContext>();
            container.Register<ITransportTestContext>(() => new TransportTestContext().Create());

            return container.GetInstance<IFabricTestContext>();
        }
    }
}