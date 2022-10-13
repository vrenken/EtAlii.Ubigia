namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.IO;
    using Xunit;

    public partial class BinaryWriterTests
    {
        [Fact]
        public void BinaryWriter_Write_String()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = "test";

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<string>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Int16()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (short)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<short>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Int32()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = 123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<int>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Int64()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (long)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<long>();

            // Assert.
            Assert.Equal(o, result);
        }


        [Fact]
        public void BinaryWriter_Write_UInt16()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (ushort)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<ushort>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_UInt32()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (uint)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<uint>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_UInt64()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (ulong)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<ulong>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Char()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (char)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<char>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Byte()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (byte)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<byte>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_SByte()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (sbyte)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<sbyte>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Float()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (float)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<float>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Double()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (double)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<double>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Guid()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = Guid.NewGuid();

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<Guid>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Bool()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);

            // Act.
            writer.Write(true);
            stream.Position = 0;
            var result = reader.Read<bool>();

            // Assert.
            Assert.True(result);
        }

        [Fact]
        public void BinaryWriter_Write_Decimal()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = (decimal)123;

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<decimal>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Range()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = new Range(1,23);

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<Range>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_DateTime()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = new DateTime(1,2,3,4,5,6);

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<DateTime>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_TimeSpan()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = new TimeSpan(1,2,3);

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<TimeSpan>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Write_Version()
        {
            // Arrange.
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);
            var o = new Version(1,2,3,4 );

            // Act.
            writer.Write(o);
            stream.Position = 0;
            var result = reader.Read<Version>();

            // Assert.
            Assert.Equal(o, result);
        }

        [Fact]
        public void BinaryWriter_Read_Object()
        {
            // Arrange.

            // Act.
            var act = new Action(() =>
            {
                using var stream = new MemoryStream();
                using var reader = new BinaryReader(stream);
                reader.Read<object>();
            });

            // Assert.
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }
    }
}
