namespace EtAlii.Ubigia.Tests.UnitTests
{
    using System;
    using Xunit;

    public class ComponentEditableEntryTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Id()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new IdentifierComponent() { Id = otherId };

            // Act.
            ((IComponentEditableEntry)entry).IdComponent = component;

            // Assert.
            Assert.Equal(otherId, entry.Id);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Parent()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new ParentComponent() { Relation = relation, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).ParentComponent = component;

            // Assert.
            Assert.Equal(relation, entry.Parent);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Parent2()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new Parent2Component() { Relation = relation, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).Parent2Component = component;

            // Assert.
            Assert.Equal(relation, entry.Parent2);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Previous()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new PreviousComponent() { Relation = relation, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).PreviousComponent = component;

            // Assert.
            Assert.Equal(relation, entry.Previous);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Next()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new NextComponent() { Relation = relation, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).NextComponent = component;

            // Assert.
            Assert.Equal(relation, entry.Next);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Downdate()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new DowndateComponent() { Relation = relation, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).DowndateComponent = component;

            // Assert.
            Assert.Equal(relation, entry.Downdate);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Indexed()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var otherId = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var relation = Relation.Create(otherId, 2);
            var component = new IndexedComponent() { Relation = relation, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).IndexedComponent = component;

            // Assert.
            Assert.Equal(relation, entry.Indexed);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Type()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var type = Guid.NewGuid().ToString();
            var component = new TypeComponent() { Type = type, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).TypeComponent = component;

            // Assert.
            Assert.Equal(type, entry.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ComponentEditableEntry_Set_And_Get_Tag()
        {
            // Arrange.
            var id = new TestIdentifierFactory().Create();
            var entry = Entry.NewEntry(id);
            var tag = Guid.NewGuid().ToString();
            var component = new TagComponent() { Tag = tag, Stored = true };

            // Act.
            ((IComponentEditableEntry)entry).TagComponent = component;

            // Assert.
            Assert.Equal(tag, entry.Tag);
        }

    }
}
