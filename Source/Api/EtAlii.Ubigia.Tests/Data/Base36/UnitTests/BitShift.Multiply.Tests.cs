namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.Linq;
    using Xunit;

    public class BitShiftAndTests
    {
        [Fact]
        public void BitShift_Multiply_10110_And_1101_Is_100011110()
        {
            // Arrange.
            Span<bool> bits = "10110".Select(c => c == '1').ToArray(); // 22
            var multiplication = "1101".Select(c => c == '1').ToArray(); // 13

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("100011110", result); // 286
        }

        [Fact]
        public void BitShift_Multiply_000_And_001_Is_000()
        {
            // Arrange.
            Span<bool> bits = "000".Select(c => c == '1').ToArray(); // 0
            var multiplication = "001".Select(c => c == '1').ToArray(); // 1

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("000", result); // 1
        }

        [Fact]
        public void BitShift_Multiply_001_And_001_Is_001()
        {
            // Arrange.
            Span<bool> bits = "001".Select(c => c == '1').ToArray(); // 1
            var multiplication = "001".Select(c => c == '1').ToArray(); // 1

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("001", result); // 1
        }

        [Fact]
        public void BitShift_Multiply_001_And_010_Is_010()
        {
            // Arrange.
            Span<bool> bits = "001".Select(c => c == '1').ToArray(); // 1
            var multiplication = "010".Select(c => c == '1').ToArray(); // 2

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("0010", result); // 2
        }

        [Fact]
        public void BitShift_Multiply_010_And_010_Is_0100()
        {
            // Arrange.
            Span<bool> bits = "010".Select(c => c == '1').ToArray(); // 2
            var multiplication = "010".Select(c => c == '1').ToArray(); // 2

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("0100", result); // 2
        }

        [Fact]
        public void BitShift_Multiply_0010_And_010_Is_00100()
        {
            // Arrange.
            Span<bool> bits = "0010".Select(c => c == '1').ToArray(); // 2
            var multiplication = "010".Select(c => c == '1').ToArray(); // 2

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("00100", result); // 2
        }


        [Fact]
        public void BitShift_Multiply_010_And_0010_Is_0100()
        {
            // Arrange.
            Span<bool> bits = "010".Select(c => c == '1').ToArray(); // 2
            var multiplication = "0010".Select(c => c == '1').ToArray(); // 2

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("0100", result); // 2
        }

        [Fact]
        public void BitShift_Multiply_111_And_001_Is_111()
        {
            // Arrange.
            Span<bool> bits = "111".Select(c => c == '1').ToArray(); // 7
            var multiplication = "001".Select(c => c == '1').ToArray(); // 1

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("111", result); // 7
        }

        [Fact]
        public void BitShift_Multiply_111_And_010_is_1110()
        {
            // Arrange.
            Span<bool> bits = "111".Select(c => c == '1').ToArray(); // 7
            var multiplication = "010".Select(c => c == '1').ToArray(); // 2

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("1110", result); // 14
        }

        [Fact]
        public void BitShift_Multiply_111_And_100_Is_11100()
        {
            // Arrange.
            Span<bool> bits = "111".Select(c => c == '1').ToArray();  // 7
            var multiplication = "100".Select(c => c == '1').ToArray();  // 4

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("11100", result); // 28
        }

        [Fact]
        public void BitShift_Multiply_1100010101_And_111001000_Is_1010111110101101000()
        {
            // Arrange.
            Span<bool> bits = "1100010101".Select(c => c == '1').ToArray();  // 789
            var multiplication = "111001000".Select(c => c == '1').ToArray();  // 456

            // Act.
            BitShift.Multiply(ref bits, multiplication);
            var result = string.Concat(bits.ToArray().Select(c => c ? "1" : "0"));

            // Assert.
            Assert.Equal("1010111110101101000", result);  // 359784
        }
    }
}
