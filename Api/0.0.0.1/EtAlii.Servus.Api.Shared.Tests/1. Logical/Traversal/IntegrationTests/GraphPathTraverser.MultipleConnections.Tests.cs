namespace EtAlii.Servus.Api.Logical.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Fabric.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GraphPathTraverser_MultipleConnections_Tests
    {
        private IDiagnosticsConfiguration _diagnostics;
        private static IFabricTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new FabricTestContextFactory().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(() =>
            {
                _diagnostics = TestDiagnostics.Create();
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                _diagnostics = null;
            });
            task.Wait();
        }

        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Create()
        {
            // Arrange.
            var fabric = await _testContext.CreateFabricContext(true);
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
                .Use(fabric);

            // Act.
            var traverser = new GraphPathTraverserFactory().Create(configuration);

            // Assert.
            Assert.IsNotNull(traverser);
        }

        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);

            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.BreadthFirst, scope);

            // Assert.
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(hierarchy[depth - 1], result.First().Type);
        }

        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.DepthFirst, scope);

            // Assert.
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(hierarchy[depth - 1], result.First().Type);
        }






        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Wrong_Path()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.BreadthFirst, scope);

            // Assert.
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Wrong_Path()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.DepthFirst, scope);

            // Assert.
            Assert.AreEqual(0, result.Count());
        }







        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Too_Short()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.BreadthFirst, scope);

            // Assert.
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(hierarchy[depth - 2], result.First().Type);
        }

        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Too_Short()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.DepthFirst, scope);

            // Assert.
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(hierarchy[depth - 2], result.First().Type);
        }





        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_BreadthFirst_Too_Long()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.BreadthFirst, scope);

            // Assert.
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GraphPathTraverser_MultipleConnections_Traverse_Time_DepthFirst_Too_Long()
        {
            // Arrange.
            const int depth = 5;
            var scope = new ExecutionScope(false);
            var fabric = await _testContext.CreateFabricContext(true);
            var communicationsRoot = await fabric.Roots.Get("Communications");
            var communicationsEntry = (IEditableEntry)await fabric.Entries.Get(communicationsRoot, scope);

            var hierarchyResult = await _testContext.CreateHierarchy(fabric, communicationsEntry, depth);
            var entry = hierarchyResult.Item1;
            var hierarchy = hierarchyResult.Item2;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_diagnostics)
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
            var result = await traverser.Traverse(path, Traversal.DepthFirst, scope);

            // Assert.
            Assert.AreEqual(0, result.Count());
        }
    }
}