namespace EtAlii.Ubigia.Tests;

using System;
using System.IO;
using Xunit;

public partial class BinaryWriterTests
{
    [Fact]
    public void BinaryWriter_WriteTyped_String()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = "test";

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Int16()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (short)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Int32()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = 123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Int64()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (long)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }


    [Fact]
    public void BinaryWriter_WriteTyped_UInt16()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (ushort)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_UInt32()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (uint)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_UInt64()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (ulong)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Char()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (char)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Byte()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (byte)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_SByte()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (sbyte)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Float()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (float)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Double()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (double)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Guid()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = Guid.NewGuid();

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Bool()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);

        // Act.
        writer.WriteTyped(true);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(true, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Decimal()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = (decimal)123;

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Range()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = new Range(1,23);

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_DateTime()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = new DateTime(1,2,3,4,5,6);

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_TimeSpan()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = new TimeSpan(1,2,3);

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }

    [Fact]
    public void BinaryWriter_WriteTyped_Version()
    {
        // Arrange.
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);
        var o = new Version(1,2,3,4 );

        // Act.
        writer.WriteTyped(o);
        stream.Position = 0;
        var result = reader.ReadTyped();

        // Assert.
        Assert.Equal(o, result);
    }
}
