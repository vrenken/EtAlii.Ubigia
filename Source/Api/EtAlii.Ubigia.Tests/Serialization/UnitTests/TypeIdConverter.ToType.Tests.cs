namespace EtAlii.Ubigia.Tests
{
    using System;
    using Xunit;

    public partial class TypeIdConverterTests
    {
        [Fact]
        public void TypeIdConverter_ToType_Invalid()
        {
            // Arrange.

            // Act.
            var act = new Action(() => TypeIdConverter.ToType((TypeId)254));

            // Assert.
            Assert.Throws<NotSupportedException>(act);
        }

        [Fact]
        public void TypeIdConverter_ToTypeId_Invalid()
        {
            // Arrange.
            var o = new object();

            // Act.
            var act = new Action(() => TypeIdConverter.ToTypeId(o));

            // Assert.
            Assert.Throws<NotSupportedException>(act);
        }

        [Fact]
        public void TypeIdConverter_ToType_Boolean()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Boolean);

            // Assert.
            Assert.Equal(typeof(bool), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Byte()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Byte);

            // Assert.
            Assert.Equal(typeof(byte), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Char()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Char);

            // Assert.
            Assert.Equal(typeof(char), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Decimal()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Decimal);

            // Assert.
            Assert.Equal(typeof(decimal), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Double()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Double);

            // Assert.
            Assert.Equal(typeof(double), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Guid()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Guid);

            // Assert.
            Assert.Equal(typeof(Guid), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Int16()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Int16);

            // Assert.
            Assert.Equal(typeof(short), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Int32()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Int32);

            // Assert.
            Assert.Equal(typeof(int), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Int64()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Int64);

            // Assert.
            Assert.Equal(typeof(long), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_UInt16()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.UInt16);

            // Assert.
            Assert.Equal(typeof(ushort), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_UInt32()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.UInt32);

            // Assert.
            Assert.Equal(typeof(uint), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_UInt64()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.UInt64);

            // Assert.
            Assert.Equal(typeof(ulong), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_None()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.None);

            // Assert.
            Assert.Null(type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Range()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Range);

            // Assert.
            Assert.Equal(typeof(Range), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Single()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Single);

            // Assert.
            Assert.Equal(typeof(float), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_String()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.String);

            // Assert.
            Assert.Equal(typeof(string), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_Version()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.Version);

            // Assert.
            Assert.Equal(typeof(Version), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_DateTime()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.DateTime);

            // Assert.
            Assert.Equal(typeof(DateTime), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_SByte()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.SByte);

            // Assert.
            Assert.Equal(typeof(sbyte), type);
        }

        [Fact]
        public void TypeIdConverter_ToType_TimeSpan()
        {
            // Arrange.

            // Act.
            var type = TypeIdConverter.ToType(TypeId.TimeSpan);

            // Assert.
            Assert.Equal(typeof(TimeSpan), type);
        }
    }
}
