// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    [CorrelateUnitTests]
    public class NodeToIdentifierAssignerTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public NodeToIdentifierAssignerTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task NodeToIdentifierAssigner_Create()
        {
            // Arrange.
            var logicalOptions = await _testContext.Fabric
                .CreateFabricOptions(true)
                .UseLogicalContext()
                .UseDiagnostics()
                .ConfigureAwait(false);
            var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);

            var updateEntryFactory = new UpdateEntryFactory(logicalOptions.FabricContext);

            // Act.
            var assigner = new NodeToIdentifierAssigner(updateEntryFactory, logicalOptions.FabricContext, traverser);

            // Assert.
            Assert.NotNull(assigner);
        }


        [Fact]
        public async Task NodeToIdentifierAssigner_Assign()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var logicalOptions = await _testContext.Fabric
                .CreateFabricOptions(true)
                .UseLogicalContext()
                .UseDiagnostics()
                .ConfigureAwait(false);
            var fabricContext = logicalOptions.FabricContext;
            var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);

            var updateEntryFactory = new UpdateEntryFactory(fabricContext);
            var assigner = new NodeToIdentifierAssigner(updateEntryFactory, fabricContext, traverser);

            var personRoot = await fabricContext.Roots.Get("Person").ConfigureAwait(false);
            var personEntry = (IEditableEntry)await fabricContext.Entries.Get(personRoot, scope).ConfigureAwait(false);

            var entry = await fabricContext.Entries.Prepare().ConfigureAwait(false);
            var node = new Node(entry);

            // Act.
            var newEntry = await assigner.Assign(node, personEntry.Id, scope).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(newEntry);
            Assert.NotEqual(newEntry.Id, entry.Id);
        }
    }
}
