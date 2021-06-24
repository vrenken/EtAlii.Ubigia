// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public partial class RootHandlerPathMatcherTests
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
