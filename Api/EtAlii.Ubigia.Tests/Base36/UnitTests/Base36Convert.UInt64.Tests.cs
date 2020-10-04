namespace EtAlii.Ubigia.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    public class Base36ConvertUInt64Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_0()
        {
            // Arrange.
            const UInt64 value = 0;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("0", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_1()
        {
            // Arrange.
            const UInt64 value = 1;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("1", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_10()
        {
            // Arrange.
            const UInt64 value = 10;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("a", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_11()
        {
            // Arrange.
            const UInt64 value = 11;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("b", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_111111()
        {
            // Arrange.
            const UInt64 value = 111111;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("2dqf", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_11111111111111111111()
        {
            // Arrange.
            const UInt64 value = 11111111111111111111;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("2cf0g61r42wnb", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToString_UInt64_MaxValue()
        {
            // Arrange.
            const UInt64 value = UInt64.MaxValue - 1;

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("3w5e11264sgse", result);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_0()
        {
            // Arrange.
            const UInt64 value = 0;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_00()
        {
            // Arrange.
            const string value = "00";

            // Act.
            var result = Base36Convert.ToUInt64(value);

            // Assert.
            Assert.Equal((UInt64)0, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_1()
        {
            // Arrange.
            const UInt64 value = 1;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_01()
        {
            // Arrange.
            const string value = "01";

            // Act.
            var result = Base36Convert.ToUInt64(value);

            // Assert.
            Assert.Equal((UInt64)1, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_10()
        {
            // Arrange.
            const UInt64 value = 10;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_11()
        {
            // Arrange.
            const UInt64 value = 11;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_111111()
        {
            // Arrange.
            const UInt64 value = 111111;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUInt64_0002dqf()
        {
            // Arrange.
            const string value = "0002dqf";

            // Act.
            var result = Base36Convert.ToUInt64(value);

            // Assert.
            Assert.Equal((UInt64)111111, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_11111111111111111111()
        {
            // Arrange.
            const UInt64 value = 11111111111111111111;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_UInt64_ToUint64_UInt64_MaxValue()
        {
            // Arrange.
            const UInt64 value = UInt64.MaxValue - 1;
            var converted = Base36Convert.ToString(value);

            // Act.
            var result = Base36Convert.ToUInt64(converted);

            // Assert.
            Assert.Equal(value, result);
        }
    }
}
