namespace EtAlii.Servus.Api.Management
{
    using EtAlii.xTechnology.MicroContainer;

    public class ManagementFabricContextFactory
    {
        public IManagementFabricContext Create(IManagementConnection connection)
        {
            var container = new Container();
            container.Register<IManagementFabricContext, ManagementFabricContext>();

            //container.RegisterSingle<IStorageContext, StorageContext>();
            //container.RegisterSingle<IAccountContext, AccountContext>();
            //container.RegisterSingle<ISpaceContext, SpaceContext>();

            return container.GetInstance<IManagementFabricContext>();
        }
    }
}
