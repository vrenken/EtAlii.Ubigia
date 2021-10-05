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
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    [CorrelateUnitTests]
    public sealed class FabricContextRootsTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private IFabricContext _fabricContext;
        private readonly FabricUnitTestContext _testContext;

        public FabricContextRootsTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }
        public async Task InitializeAsync()
        {
            var fabricOptions = await _testContext.Transport
                .CreateDataConnectionToNewSpace()
                .UseFabricContext()
                .UseDiagnostics()
                .ConfigureAwait(false);
            _fabricContext = Factory.Create<IFabricContext>(fabricOptions);
        }

        public Task DisposeAsync()
        {
            _fabricContext.Dispose();
            _fabricContext = null;
            return Task.CompletedTask;
        }

        [Fact]
        public async Task FabricContext_Roots_Add()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();

            // Act.
            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        [Fact(Skip = "Unknown reason")]
        public async Task FabricContext_Roots_Event_Added()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            Root root = null;

            // Act.
            var action = await ActionAssert
                .RaisesAsync<Guid>(
                m => _fabricContext.Roots.Added += m,
                m => _fabricContext.Roots.Added -= m,
                async () => root = await _fabricContext.Roots.Add(name).ConfigureAwait(false))
                .ConfigureAwait(false);

            // Assert.
            var addedId = action.Argument;
            Assert.NotNull(root);
            Assert.Equal(root.Id, addedId);
            Assert.NotEqual(Guid.Empty, addedId);
        }

        [Fact]
        public async Task FabricContext_Roots_Add_Multiple()
        {
            for (var i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();

                // Act.
                var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
            }
        }

        [Fact]
        public async Task FabricContext_Roots_Get_By_Id()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);

            // Act.
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        [Fact]
        public async Task FabricContext_Roots_Get_By_Name()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);

            // Act.
            root = await _fabricContext.Roots.Get(root.Name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        [Fact]
        public async Task FabricContext_Roots_Get_Multiple()
        {
            for (var i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);

                // Act.
                root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
            }
        }

        [Fact]
        public async Task FabricContext_Roots_Get_Multiple_First_Full_Add()
        {
            // Arrange.
            var roots = new List<Root>();
            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
                roots.Add(root);
            }

            foreach (var root in roots)
            {
                // Act.
                var retrievedRoot = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(retrievedRoot);
                Assert.Equal(root.Name, retrievedRoot.Name);
            }
        }
        [Fact]
        public async Task FabricContext_Roots_Get_No_Roots()
        {
            // Arrange.

            // Act.
            var retrievedRoots = await _fabricContext.Roots
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedRoots);
            Assert.Equal(SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Count());
        }

        [Fact]
        public async Task FabricContext_Roots_Get_All()
        {
            // Arrange.
            var roots = new List<Root>();
            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();

                var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);
                Assert.NotNull(root);
                Assert.Equal(name, root.Name);
                roots.Add(root);
            }

            // Act.
            var retrievedRoots = await _fabricContext.Roots
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

        [Fact]
        public async Task FabricContext_Roots_Change()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();

            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            name = Guid.NewGuid().ToString();

            // Act.
            root = await _fabricContext.Roots.Change(root.Id, name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
        }

        // TODO: The roots changed event should be raised, right?
        [Fact(Skip = "Unknown reason")]
        public async Task FabricContext_Roots_Event_Changed()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);
            name = Guid.NewGuid().ToString();

            // Act.
            var action = await ActionAssert
                .RaisesAsync<Guid>(
                    m => _fabricContext.Roots.Changed += m,
                    m => _fabricContext.Roots.Changed -= m,
                    async () => root = await _fabricContext.Roots.Change(root.Id, name).ConfigureAwait(false))
                .ConfigureAwait(false);

            // Assert.
            var changedId = action.Argument;
            Assert.NotEqual(Guid.Empty, changedId);
            Assert.Equal(root.Id, changedId);
        }


        [Fact]
        public async Task FabricContext_Roots_Remove()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);
            Assert.NotNull(root);
            Assert.Equal(name, root.Name);
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);

            // Act.
            await _fabricContext.Roots.Remove(root.Id).ConfigureAwait(false);

            // Assert.
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.Null(root);
        }

        //[Fact]
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
        [Fact]
        public async Task FabricContext_Roots_Delete_Non_Existing()
        {
            // Arrange.
            var id = Guid.NewGuid();

            // Act.
            var act = new Func<Task>(async () => await _fabricContext.Roots.Remove(id).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        [Fact]
        public async Task FabricContext_Roots_Change_Non_Existing()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // Act.
            var act = new Func<Task>(async () => await _fabricContext.Roots.Change(id, name).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }

        //[Fact]
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
        //[Fact]
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
        //[Fact]
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
        //[Fact]
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
        //[Fact]
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
        [Fact]
        public async Task FabricContext_Roots_Add_Already_Existing_Storage()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var root = await _fabricContext.Roots.Add(name).ConfigureAwait(false);
            Assert.NotNull(root);

            // Act.
            var act = new Func<Task>(async () => await _fabricContext.Roots.Add(name).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }
    }
}
