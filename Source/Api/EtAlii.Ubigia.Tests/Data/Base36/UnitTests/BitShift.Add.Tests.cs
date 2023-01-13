namespace EtAlii.Ubigia.Tests;

using System;
using System.Linq;
using Xunit;

public class BitShiftAddTests
{
    [Fact]
    public void BitShift_Add_001_And_001_Is_010()
    {
        // Arrange.
        Span<bool> bits = "001".Select(c => c == '1').ToArray(); // 1
        var addition = "001".Select(c => c == '1').ToArray(); // 1

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("010", result); // 2
    }

    [Fact]
    public void BitShift_Add_0001_And_001_Is_0010()
    {
        // Arrange.
        Span<bool> bits = "0001".Select(c => c == '1').ToArray(); // 1
        var addition = "001".Select(c => c == '1').ToArray(); // 1

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("0010", result); // 2
    }

    [Fact]
    public void BitShift_Add_001_And_0001_Is_0010()
    {
        // Arrange.
        Span<bool> bits = "001".Select(c => c == '1').ToArray(); // 1
        var addition = "0001".Select(c => c == '1').ToArray(); // 1

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("0010", result); // 2
    }


    [Fact]
    public void BitShift_Add_011_And_001_Is_100()
    {
        // Arrange.
        Span<bool> bits = "011".Select(c => c == '1').ToArray(); // 3
        var shift = "001".Select(c => c == '1').ToArray(); // 1

        // Act.
        BitShift.Add(ref bits, shift);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("100", result); // 4
    }

    [Fact]
    public void BitShift_Add_1100010101_And_101101_Is_1101000010()
    {
        // Arrange.
        Span<bool> bits = "1100010101".Select(c => c == '1').ToArray(); // 789
        var addition = "101101".Select(c => c == '1').ToArray(); // 45

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        // a: 10000111010
        // e: 01101000010
        Assert.Equal("1101000010", result); // 834
    }

    [Fact]
    public void BitShift_Add_1100010101_And_111001000_Is_10011011101()
    {
        // Arrange.
        Span<bool> bits = "1100010101".Select(c => c == '1').ToArray(); // 789
        var addition = "111001000".Select(c => c == '1').ToArray(); // 456

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("10011011101", result); // 1245
    }

    [Fact]
    public void BitShift_Add_1001001001_And_101010101_Is_1110011110()
    {
        // Arrange.
        Span<bool> bits = "1001001001".Select(c => c == '1').ToArray(); // 585
        var addition = "101010101".Select(c => c == '1').ToArray(); // 341

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("1110011110", result); // 926
    }

    [Fact]
    public void BitShift_Add_111001000_And_1100010101_Is_10011011101()
    {
        // Arrange.
        Span<bool> bits = "111001000".Select(c => c == '1').ToArray(); // 456
        var addition = "1100010101".Select(c => c == '1').ToArray(); // 789

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("10011011101", result); // 1245
    }


    [Fact]
    public void BitShift_Add_1111111111111111_And_111111111111_Is_10000111111111110()
    {
        // Arrange.
        Span<bool> bits = "1111111111111111".Select(c => c == '1').ToArray(); // 65535
        var addition = "111111111111".Select(c => c == '1').ToArray(); // 4095

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("10000111111111110", result); // 69630
    }

    [Fact]
    public void BitShift_Add_111111111111_And_1111111111111111_Is_10000111111111110()
    {
        // Arrange.
        Span<bool> bits = "111111111111".Select(c => c == '1').ToArray(); // 4095
        var addition = "1111111111111111".Select(c => c == '1').ToArray(); // 65535

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("10000111111111110", result); // 69630
    }

    [Fact]
    public void BitShift_Add_1111111111111111_And_1111111111111111_Is_11111111111111110()
    {
        // Arrange.
        Span<bool> bits = "1111111111111111".Select(c => c == '1').ToArray(); // 65535
        var addition = "1111111111111111".Select(c => c == '1').ToArray(); // 65535

        // Act.
        BitShift.Add(ref bits, addition);
        var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

        // Assert.
        Assert.Equal("11111111111111110", result); // 131070
    }

}
