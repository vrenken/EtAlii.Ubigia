// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class HierarchicalRelationDuplicatorUnitTests
    {
        private readonly TestIdentifierFactory _testIdentifierFactory;

        public HierarchicalRelationDuplicatorUnitTests()
        {
            _testIdentifierFactory = new TestIdentifierFactory();
        }

        [Fact]
        public void HierarchicalRelationDuplicator_Create()
        {
            // Arrange.

            // Act.
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Assert.
            Assert.NotNull(hierarchicalRelationDuplicator);
        }

        [Fact]
        public void HierarchicalRelationDuplicator_Duplicate_Children_10()
        {
            var count = 10;

            // Arrange.
            var entryId = _testIdentifierFactory.Create();
            var previousEntryId = _testIdentifierFactory.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var first = (IEditableEntry)Entry.NewEntry(entryId, previousEntryRelation);
            for (var i = 0; i < count; i++)
            {
                first.Children.Add(_testIdentifierFactory.Create());
            }
            var second = Entry.NewEntry(entryId, previousEntryRelation);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second);

            // Assert.
            Assert.Equal(count, second.Children.Count());
            Assert.Empty(second.Children2);

            for (var i = 0; i < count; i++)
            {
                var secondRelation = second.Children.Skip(i).First();
                Assert.True(first.Children.Contains(secondRelation.Id));
            }
        }

        [Fact]
        public void HierarchicalRelationDuplicator_Duplicate_Children2_10()
        {
            var count = 10;

            // Arrange.
            var entryId = _testIdentifierFactory.Create();
            var previousEntryId = _testIdentifierFactory.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var first = (IEditableEntry)Entry.NewEntry(entryId, previousEntryRelation);
            for (var i = 0; i < count; i++)
            {
                first.Children2.Add(_testIdentifierFactory.Create());
            }
            var second = Entry.NewEntry(entryId, previousEntryRelation);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second);

            // Assert.
            Assert.Empty(second.Children);
            Assert.Equal(count, second.Children2.Count());

            for (var i = 0; i < count; i++)
            {
                var secondRelation = second.Children2.Skip(i).First();
                Assert.True(first.Children2.Contains(secondRelation.Id));
            }
        }

        [Fact]
        public void HierarchicalRelationDuplicator_Duplicate_Children_10_Exclude()
        {
            var count = 10;

            // Arrange.
            var entryId = _testIdentifierFactory.Create();
            var previousEntryId = _testIdentifierFactory.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var first = (IEditableEntry)Entry.NewEntry(entryId, previousEntryRelation);
            var idToExclude = Identifier.Empty;
            for (var i = 0; i < count; i++)
            {
                var identifier = _testIdentifierFactory.Create();
                if (i == count / 2)
                {
                    idToExclude = identifier;
                }
                first.Children.Add(identifier);
            }
            var second = Entry.NewEntry(entryId, previousEntryRelation);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second, idToExclude);

            // Assert.
            Assert.Equal(count - 1, second.Children.Count());
            Assert.Empty(second.Children2);
            Assert.False(((IEditableEntry)second).Children.Contains(idToExclude));
            for (var i = 0; i < count - 1; i++)
            {
                var secondRelation = second.Children.Skip(i).First();
                Assert.True(first.Children.Contains(secondRelation.Id));
            }
        }

        [Fact]
        public void HierarchicalRelationDuplicator_Duplicate_Children2_10_Exclude()
        {
            var count = 10;

            // Arrange.
            var entryId = _testIdentifierFactory.Create();
            var previousEntryId = _testIdentifierFactory.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var first = (IEditableEntry)Entry.NewEntry(entryId, previousEntryRelation);
            var idToExclude = Identifier.Empty;
            for (var i = 0; i < count; i++)
            {
                var identifier = _testIdentifierFactory.Create();
                if (i == count / 2)
                {
                    idToExclude = identifier;
                }
                first.Children2.Add(identifier);
            }
            var second = Entry.NewEntry(entryId, previousEntryRelation);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();
            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second, idToExclude);

            // Assert.
            Assert.Empty(second.Children);
            Assert.Equal(count - 1, second.Children2.Count());
            Assert.False(((IEditableEntry)second).Children2.Contains(idToExclude));
            for (var i = 0; i < count - 1; i++)
            {
                var secondRelation = second.Children2.Skip(i).First();
                Assert.True(first.Children2.Contains(secondRelation.Id));
            }
        }


        [Fact]
        public void HierarchicalRelationDuplicator_Duplicate_And_Children2_10()
        {
            var count = 10;

            // Arrange.
            var entryId = _testIdentifierFactory.Create();
            var previousEntryId = _testIdentifierFactory.Create();
            var previousEntryRelation = Relation.NewRelation(previousEntryId);
            var first = (IEditableEntry)Entry.NewEntry(entryId, previousEntryRelation);
            for (var i = 0; i < count; i++)
            {
                first.Children.Add(_testIdentifierFactory.Create());
            }
            for (var i = 0; i < count; i++)
            {
                first.Children2.Add(_testIdentifierFactory.Create());
            }
            var second = Entry.NewEntry(entryId, previousEntryRelation);
            var hierarchicalRelationDuplicator = new HierarchicalRelationDuplicator();

            // Act.
            hierarchicalRelationDuplicator.Duplicate((IReadOnlyEntry)first, second);

            // Assert.
            Assert.Equal(count, second.Children.Count());
            Assert.Equal(count, second.Children2.Count());

            for (var i = 0; i < count; i++)
            {
                var secondRelation = second.Children.Skip(i).First();
                Assert.True(first.Children.Contains(secondRelation.Id));
            }
            for (var i = 0; i < count; i++)
            {
                var secondRelation = second.Children2.Skip(i).First();
                Assert.True(first.Children2.Contains(secondRelation.Id));
            }
        }
    }
}
