namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.Linq;
    using Xunit;

    public class EditableEntryTests
    {
        [Fact]
        public void IEditableEntry_Set_And_Get_Previous()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);

            // Act.
            ((IEditableEntry)entry).Previous = relation;

            // Assert.
            Assert.Equal(relation, entry.Previous);
            Assert.Equal(relation, ((IEditableEntry)entry).Previous);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Next()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);

            // Act.
            ((IEditableEntry)entry).Next = relation;

            // Assert.
            Assert.Equal(relation, entry.Next);
            Assert.Equal(relation, ((IEditableEntry)entry).Next);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Downdate()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);

            // Act.
            ((IEditableEntry)entry).Downdate = relation;

            // Assert.
            Assert.Equal(relation, entry.Downdate);
            Assert.Equal(relation, ((IEditableEntry)entry).Downdate);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Parent()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);

            // Act.
            ((IEditableEntry)entry).Parent = relation;

            // Assert.
            Assert.Equal(relation, entry.Parent);
            Assert.Equal(relation, ((IEditableEntry)entry).Parent);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Parent2()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);

            // Act.
            ((IEditableEntry)entry).Parent2 = relation;

            // Assert.
            Assert.Equal(relation, entry.Parent2);
            Assert.Equal(relation, ((IEditableEntry)entry).Parent2);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Indexed()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);

            // Act.
            ((IEditableEntry)entry).Indexed = relation;

            // Assert.
            Assert.Equal(relation, entry.Indexed);
            Assert.Equal(relation, ((IEditableEntry)entry).Indexed);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Children()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);

            // Act.
            ((IEditableEntry)entry).Children.Add(otherId);

            // Assert.
            Assert.Single(entry.Children);
            Assert.Equal(otherId, entry.Children.Single().Id);
            Assert.Equal(otherId, ((IEditableEntry)entry).Children.Single().Relations.Single().Id);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Children2()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);

            // Act.
            ((IEditableEntry)entry).Children2.Add(otherId);

            // Assert.
            Assert.Single(entry.Children2);
            Assert.Equal(otherId, entry.Children2.Single().Id);
            Assert.Equal(otherId, ((IEditableEntry)entry).Children2.Single().Relations.Single().Id);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Updates()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);

            // Act.
            ((IEditableEntry)entry).AddUpdate(otherId);

            // Assert.
            Assert.Single(entry.Updates);
            Assert.Equal(otherId, entry.Updates.Single().Id);
            Assert.Equal(otherId, ((IEditableEntry)entry).Updates.Single().Relations.Single().Id);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Indexes()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);

            // Act.
            ((IEditableEntry)entry).Indexes.Add(otherId);

            // Assert.
            Assert.Single(entry.Indexes);
            Assert.Equal(otherId, entry.Indexes.Single().Id);
            Assert.Equal(otherId, ((IEditableEntry)entry).Indexes.Single().Relations.Single().Id);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Type()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var type = Guid.NewGuid().ToString();

            // Act.
            ((IEditableEntry)entry).Type = type;

            // Assert.
            Assert.Equal(type, entry.Type);
        }

        [Fact]
        public void IEditableEntry_Set_And_Get_Tag()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var tag = Guid.NewGuid().ToString();

            // Act.
            ((IEditableEntry)entry).Tag = tag;

            // Assert.
            Assert.Equal(tag, entry.Tag);
        }
    }
}
