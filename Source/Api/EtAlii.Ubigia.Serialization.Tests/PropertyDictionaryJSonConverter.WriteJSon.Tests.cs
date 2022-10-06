// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests
{
    using System;
    using System.IO;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
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
            Assert.Equal("{\"d\":\"AAAAAA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwEFV29ybGQ=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwZ7AA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwb/fw==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwYAgA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwd7AAAA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwf///9/\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwcAAACA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwh7AAAAAAAAAA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwj/////////fw==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwgAAAAAAAAAgA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwnSBA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwn//w==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwkAAA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwrSBAAA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwr/////\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwoAAAAA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwvSBAAAAAAAAA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwv//////////w==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwsAAAAAAAAAAA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwA=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwJh\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwLvv78=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwIA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwMB\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwMA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwR7\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwR/\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwSA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwV7\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwX/\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwUA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwx56fZC\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwz//39/\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbwz//3//\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw13vp8aL91eQA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw3////////vfw==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw3////////v/w==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw5A4gEAAAAAAAAAAAAAAAMA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw7///////////////8AAAAA\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw7///////////////8AAACA\"}", result);
        }

        // TODO: Verify this and the corresponding readJson variant. They should work but don't give
        // consistent results on all machines.
        [Fact] //(Skip = "Not working as expected.")]
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw8CAKmqjo+o0gg=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw8BAKmqjo+o0gg=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw8AAKmqjo+o0gg=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw8A/z839HUoyis=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbw8AAAAAAAAAAAA=\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbxAA4fUFAAAAAA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbxD/////////fw==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbxAAAAAAAAAAgA==\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbxEfatM61o6qQr57h38XtNsF\"}", result);
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
            Assert.Equal("{\"d\":\"AQAAAAVIZWxsbxIBAAAAAgAAAAMAAAAEAAAA\"}", result);

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
            Assert.Equal("{\"d\":\"BQAAAAVIZWxsbwEFV29ybGQFSW50MzIH0gQAAAdCb29sZWFuAwEETnVsbAAFSW50MTYG0gQ=\"}", result);
        }


        [Fact]
        public void PropertyDictionaryJSonConverter_WriteJson_Complex_02()
        {
            // Arrange.
            var properties = new PropertyDictionary
            {
                ["Hello"] = "World",
                ["Int32"] = 1234,
                ["Boolean"] = true,
                ["Null"] = null,
                ["Int16"] = (short) 1234,
                ["f53e71ce-68e5-48a2-a024-5ba9ad169128"] = 1175335310
            };

            // Act.
            var result = WriteJSon(properties);

            // Assert.
            Assert.Equal("{\"d\":\"BgAAAAVIZWxsbwEFV29ybGQFSW50MzIH0gQAAAdCb29sZWFuAwEETnVsbAAFSW50MTYG0gQkZjUzZTcxY2UtNjhlNS00OGEyLWEwMjQtNWJhOWFkMTY5MTI4B44xDkY=\"}", result);
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
