namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Storage.InMemory;
    using SimpleInjector;

    public abstract class ManagementTestBase : InfrastructureTestBase
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override Container CreateInfrastructureContainer()
        {
            InfrastructureManagementTestApp.Setup<InfrastructureManagementTestApp, InMemoryStorageSystem>(true);
            return InfrastructureManagementTestApp.Current.Container;
        }
    }
}
