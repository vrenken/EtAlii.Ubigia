namespace EtAlii.Ubigia.Storage.Ntfs.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Storage.Ntfs.Tests;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;
    using System.Linq;
    using TestAssembly = EtAlii.Ubigia.Storage.Tests.TestAssembly;

    
    public class NtfsComponentStorage_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Prepare_Container()
        {
            // Arrange.
            var id = Guid.NewGuid().ToString();
            // ReSharper disable once UnusedVariable
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier(id);

            // Act.

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Entry()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var entry = StorageTestHelper.CreateEntry(storageId, accountId, spaceId, 0, 0, 0);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var entryComponent = new IdentifierComponent { Id = entry.Id };

            // Act.
            Storage.Components.Store(containerId, entryComponent);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_Entry()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalEntry = StorageTestHelper.CreateEntry(storageId, accountId, spaceId, 0, 0, 0);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var entryComponent = new IdentifierComponent { Id = originalEntry.Id };
            Storage.Components.Store(containerId, entryComponent);

            // Act.
            var retrievedEntry = Storage.Components.Retrieve<IdentifierComponent>(containerId);

            // Assert.
            Assert.NotNull(retrievedEntry);
            Assert.Equal(originalEntry.Id, retrievedEntry.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_ChildrenComponent()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalChildren = StorageTestHelper.CreateChildrenComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName = ComponentHelper.GetName(originalChildren);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, originalChildren);

            // Act.
            var retrievedData = Storage.Components.RetrieveAll<ChildrenComponent>(containerId);

            // Assert.
            Assert.Equal(1, retrievedData.Count());
            var retrievedChildren = retrievedData.First();
            var retrievedName = ComponentHelper.GetName(retrievedChildren);
            Assert.Equal(originalName, retrievedName);
            Assert.Equal(originalChildren.Relations.First(), retrievedChildren.Relations.First());
        }

        /*
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentStorage_Store_Retrieve_ChildrenComponent_Twice()
        {
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();

            var originalChildren1 = CreateChildrenComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName1 = ComponentBase.GetName(originalChildren1);

            var originalChildren2 = CreateChildrenComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName2 = ComponentBase.GetName(originalChildren1);

            var containerId = CreateSimpleContainerIdentifier();
            ComponentStorage.StoreAll(originalChildren1, containerId);
            ComponentStorage.StoreAll(originalChildren2, containerId);

            var retrievedData = ComponentStorage.RetrieveAll<ChildrenComponent>(containerId);
            Assert.Equal(2, retrievedData.Count());

            var retrievedChildren = retrievedData.First();
            var retrievedName = ComponentBase.GetName(retrievedChildren);
            Assert.Equal(originalName1, retrievedName);
            Assert.Equal(originalChildren1.Children.First(), retrievedChildren.Children.First());

            retrievedChildren = retrievedData.Skip(1).First();
            retrievedName = ComponentBase.GetName(retrievedChildren);
            Assert.Equal(originalName2, retrievedName);
            Assert.Equal(originalChildren2.Children.First(), retrievedChildren.Children.First());
        }
        */

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_ParentComponent()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalParent = StorageTestHelper.CreateParentComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName = ComponentHelper.GetName(originalParent);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, originalParent);

            // Act.
            var retrievedData = Storage.Components.Retrieve<ParentComponent>(containerId);

            // Assert.
            Assert.NotNull(retrievedData);
            var retrievedParent = retrievedData;
            var retrievedName = ComponentHelper.GetName(retrievedParent);
            Assert.Equal(originalName, retrievedName);
            Assert.Equal(originalParent.Relation, retrievedParent.Relation);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_NextComponent()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalNext = StorageTestHelper.CreateNextComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName = ComponentHelper.GetName(originalNext);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, originalNext);

            // Act.
            var retrievedData = Storage.Components.Retrieve<NextComponent>(containerId);

            // Assert.
            Assert.NotNull(retrievedData);
            var retrievedNext = retrievedData;
            var retrievedName = ComponentHelper.GetName(retrievedNext);
            Assert.Equal(originalName, retrievedName);
            Assert.Equal(originalNext.Relation, retrievedNext.Relation);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_PreviousComponent()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalPrevious = StorageTestHelper.CreatePreviousComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName = ComponentHelper.GetName(originalPrevious);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, originalPrevious);

            // Act.
            var retrievedData = Storage.Components.Retrieve<PreviousComponent>(containerId);

            // Assert.
            Assert.NotNull(retrievedData);
            var retrievedPrevious = retrievedData;
            var retrievedName = ComponentHelper.GetName(retrievedPrevious);
            Assert.Equal(originalName, retrievedName);
            Assert.Equal(originalPrevious.Relation, retrievedPrevious.Relation);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_UpdateComponent()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalUpdates = StorageTestHelper.CreateUpdatesComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName = ComponentHelper.GetName(originalUpdates);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, originalUpdates);

            // Act.
            var retrievedData = Storage.Components.RetrieveAll<UpdatesComponent>(containerId);

            // Assert.
            Assert.NotNull(retrievedData);
            var retrievedUpdates = retrievedData.First();
            var retrievedName = ComponentHelper.GetName(retrievedUpdates);
            Assert.Equal(originalName, retrievedName);
            Assert.Equal(originalUpdates.Relations.First(), retrievedUpdates.Relations.First());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_Retrieve_DowndateComponent()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var originalDowndate = StorageTestHelper.CreateDowndateComponent(storageId, accountId, spaceId, 0, 0, 0);
            var originalName = ComponentHelper.GetName(originalDowndate);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, originalDowndate);

            // Act.
            var retrievedData = Storage.Components.Retrieve<DowndateComponent>(containerId);

            // Assert.
            Assert.NotNull(retrievedData);
            var retrievedDowndate = retrievedData;
            var retrievedName = ComponentHelper.GetName(retrievedDowndate);
            Assert.Equal(originalName, retrievedName);
            Assert.Equal(originalDowndate.Relation, retrievedDowndate.Relation);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_PreviousComponent_Twice()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var firstPrevious = StorageTestHelper.CreatePreviousComponent(storageId, accountId, spaceId, 0, 0, 0);
            var secondPrevious = StorageTestHelper.CreatePreviousComponent(storageId, accountId, spaceId, 0, 0, 0);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, firstPrevious);
            
            // Act.
            var act = new Action(() =>
            {
                Storage.Components.Store(containerId, secondPrevious);
            });

            // Assert.
            ExceptionAssert.Throws<StorageException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_NextComponent_Twice()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var firstNext = StorageTestHelper.CreateNextComponent(storageId, accountId, spaceId, 0, 0, 0);
            var secondNext = StorageTestHelper.CreateNextComponent(storageId, accountId, spaceId, 0, 0, 0);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, firstNext);

            // Act.
            var act = new Action(() =>
            {
                Storage.Components.Store(containerId, secondNext);
            });

            // Assert.
            ExceptionAssert.Throws<StorageException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_DowndateComponent_Twice()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var firstDowndate = StorageTestHelper.CreateDowndateComponent(storageId, accountId, spaceId, 0, 0, 0);
            var secondDowndate = StorageTestHelper.CreateDowndateComponent(storageId, accountId, spaceId, 0, 0, 0);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, firstDowndate);

            // Act.
            var act = new Action(() =>
            {
                Storage.Components.Store(containerId, secondDowndate);
            });

            // Assert.
            ExceptionAssert.Throws<StorageException>(act);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void NtfsComponentStorage_Store_UpdateComponent_Twice()
        //{
        //    // TODO: This test should fail!
        //    // Arrange.
        //    var storageId = Guid.NewGuid();
        //    var accountId = Guid.NewGuid();
        //    var spaceId = Guid.NewGuid();
        //    var firstUpdates = StorageTestHelper.CreateUpdatesComponent(storageId, accountId, spaceId, 0, 0, 0);
        //    var secondUpdates = StorageTestHelper.CreateUpdatesComponent(storageId, accountId, spaceId, 0, 0, 0);
        //    var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
        //    ComponentStorage.Store(containerId, firstUpdates);

        //    // Act.
        //    var act = new Action(() =>
        //    {
        //        ComponentStorage.Store(containerId, secondUpdates);
        //    });

        //    // Assert.
        //    ExceptionAssert.Throws<StorageException>(act);
        //}

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsComponentStorage_Store_ParentComponent_Twice()
        {
            // Arrange.
            var storageId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var spaceId = Guid.NewGuid();
            var firstParent = StorageTestHelper.CreateParentComponent(storageId, accountId, spaceId, 0, 0, 0);
            var secondParent = StorageTestHelper.CreateParentComponent(storageId, accountId, spaceId, 0, 0, 0);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            Storage.Components.Store(containerId, firstParent);

            // Act.
            var act = new Action(() =>
            {
                Storage.Components.Store(containerId, secondParent);
            });

            // Assert.
            ExceptionAssert.Throws<StorageException>(act);
        }
    }
}
