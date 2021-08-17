// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class TraversalContextFactoryFunctionHandlersTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public TraversalContextFactoryFunctionHandlersTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create()
        {
            // Arrange.
            var options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_None()
        {
            // Arrange.
            var options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_Single()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new TestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .Use(functionHandlersProvider)
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(scriptContext);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task TraversalContextFactory_Create_With_FunctionHandler_Single_Invalid()
        {
            // Arrange.
            var functionHandlers = new IFunctionHandler[] { new InvalidTestRenameFunctionHandler() };
            var functionHandlersProvider = new FunctionHandlersProvider(functionHandlers);

            var options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .Use(functionHandlersProvider)
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                new TraversalContextFactory().Create(options);
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

            var options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .Use(functionHandlersProvider)
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var scriptContext = new TraversalContextFactory().Create(options);

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

            var options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .Use(functionHandlersProvider)
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            // Act.
            var act = new Action(() =>
            {
                new TraversalContextFactory().Create(options);
            });

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
