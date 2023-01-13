namespace EtAlii.Ubigia.Tests;

using Xunit;

public class Base36ConvertUInt64Tests
{
    [Fact]
    public void Base36Convert_UInt64_ToString_0()
    {
        // Arrange.
        const ulong value = 0;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("0", result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToString_1()
    {
        // Arrange.
        const ulong value = 1;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("1", result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToString_10()
    {
        // Arrange.
        const ulong value = 10;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("a", result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToString_11()
    {
        // Arrange.
        const ulong value = 11;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("b", result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToString_111111()
    {
        // Arrange.
        const ulong value = 111111;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("2dqf", result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToString_11111111111111111111()
    {
        // Arrange.
        const ulong value = 11111111111111111111;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("2cf0g61r42wnb", result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToString_UInt64_MaxValue()
    {
        // Arrange.
        const ulong value = ulong.MaxValue - 1;

        // Act.
        var result = Base36Convert.ToString(value);

        // Assert.
        Assert.Equal("3w5e11264sgse", result);
    }



    [Fact]
    public void Base36Convert_UInt64_ToUint64_0()
    {
        // Arrange.
        const ulong value = 0;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_00()
    {
        // Arrange.
        const string value = "00";

        // Act.
        var result = Base36Convert.ToUInt64(value);

        // Assert.
        Assert.Equal((ulong)0, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_1()
    {
        // Arrange.
        const ulong value = 1;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_01()
    {
        // Arrange.
        const string value = "01";

        // Act.
        var result = Base36Convert.ToUInt64(value);

        // Assert.
        Assert.Equal((ulong)1, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_10()
    {
        // Arrange.
        const ulong value = 10;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_11()
    {
        // Arrange.
        const ulong value = 11;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_111111()
    {
        // Arrange.
        const ulong value = 111111;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUInt64_0002dqf()
    {
        // Arrange.
        const string value = "0002dqf";

        // Act.
        var result = Base36Convert.ToUInt64(value);

        // Assert.
        Assert.Equal((ulong)111111, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_11111111111111111111()
    {
        // Arrange.
        const ulong value = 11111111111111111111;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }

    [Fact]
    public void Base36Convert_UInt64_ToUint64_UInt64_MaxValue()
    {
        // Arrange.
        const ulong value = ulong.MaxValue - 1;
        var converted = Base36Convert.ToString(value);

        // Act.
        var result = Base36Convert.ToUInt64(converted);

        // Assert.
        Assert.Equal(value, result);
    }
}
