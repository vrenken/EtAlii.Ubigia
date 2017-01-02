namespace EtAlii.Servus.Infrastructure.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;

    [TestClass]
    public class EntryRepository_Tests : TestBase
    {
        private const int _count = 10;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void EntryRepository_Prepare()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            
            // Act.
            var entry = Infrastructure.Entries.Prepare(space.Id);

            var containerId = ContainerIdentifierHelper.FromIdentifier(entry.Id);
            var folder = Infrastructure.StorageSystem.PathBuilder.GetFolder(containerId);
            Assert.IsTrue(Infrastructure.StorageSystem.FolderManager.Exists(folder));
        }

        [TestMethod]
        public void EntryRepository_Store_Only_Previous()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);

            // Act.
            entry = Infrastructure.Entries.Store(entry);

            var containerId = ContainerIdentifierHelper.FromIdentifier(entry.Id);
            var folder = Infrastructure.StorageSystem.PathBuilder.GetFolder(containerId);
            Assert.IsTrue(Infrastructure.StorageSystem.FolderManager.Exists(folder));
            var fileName = String.Format(Infrastructure.StorageSystem.ItemSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.IsTrue(Infrastructure.StorageSystem.FileManager.Exists(file));
        }

        [TestMethod]
        public void EntryRepository_Store_Previous_And_Next()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry1 = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);
            var entry2 = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);
            entry2 = Infrastructure.Entries.Store(entry2);

            // Act.
            entry2.Previous = Relation.NewRelation(entry1.Id);
            entry2 = Infrastructure.Entries.Store(entry2);

            // Arrange.
            var containerId = ContainerIdentifierHelper.FromIdentifier(entry1.Id);
            var folder = Infrastructure.StorageSystem.PathBuilder.GetFolder(containerId);
            Assert.IsTrue(Infrastructure.StorageSystem.FolderManager.Exists(folder));
            var fileName = String.Format(Infrastructure.StorageSystem.ItemSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.IsTrue(Infrastructure.StorageSystem.FileManager.Exists(file));
            fileName = String.Format(Infrastructure.StorageSystem.ItemSerializer.FileNameFormat, "Next");
            file = Path.Combine(folder, fileName);
            Assert.IsTrue(Infrastructure.StorageSystem.FileManager.Exists(file));
        }

        [TestMethod]
        public void EntryRepository_Store_Sequence_Same_Identifiers()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSequence(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = (IEditableEntry)Infrastructure.Entries.Get(createdEntries[i].Id);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.AreNotEqual(Identifier.Empty, createdEntry.Id);
                Assert.AreEqual(loadedEntry.Id, createdEntry.Id);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Sequence_Check_Next_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSequence(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count - 1; i++)
            {
                // Act.
                loadedEntries[i] = (IEditableEntry)Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.AreNotEqual(Relation.None, loadedEntry.Next);
                Assert.AreEqual(createdEntries[i + 1].Id, loadedEntry.Next.Id);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_First_Type_Hierarchy_Check_Child_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateFirstTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count - 1; i++)
            {
                // Act.
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                var loadedChildren = ((Entry)loadedEntry).Children;
                var createdChildren = ((Entry)createdEntry).Children;
                Assert.AreEqual(loadedChildren.First().Id, createdEntries[i + 1].Id);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Second_Type_Hierarchy_Check_Child_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSecondTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 0; i < _count - 1; i++)
            {
                // Act.
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                var loadedChildren2 = ((Entry)loadedEntry).Children2;
                var createdChildren2 = ((Entry)createdEntry).Children2;
                Assert.AreEqual(loadedChildren2.First().Id, createdEntries[i + 1].Id);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSequence(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 1; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.AreNotEqual(Identifier.Empty, loadedEntry.Previous);
                Assert.AreEqual(createdEntry.Previous, loadedEntry.Previous);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_First_Type_Hierarchy_Parent_Previous_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateFirstTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 1; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.AreNotEqual(Identifier.Empty, loadedEntry.Parent);
                Assert.AreEqual(createdEntry.Parent, loadedEntry.Parent);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Second_Type_Hierarchy_Parent_Previous_Identifiers_Based_On_Created()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSecondTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            for (int i = 1; i < _count; i++)
            {
                // Act.
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);

                // Assert.
                var loadedEntry = loadedEntries[i];
                var createdEntry = createdEntries[i];
                Assert.AreNotEqual(Identifier.Empty, loadedEntry.Parent2);
                Assert.AreEqual(createdEntry.Parent2, loadedEntry.Parent2);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_First_Type_Hierarchy_Child_With_Parent()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var parentEntry = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);
            parentEntry = Infrastructure.Entries.Store(parentEntry);
            var childEntry = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);

            // Act.
            childEntry.Parent = Relation.NewRelation(parentEntry.Id);
            childEntry = Infrastructure.Entries.Store(childEntry);

            // Assert.
            parentEntry = Infrastructure.Entries.Get(parentEntry.Id, EntryRelation.Parent | EntryRelation.Child);
            Assert.IsTrue(parentEntry.Children.Count == 1);
            var childId = parentEntry.Children.First().Relations.First().Id;
            Assert.AreEqual(childEntry.Id, childId);
        }

        [TestMethod]
        public void EntryRepository_Store_Second_Type_Hierarchy_Child_With_Parent()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var parent2Entry = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);
            parent2Entry = Infrastructure.Entries.Store(parent2Entry);
            var child2Entry = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);

            // Act.
            child2Entry.Parent2 = Relation.NewRelation(parent2Entry.Id);
            child2Entry = Infrastructure.Entries.Store(child2Entry);

            // Assert.
            parent2Entry = Infrastructure.Entries.Get(parent2Entry.Id, EntryRelation.Parent | EntryRelation.Child);
            Assert.IsTrue(parent2Entry.Children2.Count == 1);
            var child2Id = parent2Entry.Children2.First().Relations.First().Id;
            Assert.AreEqual(child2Entry.Id, child2Id);
        }

        [TestMethod]
        public void EntryRepository_Relate_Indexed_Entry_Using_Indexes()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var index = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);
            index = Infrastructure.Entries.Store(index);
            var entry = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);

            // Act.
            entry.Indexes.Add(index.Id);
            entry = Infrastructure.Entries.Store(entry);

            // Assert.
            index = Infrastructure.Entries.Get(index.Id, EntryRelation.Index | EntryRelation.Indexed);
            Assert.AreNotEqual(Relation.None, index.Indexed);
            Assert.AreEqual(index.Indexed.Id, entry.Id);
        }


        [TestMethod]
        public void EntryRepository_Relate_Indexed_Entry_Using_Indexed()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);
            entry = Infrastructure.Entries.Store(entry);
            var index = (IEditableEntry)Infrastructure.Entries.Prepare(space.Id);

            // Act.
            index.Indexed = Relation.NewRelation(entry.Id);
            index = Infrastructure.Entries.Store(index);

            // Assert.
            entry = Infrastructure.Entries.Get(entry.Id, EntryRelation.Index | EntryRelation.Indexed);
            Assert.IsTrue(entry.Indexes.Contains(index.Id));
        }

        [TestMethod]
        public void EntryRepository_Store_Sequence_Check_Previous_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSequence(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                Assert.AreNotEqual(Identifier.Empty, loadedEntry.Id);
                Assert.AreEqual(loadedEntries[i + 1].Previous.Id, loadedEntry.Id);
            }
        }


        [TestMethod]
        public void EntryRepository_Store_Hierarchy_Check_Parent_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateFirstTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                Assert.AreNotEqual(Identifier.Empty, loadedEntry.Id);
                Assert.AreEqual(loadedEntries[i + 1].Parent.Id, loadedEntry.Id);
            }
        }


        [TestMethod]
        public void EntryRepository_Store_Sequence_Check_Next_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSequence(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                var next = loadedEntry.Next.Id;
                Assert.AreNotEqual(Identifier.Empty, next);
                Assert.AreEqual(loadedEntries[i + 1].Id, next);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Hierarchy_Check_Child_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateFirstTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var loadedEntry = loadedEntries[i];
                var child = ((Entry)loadedEntry).Children.First().Id;
                Assert.AreNotEqual(Identifier.Empty, child);
                Assert.AreEqual(loadedEntries[i + 1].Id, child);
            }
        }


        [TestMethod]
        public void EntryRepository_Store_Sequence_Check_Next_Previous_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateSequence(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Previous | EntryRelation.Next);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var previousEntry = loadedEntries[i];
                var nextId = previousEntry.Next.Id;
                var nextEntry = loadedEntries[i + 1];
                var previousId = nextEntry.Previous.Id;
                Assert.AreNotEqual(Identifier.Empty, previousEntry.Id);
                Assert.AreEqual(previousId, previousEntry.Id);
                Assert.AreNotEqual(Identifier.Empty, nextEntry.Id);
                Assert.AreEqual(nextId, nextEntry.Id);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Hierarchy_Check_Child_Parent_Identifiers_Based_On_Loaded()
        {
            // Arrange.
            var createdEntries = ApiTestHelper.CreateFirstTypeHierarchy(_count, Infrastructure);
            var loadedEntries = new IEditableEntry[_count];

            // Act.
            for (int i = 0; i < _count; i++)
            {
                loadedEntries[i] = Infrastructure.Entries.Get(createdEntries[i].Id, EntryRelation.Parent | EntryRelation.Child);
            }

            // Assert.
            for (int i = 0; i < _count - 1; i++)
            {
                var parentEntry = loadedEntries[i];
                var childId = ((Entry)parentEntry).Children.First().Id;
                var childEntry = loadedEntries[i + 1];
                var parentId = childEntry.Parent.Id;
                Assert.AreNotEqual(Identifier.Empty, parentEntry.Id);
                Assert.AreEqual(parentId, parentEntry.Id);
                Assert.AreNotEqual(Identifier.Empty, childEntry.Id);
                Assert.AreEqual(childId, childEntry.Id);
            }
        }

        [TestMethod]
        public void EntryRepository_Store_Already_Existing_Entry()
        {
            // Arrange.
            var space = ApiTestHelper.CreateSpace(Infrastructure);
            var entry = Infrastructure.Entries.Prepare(space.Id);
            var containerId = ContainerIdentifierHelper.FromIdentifier(entry.Id);
            var folder = Infrastructure.StorageSystem.PathBuilder.GetFolder(containerId);
            Assert.IsTrue(Infrastructure.StorageSystem.FolderManager.Exists(folder));

            // Act.
            entry = Infrastructure.Entries.Store(entry);

            // Assert.
            var fileName = String.Format(Infrastructure.StorageSystem.ItemSerializer.FileNameFormat, "Identifier");
            var file = Path.Combine(folder, fileName);
            Assert.IsTrue(Infrastructure.StorageSystem.FileManager.Exists(file));

            // Act.
            entry = Infrastructure.Entries.Store(entry);
        }
    }
}
