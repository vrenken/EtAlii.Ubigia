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

        public async Task DisposeAsync()
        {
            await _fabricContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _fabricContext = null;
        }

        [Fact]
        public async Task FabricContext_Roots_Add()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var rootType = new RootType(Guid.NewGuid().ToString());

            // Act.
            var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(rootType, root.Type);
        }

        [Fact]
        public async Task FabricContext_Roots_Add_Multiple()
        {
            for (var i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var rootType = new RootType(Guid.NewGuid().ToString());

                // Act.
                var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(root);
                // RCI2022: We want to make roots case insensitive.
                Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
                Assert.Equal(rootType, root.Type);
            }
        }

        [Fact]
        public async Task FabricContext_Roots_Get_By_Id()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var rootType = new RootType(Guid.NewGuid().ToString());
            var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

            // Act.
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(rootType, root.Type);
        }

        [Fact]
        public async Task FabricContext_Roots_Get_By_Name()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var rootType = new RootType(Guid.NewGuid().ToString());
            var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

            // Act.
            root = await _fabricContext.Roots.Get(root.Name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(rootType, root.Type);
        }

        [Fact]
        public async Task FabricContext_Roots_Get_Multiple()
        {
            for (var i = 0; i < 10; i++)
            {
                // Arrange.
                var name = Guid.NewGuid().ToString();
                var rootType = new RootType(Guid.NewGuid().ToString());
                var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

                // Act.
                root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(root);
                // RCI2022: We want to make roots case insensitive.
                Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
                Assert.Equal(rootType, root.Type);
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
                var rootType = new RootType(Guid.NewGuid().ToString());
                var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

                Assert.NotNull(root);
                // RCI2022: We want to make roots case insensitive.
                Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
                Assert.Equal(rootType, root.Type);
                roots.Add(root);
            }

            foreach (var root in roots)
            {
                // Act.
                var retrievedRoot = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);

                // Assert.
                Assert.NotNull(retrievedRoot);
                // RCI2022: We want to make roots case insensitive.
                Assert.Equal(root.Name, retrievedRoot.Name, StringComparer.OrdinalIgnoreCase);
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
            Assert.Equal(SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Length);
        }

        [Fact]
        public async Task FabricContext_Roots_Get_All()
        {
            // Arrange.
            var roots = new List<Root>();
            for (var i = 0; i < 10; i++)
            {
                var name = Guid.NewGuid().ToString();
                var rootType = new RootType(Guid.NewGuid().ToString());
                var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

                Assert.NotNull(root);
                // RCI2022: We want to make roots case insensitive.
                Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
                Assert.Equal(rootType, root.Type);
                roots.Add(root);
            }

            // Act.
            var retrievedRoots = await _fabricContext.Roots
                .GetAll()
                .ToArrayAsync()
                .ConfigureAwait(false);

            // Assert.
            Assert.Equal(roots.Count + SpaceTemplate.Data.RootsToCreate.Length, retrievedRoots.Length);
            foreach (var root in roots)
            {
                var matchingRoot = retrievedRoots.Single(r => r.Id == root.Id);
                Assert.NotNull(matchingRoot);
                Assert.Equal(root.Name, matchingRoot.Name);
            }
        }

        [Fact]
        public async Task FabricContext_Roots_Change_Name()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var rootType = new RootType(Guid.NewGuid().ToString());
            var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(rootType, root.Type);
            name = Guid.NewGuid().ToString();

            // Act.
            root = await _fabricContext.Roots.Change(root.Id, name).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(rootType, root.Type);
        }

        [Fact]
        public async Task FabricContext_Roots_Remove()
        {
            // Arrange.
            var name = Guid.NewGuid().ToString();
            var rootType = new RootType(Guid.NewGuid().ToString());
            var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);

            Assert.NotNull(root);
            // RCI2022: We want to make roots case insensitive.
            Assert.Equal(name, root.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(rootType, root.Type);
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.NotNull(root);

            // Act.
            await _fabricContext.Roots.Remove(root.Id).ConfigureAwait(false);

            // Assert.
            root = await _fabricContext.Roots.Get(root.Id).ConfigureAwait(false);
            Assert.Null(root);
        }

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
            var rootType = new RootType(Guid.NewGuid().ToString());
            var root = await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false);
            Assert.NotNull(root);

            // Act.
            var act = new Func<Task>(async () => await _fabricContext.Roots.Add(name, rootType).ConfigureAwait(false));

            // Assert.
            await Assert.ThrowsAsync<InvalidInfrastructureOperationException>(act).ConfigureAwait(false);
        }
    }
}
