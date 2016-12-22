namespace EtAlii.Servus.Api.Logical.Tests
{
    using System.Linq;
    using System.Runtime.InteropServices;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    
    public class HierarchicalRelationDuplicator_UnitTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void HierarchicalRelationDuplicator_Create()
        {
            // Arrange.

            // Act.
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void HierarchicalRelationDuplicator_Duplicate_Children_10()
        {
            var count = 10;

            // Arrange.
            var entryId = TestIdentifier.Create();
            var previousEntryId = TestIdentifier.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var testContent = "TestContent";
            var first = (IEditableEntry)new Entry(entryId, previousEntryRelation, testContent);
            for (int i = 0; i < count; i++)
            {
                first.Children.Add(TestIdentifier.Create());
            }
            var second = new Entry(entryId, previousEntryRelation, testContent);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second);

            // Assert.
            Assert.Equal(count, second.Children.Count());
            Assert.Equal(0, second.Children2.Count());

            for (int i = 0; i < count; i++)
            {
                var secondRelation = second.Children.Skip(i).First();
                Assert.True(first.Children.Contains(secondRelation.Id));
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void HierarchicalRelationDuplicator_Duplicate_Children2_10()
        {
            var count = 10;

            // Arrange.
            var entryId = TestIdentifier.Create();
            var previousEntryId = TestIdentifier.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var testContent = "TestContent";
            var first = (IEditableEntry)new Entry(entryId, previousEntryRelation, testContent);
            for (int i = 0; i < count; i++)
            {
                first.Children2.Add(TestIdentifier.Create());
            }
            var second = new Entry(entryId, previousEntryRelation, testContent);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second);

            // Assert.
            Assert.Equal(0, second.Children.Count());
            Assert.Equal(count, second.Children2.Count());

            for (int i = 0; i < count; i++)
            {
                var secondRelation = second.Children2.Skip(i).First();
                Assert.True(first.Children2.Contains(secondRelation.Id));
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void HierarchicalRelationDuplicator_Duplicate_Children_10_Exclude()
        {
            var count = 10;

            // Arrange.
            var entryId = TestIdentifier.Create();
            var previousEntryId = TestIdentifier.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var testContent = "TestContent";
            var first = (IEditableEntry)new Entry(entryId, previousEntryRelation, testContent);
            var idToExclude = Identifier.Empty;
            for (int i = 0; i < count; i++)
            {
                var identifier = TestIdentifier.Create();
                if (i == count / 2)
                {
                    idToExclude = identifier;
                }
                first.Children.Add(identifier);
            }
            var second = new Entry(entryId, previousEntryRelation, testContent);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();
            
            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second, idToExclude);

            // Assert.
            Assert.Equal(count - 1, second.Children.Count());
            Assert.Equal(0, second.Children2.Count());
            Assert.False(((IEditableEntry)second).Children.Contains(idToExclude));
            for (int i = 0; i < count - 1; i++)
            {
                var secondRelation = second.Children.Skip(i).First();
                Assert.True(first.Children.Contains(secondRelation.Id));
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void HierarchicalRelationDuplicator_Duplicate_Children2_10_Exclude()
        {
            var count = 10;

            // Arrange.
            var entryId = TestIdentifier.Create();
            var previousEntryId = TestIdentifier.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var testContent = "TestContent";
            var first = (IEditableEntry)new Entry(entryId, previousEntryRelation, testContent);
            var idToExclude = Identifier.Empty;
            for (int i = 0; i < count; i++)
            {
                var identifier = TestIdentifier.Create();
                if (i == count / 2)
                {
                    idToExclude = identifier;
                }
                first.Children2.Add(identifier);
            }
            var second = new Entry(entryId, previousEntryRelation, testContent);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();
            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second, idToExclude);

            // Assert.
            Assert.Equal(0, second.Children.Count());
            Assert.Equal(count - 1, second.Children2.Count());
            Assert.False(((IEditableEntry)second).Children2.Contains(idToExclude));
            for (int i = 0; i < count - 1; i++)
            {
                var secondRelation = second.Children2.Skip(i).First();
                Assert.True(first.Children2.Contains(secondRelation.Id));
            }
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void HierarchicalRelationDuplicator_Duplicate_And_Children2_10()
        {
            var count = 10;

            // Arrange.
            var entryId = TestIdentifier.Create();
            var previousEntryId = TestIdentifier.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var testContent = "TestContent";
            var first = (IEditableEntry)new Entry(entryId, previousEntryRelation, testContent);
            for (int i = 0; i < count; i++)
            {
                first.Children.Add(TestIdentifier.Create());
            }
            for (int i = 0; i < count; i++)
            {
                first.Children2.Add(TestIdentifier.Create());
            }
            var second = new Entry(entryId, previousEntryRelation, testContent);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second);

            // Assert.
            Assert.Equal(count, second.Children.Count());
            Assert.Equal(count, second.Children2.Count());

            for (int i = 0; i < count; i++)
            {
                var secondRelation = second.Children.Skip(i).First();
                Assert.True(first.Children.Contains(secondRelation.Id));
            }
            for (int i = 0; i < count; i++)
            {
                var secondRelation = second.Children2.Skip(i).First();
                Assert.True(first.Children2.Contains(secondRelation.Id));
            }
        }
    }
}
