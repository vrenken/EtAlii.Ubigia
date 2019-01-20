namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;
    using Xunit.Abstractions;


    public partial class RootHandlerPathMatcherTests
    {
        private readonly ITestOutputHelper _output;

        public RootHandlerPathMatcherTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
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