namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;


    public partial class RootHandlerPathMatcher_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_New()
        {
            // Arrange.

            // Act.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();

            // Assert.
            Assert.NotNull(rootHandlerPathMatcher);
        }

        private IRootHandlerPathMatcher CreateRootHandlerPathMatcher()
        {
            var container = new Container();
            new RootProcessingScaffolding(null).Register(container);
            return container.GetInstance<IRootHandlerPathMatcher>();
        }
    }
}