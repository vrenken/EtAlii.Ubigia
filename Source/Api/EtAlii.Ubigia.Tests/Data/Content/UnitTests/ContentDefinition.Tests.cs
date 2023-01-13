namespace EtAlii.Ubigia.Tests;

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
        var parts = new[] { new ContentDefinitionPart() };

        // Act.
        var content = ContentDefinition.Create(0, 0, parts);

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
        var first = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());

        // Assert.
        Assert.True(first == second);
    }

    [Fact]
    public void ContentDefinition_InEquality_Operator_By_Checksum()
    {
        // Arrange.
        var checksum = (ulong)new Random().Next(0, int.MaxValue);

        // Act.
        var first = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());

        // Assert.
        Assert.False(first != second);
    }

    [Fact]
    public void ContentDefinition_Equality_By_Checksum()
    {
        // Arrange.
        var checksum = (ulong)new Random().Next(0, int.MaxValue);

        // Act.
        var first = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());

        // Assert.
        Assert.True(first.Equals(second));
    }

    [Fact]
    public void ContentDefinition_Equality_Operator_By_Size()
    {
        // Arrange.
        var size = (ulong)new Random().Next(0, int.MaxValue);

        // Act.
        var first = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());

        // Assert.
        Assert.True(first == second);
    }

    [Fact]
    public void ContentDefinition_InEquality_Operator_By_Size()
    {
        // Arrange.
        var size = (ulong)new Random().Next(0, int.MaxValue);

        // Act.
        var first = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());

        // Assert.
        Assert.False(first != second);
    }

    [Fact]
    public void ContentDefinition_Equality_By_Size()
    {
        // Arrange.
        var size = (ulong)new Random().Next(0, int.MaxValue);

        // Act.
        var first = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());

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
        var first = ContentDefinition.Create(0, (ulong)random.Next(0, int.MaxValue), Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(0, (ulong)random.Next(0, int.MaxValue), Array.Empty<ContentDefinitionPart>());

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
        var first = ContentDefinition.Create((ulong)random.Next(0, int.MaxValue), 0, Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create((ulong)random.Next(0, int.MaxValue), 0, Array.Empty<ContentDefinitionPart>());

        // Act.
        var equals = first.Equals(second);

        // Assert.
        Assert.False(equals);
    }

    [Fact]
    public void ContentDefinition_Compare_With_Other_Parts()
    {
        // Arrange.
        var firstParts = new[]
        {
            ContentDefinitionPart.Create(0, 0, 0),
            ContentDefinitionPart.Create(0, 0, 0)
        };
        var first = ContentDefinition.Create(0, 0, firstParts);
        var secondParts = new[] { ContentDefinitionPart.Create(0, 0, 0) };
        var second = ContentDefinition.Create(0, 0, secondParts);

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
        var first = ContentDefinition.Create(0, (ulong)random.Next(0, int.MaxValue), Array.Empty<ContentDefinitionPart>());
        var second = ContentDefinition.Create(0, (ulong)random.Next(0, int.MaxValue), Array.Empty<ContentDefinitionPart>());

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
        var checksum = (ulong)random.Next(0, int.MaxValue);
        var content = ContentDefinition.Create(checksum, 0, Array.Empty<ContentDefinitionPart>());

        // Act.
        var hash = content.GetHashCode();

        // Assert.
        Assert.NotEqual(0, hash);
    }

}
