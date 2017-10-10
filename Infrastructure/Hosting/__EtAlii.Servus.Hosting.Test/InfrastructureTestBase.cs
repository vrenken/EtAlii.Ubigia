namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimpleInjector;

    [TestClass]
    public abstract class InfrastructureTestBase
    {
        protected Container InfrastructureContainer { get { return _infrastructureContainer; } private set { _infrastructureContainer = value; } }
        private Container _infrastructureContainer;

        public virtual void Initialize()
        {
            InfrastructureContainer = CreateInfrastructureContainer();
        }

        public virtual void Cleanup()
        {
        }

        protected abstract Container CreateInfrastructureContainer();
    }
}
