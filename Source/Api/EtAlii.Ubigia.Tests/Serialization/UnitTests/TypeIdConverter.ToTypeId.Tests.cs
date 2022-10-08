namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public partial class TypeIdConverterTests
    {
        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Boolean()
        {
            // Arrange.

            // Act.
            var typeId = TypeIdConverter.ToTypeId(true);

            // Assert.
            Assert.Equal(TypeId.Boolean, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Byte()
        {
            // Arrange.
            var o = (byte)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Byte, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Char()
        {
            // Arrange.
            var o = (char)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Char, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Decimal()
        {
            // Arrange.
            var o = (decimal)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Decimal, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Double()
        {
            // Arrange.
            var o = (double)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Double, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Guid()
        {
            // Arrange.
            var o = Guid.NewGuid();

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Guid, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Int16()
        {
            // Arrange.
            var o = (short)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Int16, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Int32()
        {
            // Arrange.
            var o = 123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Int32, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Int64()
        {
            // Arrange.
            var o = (long)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Int64, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_UInt16()
        {
            // Arrange.
            var o = (ushort)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.UInt16, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_UInt32()
        {
            // Arrange.
            var o = (uint)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.UInt32, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_UInt64()
        {
            // Arrange.
            var o = (ulong)123;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.UInt64, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_None()
        {
            // Arrange.
            var o = (object)null;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.None, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Range()
        {
            // Arrange.
            var o = new Range(1,2 );

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Range, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Single()
        {
            // Arrange.
            var o = 1.2f;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Single, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_String()
        {
            // Arrange.
            var o = "test";

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.String, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_Version()
        {
            // Arrange.
            var o = new Version(1, 2, 3, 4);

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.Version, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_DateTime()
        {
            // Arrange.
            var o = new DateTime(1, 2, 3);

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.DateTime, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_SByte()
        {
            // Arrange.
            var o = (sbyte)2;

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.SByte, typeId);
        }

        [Fact]
        public void TypeIdConverter_ToType_ToTypeId_TimeSpan()
        {
            // Arrange.
            var o = new TimeSpan(1, 2, 3, 4, 5);

            // Act.
            var typeId = TypeIdConverter.ToTypeId(o);

            // Assert.
            Assert.Equal(TypeId.TimeSpan, typeId);
        }
    }
}
