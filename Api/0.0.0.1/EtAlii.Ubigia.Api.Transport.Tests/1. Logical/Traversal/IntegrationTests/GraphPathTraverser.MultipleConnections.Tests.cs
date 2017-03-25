namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Tests;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using Xunit;

    
    public class GraphPathTraverserMultipleConnectionsTests : IClassFixture<FabricUnitTestContext>
    {
        private readonly FabricUnitTestContext _testContext;

        public GraphPathTraverserMultipleConnectionsTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Create()
        {
            // Arrange.
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);

            // Act.
            var traverser = new GraphPathTraverserFactory().Create(configuration);

            // Assert.
            Assert.NotNull(traverser);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(1, result.Length);
            Assert.Equal(hierarchy[depth - 1], result.First().Type);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(1, result.Length);
            Assert.Equal(hierarchy[depth - 1], result.First().Type);
        }






        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Wrong_Path()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy[3] = Guid.NewGuid().ToString();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Wrong_Path()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy[3] = Guid.NewGuid().ToString();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(0, result.Length);
        }







        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Too_Short()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy = hierarchy.Take(depth - 1).ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(1, result.Length);
            Assert.Equal(hierarchy[depth - 2], result.First().Type);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Too_Short()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            hierarchy = hierarchy.Take(depth - 1).ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(1, result.Length);
            Assert.Equal(hierarchy[depth - 2], result.First().Type);
        }





        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Too_Long()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            var newHierarchy = new List<string>(hierarchy);
            newHierarchy.Add(Guid.NewGuid().ToString());
            hierarchy = newHierarchy.ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Too_Long()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.FabricTestContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.FabricTestContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_testContext.DiagnosticsConfiguration)
                .Use(fabric);
            var traverser = new GraphPathTraverserFactory().Create(configuration);
            var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
            graphPathBuilder.Add(communicationsEntry.Id);
            var newHierarchy = new List<string>(hierarchy);
            newHierarchy.Add(Guid.NewGuid().ToString());
            hierarchy = newHierarchy.ToArray();
            foreach (var item in hierarchy)
            {
                graphPathBuilder = graphPathBuilder
                    .Add(GraphRelation.Child)
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
            Assert.Equal(0, result.Length);
        }
    }
}