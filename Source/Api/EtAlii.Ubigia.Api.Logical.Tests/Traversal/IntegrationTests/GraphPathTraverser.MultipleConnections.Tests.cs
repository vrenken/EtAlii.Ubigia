// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class GraphPathTraverserMultipleConnectionsTests : IClassFixture<LogicalUnitTestContext>
    {
        private readonly LogicalUnitTestContext _testContext;

        public GraphPathTraverserMultipleConnectionsTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Create()
        {
            // Arrange.
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);

            // Act.
            var traverser = new GraphPathTraverserFactory().Create(options);

            // Assert.
            Assert.NotNull(traverser);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);

            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Single(result);
            Assert.Equal(hierarchy[depth - 1], result.First().Type);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Single(result);
            Assert.Equal(hierarchy[depth - 1], result.First().Type);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Wrong_Path()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy[3] = Guid.NewGuid().ToString();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Empty(result);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Wrong_Path()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy[3] = Guid.NewGuid().ToString();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Empty(result);
        }







        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Too_Short()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy = hierarchy.Take(depth - 1).ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Single(result);
            Assert.Equal(hierarchy[depth - 2], result.First().Type);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Too_Short()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy = hierarchy.Take(depth - 1).ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Single(result);
            Assert.Equal(hierarchy[depth - 2], result.First().Type);
        }





        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Too_Long()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            var newHierarchy = new List<string>(hierarchy);
            newHierarchy.Add(Guid.NewGuid().ToString());
            hierarchy = newHierarchy.ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Empty(result);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Too_Long()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope();
            using var fabric = await _testContext.Fabric.CreateFabricContext(true).ConfigureAwait(false);
            var communicationsRoot = await fabric.Roots.Get("Communication").ConfigureAwait(false);
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope).ConfigureAwait(false);

            var hierarchyResult = await _testContext.Fabric.CreateHierarchy(fabric, communicationsEntry, depth).ConfigureAwait(false);
            var hierarchy = hierarchyResult.Item2;

            var options = new GraphPathTraverserOptions(_testContext.ClientConfiguration)
                .UseLogicalDiagnostics()
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(options);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            var newHierarchy = new List<string>(hierarchy);
            newHierarchy.Add(Guid.NewGuid().ToString());
            hierarchy = newHierarchy.ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Children)
                    .Add(item);
            }
            var path = graphPathBuilder.ToPath();

            // Act.
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.ToArray();

            // Assert.
            Assert.Empty(result);
        }
    }
}
