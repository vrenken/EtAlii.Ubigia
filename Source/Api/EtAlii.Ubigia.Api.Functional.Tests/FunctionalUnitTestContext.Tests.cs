// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class FunctionalUnitTestContextTests
    {
        private async Task<FunctionalOptions> InitializeAsync(FunctionalUnitTestContext context)
        {
            var options = await new FunctionalOptions(context.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(context, true)
                .ConfigureAwait(false);

            var (_, traversalContext) = context.CreateComponent<IGraphContext, ITraversalContext>(options);

            var scope = new ExecutionScope();
            await context.Functional
                .AddPeople(traversalContext, scope)
                .ConfigureAwait(false);
            await context.Functional
                .AddAddresses(traversalContext, scope)
                .ConfigureAwait(false);

            return options;
        }

        private async Task DisposeAsync(FunctionalOptions options)
        {
            await options.LogicalContext
                .DisposeAsync()
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task FunctionalUnitTestContext_Create_Empty()
        {
            // Arrange.
            var context = new FunctionalUnitTestContext();
            var start = DateTime.Now;

            // Act.
            try
            {
                await context.InitializeAsync().ConfigureAwait(false);

            }
            finally
            {
                await context.DisposeAsync().ConfigureAwait(false);
            }

            var end = DateTime.Now;
            var duration = end - start;

            // Assert.
            Assert.NotNull(context);
            Assert.True(duration.TotalMinutes < 3);
        }

        [Fact]
        public async Task FunctionalUnitTestContext_Create_Complete()
        {
            // Arrange.
            var context = new FunctionalUnitTestContext();
            var start = DateTime.Now;

            // Act.
            try
            {
                await context.InitializeAsync().ConfigureAwait(false);
                var options = await InitializeAsync(context).ConfigureAwait(false);

                await DisposeAsync(options).ConfigureAwait(false);
            }
            finally
            {
                await context.DisposeAsync().ConfigureAwait(false);
            }

            var end = DateTime.Now;
            var duration = end - start;

            // Assert.
            Assert.NotNull(context);
            Assert.True(duration.TotalMinutes < 3);
        }
    }
}
