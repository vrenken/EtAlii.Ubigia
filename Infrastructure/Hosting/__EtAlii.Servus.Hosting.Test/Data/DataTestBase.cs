namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Storage.InMemory;
    using SimpleInjector;

    public abstract class DataTestBase : InfrastructureTestBase
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override Container CreateInfrastructureContainer()
        {
            InfrastructureDataTestApp.Setup<InfrastructureDataTestApp, InMemoryStorageSystem>(true);
            return InfrastructureDataTestApp.Current.Container;
        }
    }
}
