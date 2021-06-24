// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests
{
    using System;
    using System.IO;
    using Xunit;

    public class PropertyDictionaryJSonConverterWriteJSonTests
    {
        [Fact]
        public void PropertyDictionaryJSonConverter_Create()
        {
            // Arrange.

            // Act.
            var converter = new PropertyDictionaryJSonConverter();

            // Assert.
            Assert.NotNull(converter);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_CanConvert_True()
        {
            // Arrange.
            var converter = new PropertyDictionaryJSonConverter();

            // Act.
            var result = converter.CanConvert(typeof(PropertyDictionary));

            // Assert.
            Assert.True(result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_CanConvert_False_1()
        {
            // Arrange.
            var converter = new PropertyDictionaryJSonConverter();

            // Act.
            var result = converter.CanConvert(typeof(string));

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_CanConvert_False_2()
        {
            // Arrange.
            var converter = new PropertyDictionaryJSonConverter();

            // Act.
            var result = converter.CanConvert(null);

            // Assert.
            Assert.False(result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Empty()
        {
            // Arrange.
            var properties = new PropertyDictionary();

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_String()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = "World"
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":1,\"v\":\"World\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int16()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (short) 123
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":6,\"v\":123}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int16_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = short.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":6,\"v\":32767}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int16_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = short.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":6,\"v\":-32768}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int32()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = 123
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":7,\"v\":123}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int32_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = int.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":7,\"v\":2147483647}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int32_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = int.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":7,\"v\":-2147483648}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int64()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (long) 123
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":8,\"v\":123}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int64_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = long.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":8,\"v\":9223372036854775807}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Int64_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = long.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":8,\"v\":-9223372036854775808}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt16()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (ushort) 1234
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":9,\"v\":1234}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt16_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = ushort.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":9,\"v\":65535}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt16_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = ushort.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":9,\"v\":0}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt32()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (uint) 1234
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":10,\"v\":1234}]", result);
        }


        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt32_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = uint.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":10,\"v\":4294967295}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt32_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = uint.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":10,\"v\":0}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt64()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (ulong) 1234
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":11,\"v\":\"0gQAAAAAAAA=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt64_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = ulong.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":11,\"v\":\"//////////8=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_UInt64_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = ulong.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":11,\"v\":\"AAAAAAAAAAA=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_None()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = null
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":0}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Char()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = 'a'
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":2,\"v\":\"a\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Char_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = char.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":2,\"v\":\"" + (char)0xFFFF + "\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Char_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = char.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":2,\"v\":\"\\u0000\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Boolean_True()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = true
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":3,\"v\":true}]", result);
        }
        
        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Boolean_False()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = false
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":3,\"v\":false}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_SByte()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (sbyte) 123
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":4,\"v\":123}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_SByte_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = sbyte.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":4,\"v\":127}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_SByte_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = sbyte.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":4,\"v\":-128}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Byte()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (byte) 123
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":5,\"v\":123}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Byte_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = byte.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":5,\"v\":255}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Byte_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = byte.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":5,\"v\":0}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Single()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (float) 123.456
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":12,\"v\":123.456}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Single_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = float.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":12,\"v\":3.4028235E+38}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Single_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = float.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":12,\"v\":-3.4028235E+38}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Double()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = 123.456
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":13,\"v\":123.456}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Double_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = double.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":13,\"v\":1.7976931348623157E+308}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Double_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = double.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":13,\"v\":-1.7976931348623157E+308}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Decimal()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = (decimal) 123.456
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":14,\"v\":\"123.456\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Decimal_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = decimal.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":14,\"v\":\"79228162514264337593543950335\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Decimal_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = decimal.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":14,\"v\":\"-79228162514264337593543950335\"}]", result);
        }

        // TODO: Verify this and the corresponding readJson variant. They should work but don't give
        // consistent results on all machines.
        [Fact(Skip = "Not working as expected.")] 
        public void PropertyDictionaryJSonConverter_WriteJson_DateTime_Local()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Local)
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":15,\"v\":\"ANkhy36o0og=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_DateTime_Utc()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Utc)
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":15,\"v\":\"AKmqjo+o0kg=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_DateTime_Unspecified()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Unspecified)
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":15,\"v\":\"AKmqjo+o0gg=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_DateTime_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = DateTime.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":15,\"v\":\"/z839HUoyis=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_DateTime_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = DateTime.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":15,\"v\":\"AAAAAAAAAAA=\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_TimeSpan()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = TimeSpan.FromSeconds(10)
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":16,\"v\":\"00:00:10\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_TimeSpan_Max()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = TimeSpan.MaxValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":16,\"v\":\"10675199.02:48:05.4775807\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_TimeSpan_Min()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = TimeSpan.MinValue
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":16,\"v\":\"-10675199.02:48:05.4775808\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Guid()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = Guid.Parse("3AD36A1F-8ED6-42AA-BE7B-877F17B4DB05")
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":17,\"v\":\"3ad36a1f-8ed6-42aa-be7b-877f17b4db05\"}]", result);
        }

        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Version()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = new Version(1, 2, 3, 4)
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":18,\"v\":\"1.2.3.4\"}]", result);

        }


        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Complex_01()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = "World",
                ["Int32"] = 1234,
                ["Boolean"] = true,
                ["Null"] = null,
                ["Int16"] = (short) 1234
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("[{\"k\":\"Hello\",\"t\":1,\"v\":\"World\"},{\"k\":\"Int32\",\"t\":7,\"v\":1234},{\"k\":\"Boolean\",\"t\":3,\"v\":true},{\"k\":\"Null\",\"t\":0},{\"k\":\"Int16\",\"t\":6,\"v\":1234}]", result);
        }

        private string WriteJSon(PropertyDictionary properties)
        {
            var serializer = (Serializer)new SerializerFactory().Create();
            var converter = new PropertyDictionaryJSonConverter();

            using var textWriter = new StringWriter();
            using var jsonWriter = new Newtonsoft.Json.JsonTextWriter(textWriter);
            converter.WriteJson(jsonWriter, properties, serializer);
            return textWriter.ToString();
        }
    }
}
