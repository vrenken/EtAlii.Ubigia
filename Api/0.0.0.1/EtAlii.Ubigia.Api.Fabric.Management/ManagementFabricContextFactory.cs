namespace EtAlii.Ubigia.Api.Fabric.Management
{
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.MicroContainer;

    public class ManagementFabricContextFactory
    {
        public IManagementFabricContext Create(IManagementConnection connection)
        {
            var container = new Container();
            container.Register<IManagementFabricContext, ManagementFabricContext>();

            return container.GetInstance<IManagementFabricContext>();
        }
    }
}
