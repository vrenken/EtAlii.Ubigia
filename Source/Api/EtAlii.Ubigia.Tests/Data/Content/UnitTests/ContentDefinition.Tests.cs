namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public class ContentDefinitionTests
    {
        [Fact]
        public void ContentDefinition_Create()
        {
            // Arrange.

            // Act.
            var content = new ContentDefinition();

            // Assert.
            Assert.Empty(content.Parts);
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact]
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

        [Fact]
        public void ContentDefinition_Create_ReadOnly()
        {
            // Arrange.

            // Act.
            var content = new ContentDefinition();

            // Assert.
            Assert.Empty(content.Parts);
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact]
        public void ContentDefinition_Add_Part()
        {
            // Arrange.

            // Act.
            var content = new ContentDefinition
            {
                Parts = new []
                {
                    new ContentDefinitionPart()
                }
            };

            // Assert.
            Assert.Single(content.Parts);
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact]
        public void ContentDefinition_Add_Part_ReadOnly()
        {
            // Arrange.

            // Act.
            var content = new ContentDefinition
            {
                Parts = new []
                {
                    new ContentDefinitionPart()
                }
            };

            // Assert.
            Assert.Single(content.Parts);
            Assert.Equal((ulong)0, content.Size);
        }

        [Fact]
        public void ContentDefinition_Equality_Operator_By_Checksum()
        {
            // Arrange.
            var checksum = (ulong)new Random().Next(0, int.MaxValue);

            // Act.
            var first = new ContentDefinition { Checksum = checksum };
            var second = new ContentDefinition { Checksum = checksum };

            // Assert.
            Assert.True(first == second);
        }

        [Fact]
        public void ContentDefinition_InEquality_Operator_By_Checksum()
        {
            // Arrange.
            var checksum = (ulong)new Random().Next(0, int.MaxValue);

            // Act.
            var first = new ContentDefinition { Checksum = checksum };
            var second = new ContentDefinition { Checksum = checksum };

            // Assert.
            Assert.False(first != second);
        }

        [Fact]
        public void ContentDefinition_Equality_By_Checksum()
        {
            // Arrange.
            var checksum = (ulong)new Random().Next(0, int.MaxValue);

            // Act.
            var first = new ContentDefinition { Checksum = checksum };
            var second = new ContentDefinition { Checksum = checksum };

            // Assert.
            Assert.True(first.Equals(second));
        }

        [Fact]
        public void ContentDefinition_Equality_Operator_By_Size()
        {
            // Arrange.
            var size = (ulong)new Random().Next(0, int.MaxValue);

            // Act.
            var first = new ContentDefinition { Size = size };
            var second = new ContentDefinition { Size = size };

            // Assert.
            Assert.True(first == second);
        }

        [Fact]
        public void ContentDefinition_InEquality_Operator_By_Size()
        {
            // Arrange.
            var size = (ulong)new Random().Next(0, int.MaxValue);

            // Act.
            var first = new ContentDefinition { Size = size };
            var second = new ContentDefinition { Size = size };

            // Assert.
            Assert.False(first != second);
        }

        [Fact]
        public void ContentDefinition_Equality_By_Size()
        {
            // Arrange.
            var size = (ulong)new Random().Next(0, int.MaxValue);

            // Act.
            var first = new ContentDefinition { Size = size };
            var second = new ContentDefinition { Size = size };

            // Assert.
            Assert.True(first.Equals(second));
        }

        [Fact]
        public void ContentDefinition_Stored_Defaults_To_False()
        {
            // Arrange.

            // Act.
            var contentDefinition = new ContentDefinition();

            // Assert.
            Assert.False(contentDefinition.Stored);
        }

        [Fact]
        public void ContentDefinition_Size_Defaults_To_0()
        {
            // Arrange.

            // Act.
            var contentDefinition = new ContentDefinition();

            // Assert.
            Assert.Equal((ulong)0, contentDefinition.Size);
        }

        [Fact]
        public void ContentDefinition_Checksum_Defaults_To_0()
        {
            // Arrange.

            // Act.
            var contentDefinition = new ContentDefinition();

            // Assert.
            Assert.Equal((ulong)0, contentDefinition.Checksum);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Null()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals(null);

            // Assert.
            Assert.False(equals);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Null_Object()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals(null);

            // Assert.
            Assert.False(equals);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Self()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals(content);

            // Assert.
            Assert.True(equals);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Self_Object()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var equals = content.Equals((object)content);

            // Assert.
            Assert.True(equals);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Other_Size()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition { Size = (ulong)random.Next(0, int.MaxValue) };
            var second = new ContentDefinition { Size = (ulong)random.Next(0, int.MaxValue) };

            // Act.
            var equals = first.Equals(second);

            // Assert.
            Assert.False(equals);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Other_Checksum()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition { Checksum = (ulong)random.Next(0, int.MaxValue) };
            var second = new ContentDefinition { Checksum = (ulong)random.Next(0, int.MaxValue) };

            // Act.
            var equals = first.Equals(second);

            // Assert.
            Assert.False(equals);
        }

        [Fact]
        public void ContentDefinition_Compare_With_Other_Parts()
        {
            // Arrange.
            var first = new ContentDefinition
            {
                Parts = new []
                {
                    new ContentDefinitionPart(),
                    new ContentDefinitionPart()
                }
            };
            var second = new ContentDefinition()
            {
                Parts = new []
                {
                    new ContentDefinitionPart(),
                }
            };

            // Act.
            var equals = first.Equals(second);

            // Assert.
            Assert.False(equals);
        }


        [Fact]
        public void ContentDefinition_Compare_With_Other_Object()
        {
            // Arrange.
            var random = new Random();
            var first = new ContentDefinition { Size = (ulong)random.Next(0, int.MaxValue)};
            var second = new ContentDefinition { Size = (ulong)random.Next(0, int.MaxValue)};

            // Act.
            var equals = first.Equals((object)second);

            // Assert.
            Assert.False(equals);
        }


        [Fact(Skip = "The new HashCode uses random seeds to calculate in-process hashes")]
        public void ContentDefinition_Get_Hash_For_Empty()
        {
            // Arrange.
            var content = new ContentDefinition();

            // Act.
            var hash = content.GetHashCode();

            // Assert.
            Assert.Equal(0, hash);
        }

        [Fact]
        public void ContentDefinition_Get_Hash()
        {
            // Arrange.
            var random = new Random();
            var content = new ContentDefinition {Checksum = (ulong) random.Next(0, int.MaxValue)};

            // Act.
            var hash = content.GetHashCode();

            // Assert.
            Assert.NotEqual(0, hash);
        }

    }
}
