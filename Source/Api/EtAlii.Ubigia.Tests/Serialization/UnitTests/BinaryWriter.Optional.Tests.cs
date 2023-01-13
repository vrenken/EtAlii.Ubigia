namespace EtAlii.Ubigia.Tests;

using System.IO;
using Xunit;

public partial class BinaryWriterTests
{
    [Fact]
    public void BinaryWriter_WriteOptional_String()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = "Test";

        // Act.
        writer.WriteOptional(o);
        stream.Position = 0;
        var result = reader.ReadOptionalReferenceType<string>();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteOptional_String_Null()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);

        // Act.
        writer.WriteOptional<string>(null);
        stream.Position = 0;
        var result = reader.ReadOptionalReferenceType<string>();

        // Assert.
        Assert.Null(result);
    }

    [Fact]
    public void BinaryWriter_WriteOptional_Int32()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = 123;

        // Act.
        writer.WriteOptional<int>(o);
        stream.Position = 0;
        var result = reader.ReadOptionalValueType<int>();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteOptional_Int32_Null()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);

        // Act.
        writer.WriteOptional<int>(null);
        stream.Position = 0;
        var result = reader.ReadOptionalValueType<int>();

        // Assert.
        Assert.Null(result);
    }
}
