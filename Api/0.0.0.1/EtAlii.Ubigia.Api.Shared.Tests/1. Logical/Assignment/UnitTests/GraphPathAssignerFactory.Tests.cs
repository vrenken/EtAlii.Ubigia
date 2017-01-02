namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class GraphPathAssignerFactory_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathAssignerFactory_New()
        {
            // Arrange.

            // Act.
            var factory = new GraphPathAssignerFactory();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathAssignerFactory_Create()
        {
            // Arrange.
            var factory = new GraphPathAssignerFactory();

            // Act.
            var assigner = factory.Create(null);

            // Assert.
            Assert.NotNull(assigner);
        }
    }
}
