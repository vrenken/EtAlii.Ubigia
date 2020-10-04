﻿namespace EtAlii.Ubigia.Tests.UnitTests
{
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    public class ComponentHelperTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentHelper_CompositeComponent_GetName()
        {
            var component = new ChildrenComponent();
            var name = ComponentHelper.GetName(component);
            Assert.Equal(@"Children", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentHelper_Generic_CompositeComponent_GetName()
        {
            var name = ComponentHelper.GetName<ChildrenComponent>();
            Assert.Equal(@"Children", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentHelper_NonCompositeComponent_GetName()
        {
            var component = new ParentComponent();
            var name = ComponentHelper.GetName(component);
            Assert.Equal(@"Parent", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentHelper_Generic_NonCompositeComponent_GetName()
        {
            var name = ComponentHelper.GetName<ParentComponent>();
            Assert.Equal(@"Parent", name);
        }
    }
}
