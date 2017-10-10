namespace EtAlii.Servus.Infrastructure.Test
{
    using EtAlii.Servus.Infrastructure.Shared;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimpleInjector;
    using System.Diagnostics.CodeAnalysis;

    [TestClass]
    public class TypeRegistrationEventArgs_Tests
    {
        [TestMethod]
        public void TypeRegistrationEventArgs_Create()
        {
            var e = new TypeRegistrationEventArgs(new Container());
            Assert.IsNotNull(e);
            Assert.IsNotNull(e.Container);
        }
    }
}
