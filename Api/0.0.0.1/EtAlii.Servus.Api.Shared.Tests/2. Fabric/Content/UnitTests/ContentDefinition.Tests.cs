namespace EtAlii.Servus.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Xunit;

    
    public class ContentDefinition_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Create()
        {
            // Arrange.

            // Act.
            var content = new ContentDefinition();

            // Assert.
            Assert.Equal(0, content.Parts.Count);
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Equals()
        {
            // Arrange.
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            var areEqual = first.Equals(second);

            // Assert.
            Assert.True(areEqual);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Create_ReadOnly()
        {
            // Arrange.

            // Act.
            var content = new ContentDefinition() as IReadOnlyContentDefinition;

            // Assert.
            Assert.Equal(0, content.Parts.Count());
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Add_Part()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            content.Parts.Add(new ContentDefinitionPart());

            // Assert.
            Assert.Equal(1, content.Parts.Count);
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Add_Part_ReadOnly()
        {
            // Arrange.
            var editableContent = new ContentDefinition();

            // Act.
            editableContent.Parts.Add(new ContentDefinitionPart());
            var content = editableContent as IReadOnlyContentDefinition;

            // Assert.
            Assert.Equal(1, content.Parts.Count());
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Equality_Operator_By_Checksum()
        {
            // Arrange.
            ulong checksum = (ulong)new Random().Next(0, int.MaxValue);
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            first.Checksum = checksum;
            second.Checksum = checksum;

            // Assert.
            Assert.True(first == second);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_InEquality_Operator_By_Checksum()
        {
            // Arrange.
            ulong checksum = (ulong)new Random().Next(0, int.MaxValue);
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            first.Checksum = checksum;
            second.Checksum = checksum;

            // Assert.
            Assert.False(first != second);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Equality_By_Checksum()
        {
            // Arrange.
            ulong checksum = (ulong)new Random().Next(0, int.MaxValue);
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            first.Checksum = checksum;
            second.Checksum = checksum;

            // Assert.
            Assert.True(first.Equals(second));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Equality_Operator_By_Size()
        {
            // Arrange.
            ulong size = (ulong)new Random().Next(0, int.MaxValue);
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            first.Size = size;
            second.Size = size;

            // Assert.
            Assert.True(first == second);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_InEquality_Operator_By_Size()
        {
            // Arrange.
            ulong size = (ulong)new Random().Next(0, int.MaxValue);
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            first.Size = size;
            second.Size = size;

            // Assert.
            Assert.False(first != second);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Equality_By_Size()
        {
            // Arrange.
            ulong size = (ulong)new Random().Next(0, int.MaxValue);
            var first = new ContentDefinition();
            var second = new ContentDefinition();

            // Act.
            first.Size = size;
            second.Size = size;

            // Assert.
            Assert.True(first.Equals(second));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Stored_Defaults_To_False()
        {
            // Arrange.

            // Act.
            var contentDefinition = new ContentDefinition();

            // Assert.
            Assert.False(contentDefinition.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Size_Defaults_To_0()
        {
            // Arrange.

            // Act.
            var contentDefinition = new ContentDefinition();

            // Assert.
            Assert.Equal((UInt64)0, contentDefinition.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Checksum_Defaults_To_0()
        {
            // Arrange.

            // Act.
            var contentDefinition = new ContentDefinition();

            // Assert.
            Assert.Equal((UInt64)0, contentDefinition.Checksum);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Null()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals(null);

            // Assert.
            Assert.False(equals);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Null_Object()
        {
            // Arrange.
            var content = new ContentDefinition();
            var o = (object)null;

            // Act.
            var equals = content.Equals(o);

            // Assert.
            Assert.False(equals);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Self()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals(content);

            // Assert.
            Assert.True(equals);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Self_Object()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals((object)content);

            // Assert.
            Assert.True(equals);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Other_Size()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition();
            var second = new ContentDefinition();
            first.Size = (ulong)random.Next(0, int.MaxValue);
            second.Size = (ulong)random.Next(0, int.MaxValue);

            // Act.
            var equals = first.Equals(second);

            // Assert.
            Assert.False(equals);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Other_Checksum()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition();
            var second = new ContentDefinition();
            first.Checksum = (ulong)random.Next(0, int.MaxValue);
            second.Checksum = (ulong)random.Next(0, int.MaxValue);

            // Act.
            var equals = first.Equals(second);

            // Assert.
            Assert.False(equals);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Other_Parts()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition();
            var second = new ContentDefinition();
            first.Parts.Add(new ContentDefinitionPart());
            first.Parts.Add(new ContentDefinitionPart());
            second.Parts.Add(new ContentDefinitionPart());

            // Act.
            var equals = first.Equals(second);

            // Assert.
            Assert.False(equals);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Compare_With_Other_Object()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition();
            var second = new ContentDefinition();
            first.Size = (ulong)random.Next(0, int.MaxValue);
            second.Size = (ulong)random.Next(0, int.MaxValue);

            // Act.
            var equals = first.Equals((object)second);

            // Assert.
            Assert.False(equals);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Get_Hash_For_Empty()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var hash = content.GetHashCode();

            // Assert.
            Assert.Equal(0, hash);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Get_Hash()
        {
            // Arrange.
            var random = new Random();
            var content = new ContentDefinition();
            content.Checksum = (ulong)random.Next(0, int.MaxValue);

            // Act.
            var hash = content.GetHashCode();

            // Assert.
            Assert.NotEqual(0, hash);
        }

    }
}
