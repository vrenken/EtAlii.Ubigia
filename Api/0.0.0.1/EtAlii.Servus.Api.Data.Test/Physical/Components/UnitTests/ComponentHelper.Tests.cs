namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ComponentHelper_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ComponentHelper_CompositeComponent_GetName()
        {
            var component = new ChildrenComponent();
            var name = ComponentHelper.GetName(component);
            Assert.AreEqual(@"Children", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ComponentHelper_Generic_CompositeComponent_GetName()
        {
            var name = ComponentHelper.GetName<ChildrenComponent>();
            Assert.AreEqual(@"Children", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ComponentHelper_NonCompositeComponent_GetName()
        {
            var component = new ParentComponent();
            var name = ComponentHelper.GetName(component);
            Assert.AreEqual(@"Parent", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ComponentHelper_Generic_NonCompositeComponent_GetName()
        {
            var name = ComponentHelper.GetName<ParentComponent>();
            Assert.AreEqual(@"Parent", name);
        }
    }
}
