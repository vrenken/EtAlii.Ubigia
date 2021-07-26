// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public sealed class FabricContextRootsTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabric;
        private readonly FabricUnitTestContext _testContext;

        public FabricContextRootsTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }
        public async Task InitializeAsync()
        {
            var connection = await _testContext.TransportTestContext.CreateDataConnectionToNewSpace().ConfigureAwait(false);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection)
                .UseFabricDiagnostics(TestConfiguration.Root);
            _fabric = new FabricContextFactory().Create(fabricContextConfiguration);
        }

        public Task DisposeAsync()
        {
            _fabric.Dispose();
            _fabric = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Add()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();

            // Act.
            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Event_Added()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            Root root = null;

            // Act.
            var action = await ActionAssert
                .RaisesAsync<Guid>(
                m => _fabric.Roots.Added += m,
                m => _fabric.Roots.Added -= m,
                async () => root = await _fabric.Roots.Add(name).ConfigureAwait(false))
                .ConfigureAwait(false);

            // Assert.
            var addedId = action.Argument;
            Assert.NotNull(root);
            Assert.Equal(root.Id, addedId);
            Assert.NotEqual(Guid.Empty, addedId);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Add_Multiple()
        {
            for (var i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();

                // Act.
                var root = await _fabric.Roots.Add(name).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_By_Id()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);

            // Act.
            root = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_By_Name()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);

            // Act.
            root = await _fabric.Roots.Get(root.Name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_Multiple()
        {
            for (var i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var root = await _fabric.Roots.Add(name).ConfigureAwait(false);

                // Act.
                root = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_Multiple_First_Full_Add()
        {
            // Arrange.
            var roots = new List<Root>();
            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var root = await _fabric.Roots.Add(name).ConfigureAwait(false);
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
                roots.Add(root);
            }

            foreach (var root in roots)
            {
                // Act.
                var retrievedRoot = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(retrievedRoot);
                Assert.Equal(root.Name, retrievedRoot.Name);
            }
        }
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_No_Roots()
        {
            // Arrange.

            // Act.
            var retrievedRoots = await _fabric.Roots
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedRoots);
            Assert.Equal(SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Count());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Get_All()
        {
            // Arrange.
            var roots = new List<Root>();
            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = await _fabric.Roots.Add(name).ConfigureAwait(false);
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
                roots.Add(root);
            }

            // Act.
            var retrievedRoots = await _fabric.Roots
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Equal(roots.Count + SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Count());
            foreach (var root in roots)
            {
                var matchingRoot = retrievedRoots.Single(r => r.Id == root.Id);
                Assert.NotNull(matchingRoot);
                Assert.Equal(root.Name, matchingRoot.Name);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Change()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();

            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            root = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            name = Guid.NewGuid().ToString();

            // Act.
            root = await _fabric.Roots.Change(root.Id, name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            root = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        // TODO: The roots changed event should be raised, right?
        [Fact(Skip = "Unknown reason"), Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Event_Changed()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);
            name = Guid.NewGuid().ToString();

            // Act.
            var action = await ActionAssert
                .RaisesAsync<Guid>(
                    m => _fabric.Roots.Changed += m,
                    m => _fabric.Roots.Changed -= m,
                    async () => root = await _fabric.Roots.Change(root.Id, name).ConfigureAwait(false))
                .ConfigureAwait(false);

            // Assert.
            var changedId = action.Argument;
            Assert.NotEqual(Guid.Empty, changedId);
            Assert.Equal(root.Id, changedId);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Remove()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            root = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);

            // Act.
            await _fabric.Roots.Remove(root.Id).ConfigureAwait(false);

            // Assert.
            root = await _fabric.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.Null(root);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_Roots_Event_Removed()
        //[
        //    var name = Guid.NewGuid().ToString()

        //    var root = await connection.Roots.Add(name)

        //    var removedEvent = new ManualResetEvent(false)
        //    var removedId = Guid.Empty;

        //    connection.Roots.Removed += (id) => [ removedId = id; removedEvent.Set(); ]
        //    await connection.Roots.Remove(root.Id)

        //    removedEvent.WaitOne(TimeSpan.FromSeconds(10))

        //    Assert.NotEqual(Guid.Empty, removedId)
        //    Assert.NotEqual(root.Id, removedId)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Delete_Non_Existing()
        {
            // Arrange.
            var id = Guid.NewGuid();

            // Act.
            var act = new Func<Task>(async () => await _fabric.Roots.Remove(id).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Change_Non_Existing()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // Act.
            var act = new Func<Task>(async () => await _fabric.Roots.Change(id, name).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_Roots_Add_With_Closed_Connection()
        //[
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false)
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection)
        //        .Use(DiagnosticsConfiguration.Default)


        //    // Act.
        //    var act = (Action)(() => [ var fabric = new FabricContextFactory().Create(fabricContextConfiguration); ])
        //    //var act = fabric.Roots.Add(Guid.NewGuid().ToString())

        //    // Assert.
        //    Assert.Throws<InvalidInfrastructureOperationException>(act)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_Roots_Get_With_Closed_Connection()
        //[
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false)
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection)
        //        .Use(DiagnosticsConfiguration.Default)

        //    // Act.
        //    var act = (Action)(() => [ var fabric = new FabricContextFactory().Create(fabricContextConfiguration); ])
        //    //var act = fabric.Roots.Get(Guid.NewGuid())

        //    // Assert.
        //    Assert.Throws<InvalidInfrastructureOperationException>(act)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_Roots_Remove_With_Closed_Connection()
        //[
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false)
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection)
        //        .Use(DiagnosticsConfiguration.Default)

        //    // Act.
        //    var act = (Action)(() => [ var fabric = new FabricContextFactory().Create(fabricContextConfiguration); ])
        //    //var act = fabric.Roots.Remove(Guid.NewGuid())

        //    // Assert.
        //    Assert.Throws<InvalidInfrastructureOperationException>(act)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_Roots_GetAll_With_Closed_Connection()
        //[
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false)
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection)
        //        .Use(DiagnosticsConfiguration.Default)

        //    // Act.
        //    var act = (Action)(() => [ var fabric = new FabricContextFactory().Create(fabricContextConfiguration); ])
        //    //var act = fabric.Roots.GetAll()

        //    // Assert.
        //    Assert.Throws<InvalidInfrastructureOperationException>(act)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public async Task FabricContext_Roots_Change_With_Closed_Connection()
        //[
        //    // Arrange.
        //    var connection = await _testContext.CreateDataConnection(false)
        //    var fabricContextConfiguration = new FabricContextConfiguration()
        //        .Use(connection)
        //        .Use(DiagnosticsConfiguration.Default)

        //    // Act.
        //    var act = (Action)(() => [ var fabric = new FabricContextFactory().Create(fabricContextConfiguration); ])
        //    //act = fabric.Roots.Change(Guid.NewGuid(), Guid.NewGuid().ToString())

        //    // Assert.
        //    Assert.Throws<InvalidInfrastructureOperationException>(act)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task FabricContext_Roots_Add_Already_Existing_Storage()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabric.Roots.Add(name).ConfigureAwait(false);
            Assert.NotNull(root);

            // Act.
            var act = new Func<Task>(async () => await _fabric.Roots.Add(name).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }
    }
}
