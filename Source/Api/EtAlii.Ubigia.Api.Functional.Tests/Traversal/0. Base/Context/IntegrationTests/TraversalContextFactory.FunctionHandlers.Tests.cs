// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;

    public class TraversalContextFactoryFunctionHandlersTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;

        public TraversalContextFactoryFunctionHandlersTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create()
        {
            // Arrange.
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = _testContext.CreateComponent<ITraversalContext>(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = _testContext.CreateComponent<ITraversalContext>(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .Use(functionHandlersProvider)
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = _testContext.CreateComponent<ITraversalContext>(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new InvalidTestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .Use(functionHandlersProvider)
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                _testContext.CreateComponent<ITraversalContext>(options);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_Multiple()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestFormatFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .Use(functionHandlersProvider)
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = _testContext.CreateComponent<ITraversalContext>(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_Multiple_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[]
            {
                new TestRenameFunctionHandler(),
                new TestRenameFunctionHandler()
            };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .Use(functionHandlersProvider)
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                _testContext.CreateComponent<ITraversalContext>(options);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
