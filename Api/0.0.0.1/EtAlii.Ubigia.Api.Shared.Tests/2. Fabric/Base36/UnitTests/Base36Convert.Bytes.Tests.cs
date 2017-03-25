namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    public class Base36Convert_Bytes_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_0x00()
        {
            // Arrange.
            var value = new[]{ (byte)0 };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("0", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_0x01()
        {
            // Arrange.
            var value = new[] { (byte)1 };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("1", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_0x10()
        {
            // Arrange.
            var value = new[] { (byte)10 };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("a", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_0x11()
        {
            // Arrange.
            var value = new[] { (byte)11 };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("b", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_0xffff()
        {
            // Arrange.
            var value = new byte[] { 0xFF, 0xFF };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("1ekf", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_0x0fffff()
        {
            // Arrange.
            var value = new byte[] { 0x0F, 0xFF, 0xFF };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("mh33", result);
        }

        

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_abcd()
        {
            // Arrange.
            var value = new byte[] { 0xAB, 0xCD };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("xxp", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_2fj6n()
        {
            // Arrange.
            const string value = "2fj6n";

            // Act.
            var result = Base36Convert.ToBytes(value);

            // Assert.
            Assert.Equal(3, result.Length);
            Assert.Equal(0x3e, result[0]);
            Assert.Equal(0x50, result[1]);
            Assert.Equal(0xdf, result[2]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_2fj6n()
        {
            // Arrange.
            var value = new byte[] { 0x3e, 0x50, 0xdf };

            // Act.
            var result = Base36Convert.ToString(value);

            // Assert.
            Assert.Equal("2fj6n", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_2dqf()
        {
            // Arrange.
            const string value = "2dqf";

            // Act.
            var result = Base36Convert.ToBytes(value);

            // Assert.
            Assert.Equal(3, result.Length);
            Assert.Equal(0x01, result[0]);
            Assert.Equal(0xb2, result[1]);
            Assert.Equal(0x07, result[2]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_2dqf_ToString()
        {
            // Arrange.
            const string value = "2dqf";
            var bytes = Base36Convert.ToBytes(value);

            // Act.
            var result = Base36Convert.ToString(bytes);

            // Assert.
            Assert.Equal(value, result);
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_0()
        {
            // Arrange.
            const string value = "0";
            var converted = Base36Convert.ToBytes(value);

            // Act.
            var result = Base36Convert.ToString(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_1()
        {
            // Arrange.
            const string value = "1";
            var converted = Base36Convert.ToBytes(value);

            // Act.
            var result = Base36Convert.ToString(converted);

            // Assert.
            Assert.Equal(value, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_Timed_1000()
        {
            // Arrange.
            var duration = 0;
            var random = new Random();
            var iterations = 1000;
            var originalBytes = new List<byte[]>();
            for (int i = 0; i < iterations; i++)
            {
                var bytes = new byte[4];
                random.NextBytes(bytes);
                originalBytes.Add(bytes);
            }
            var convertedBytes = new List<string>();

            // Act.
            foreach (var bytes in originalBytes)
            {
                var start = Environment.TickCount;
                var s = Base36Convert.ToString(bytes);
                var stop = Environment.TickCount;
                duration += stop - start;
                convertedBytes.Add(s);
            }

            // Assert.
            Assert.True(TimeSpan.FromTicks(duration).TotalMilliseconds < 50,
                $"Calling Base36.ToString {iterations} took {TimeSpan.FromTicks(duration).TotalSeconds} seconds");

            //for (int i = 0; i < iterations; i++)
            //{
            //    var s = convertedBytes[i];
            //    var converted = Base36Convert.ToBytes(s);
            //    var original = originalBytes[i].SkipWhile(o => o == 0x00).ToArray();
            //    AreEqual(original, converted);
            //}
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToString_Timed_1000000()
        {
            // Arrange.
            var duration = 0;
            var random = new Random();
            var iterations = 1000000;
            var originalBytes = new List<byte[]>();
            for (int i = 0; i < iterations; i++)
            {
                var bytes = new byte[4];
                random.NextBytes(bytes);
                originalBytes.Add(bytes);
            }
            var convertedBytes = new List<string>();

            // Act.
            foreach (var bytes in originalBytes)
            {
                var start = Environment.TickCount;
                var s = Base36Convert.ToString(bytes);
                var stop = Environment.TickCount;
                duration += stop - start;
                convertedBytes.Add(s);
            }

            // Assert.
            Assert.True(TimeSpan.FromTicks(duration).TotalSeconds < 21f,
                $"Calling Base36.ToString {iterations} took {TimeSpan.FromTicks(duration).TotalSeconds} seconds");

            //for (int i = 0; i < iterations; i++)
            //{
            //    var s = convertedBytes[i];
            //    var converted = Base36Convert.ToBytes(s);
            //    var original = originalBytes[i].SkipWhile(o => o == 0x00).ToArray();
            //    AreEqual(original, converted);
            //}
        }

        private static readonly char[] alphabet = new[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 
                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
                'w', 'x', 'y', 'z'
            };

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_Timed_1000()
        {
            // Arrange.
            var duration = 0;
            var random = new Random();
            var iterations = 1000;
            var originalStrings = new List<string>();
            for (int i = 0; i < iterations; i++)
            {
                var characters = new char[4];
                for (int c = 0; c < characters.Length; c++)
                {
                    characters[c] = alphabet[random.Next(alphabet.Length)];
                }
                originalStrings.Add(new string(characters));
            }
            var convertedStrings = new List<byte[]>();

            // Act.
            foreach (var s in originalStrings)
            {
                var start = Environment.TickCount;
                var bytes = Base36Convert.ToBytes(s);
                var stop = Environment.TickCount;
                duration += stop - start;
                convertedStrings.Add(bytes);
            }

            // Assert.
            Assert.True(TimeSpan.FromTicks(duration).TotalSeconds < 2,
                $"Calling Base36.ToBytes {iterations} took {TimeSpan.FromTicks(duration).TotalSeconds} seconds");

            //for (int i = 0; i < iterations; i++)
            //{
            //    var bytes = convertedStrings[i];
            //    var converted = Base36Convert.ToString(bytes);
            //    var original = originalStrings[i];
            //    AreEqual(original, converted);
            //}
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Base36Convert_Bytes_ToBytes_Timed_1000000()
        {
            // Arrange.
            var duration = 0;
            var random = new Random();
            var iterations = 1000000;
            var originalStrings = new List<string>();
            for (int i = 0; i < iterations; i++)
            {
                var characters = new char[4];
                for (int c = 0; c < characters.Length; c++)
                {
                    characters[c] = alphabet[random.Next(alphabet.Length)];
                }
                originalStrings.Add(new string(characters));
            }
            var convertedStrings = new List<byte[]>();

            // Act.
            foreach (var s in originalStrings)
            {
                var start = Environment.TickCount;
                var bytes = Base36Convert.ToBytes(s);
                var stop = Environment.TickCount;
                duration += stop - start;
                convertedStrings.Add(bytes);
            }

            // Assert.
            Assert.True(TimeSpan.FromTicks(duration).TotalSeconds < 35,
                $"Calling Base36.ToBytes {iterations} took {TimeSpan.FromTicks(duration).TotalSeconds} seconds");

            //for (int i = 0; i < iterations; i++)
            //{
            //    var bytes = convertedStrings[i];
            //    var converted = Base36Convert.ToString(bytes);
            //    var original = originalStrings[i];
            //    AreEqual(original, converted);
            //}
        }


        private static void AreEqual(byte[] expected, byte[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
