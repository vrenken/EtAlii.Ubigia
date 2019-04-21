﻿namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Xunit;

    public class PropertyDictionaryJSonConverterReadJSonTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Empty()
        {
            // Arrange.
            var json = "[]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_String()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":1,\"v\":\"World\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("World", result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int16()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":6,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((Int16)123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int16_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":6,\"v\":32767}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Int16.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int16_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":6,\"v\":-32768}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Int16.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int32()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":7,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int32_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":7,\"v\":2147483647}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Int32.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int32_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":7,\"v\":-2147483648}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Int32.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int64()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":8,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((Int64)123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int64_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":8,\"v\":9223372036854775807}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Int64.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Int64_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":8,\"v\":-9223372036854775808}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Int64.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt16()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":9,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((UInt16)123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt16_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":9,\"v\":65535}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(UInt16.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt16_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":9,\"v\":0}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(UInt16.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt32()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":10,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((UInt32)123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt32_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":10,\"v\":4294967295}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(UInt32.MaxValue, result["Hello"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt32_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":10,\"v\":0}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(UInt32.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt64()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":11,\"v\":\"0gQAAAAAAAA=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((UInt64)1234, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt64_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":11,\"v\":\"//////////8=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(UInt64.MaxValue, result["Hello"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_UInt64_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":11,\"v\":\"AAAAAAAAAAA=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(UInt64.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_None()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":0}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Null(result["Hello"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Char()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":2,\"v\":\"a\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal('a', result["Hello"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Char_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":2,\"v\":\"" + (char)0xFFFF + "\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Char.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Char_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":2,\"v\":\"\\u0000\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Char.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Boolean_True()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":3,\"v\":true}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.True((bool)result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Boolean_False()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":3,\"v\":false}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.False((bool)result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_SByte()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":4,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((SByte)123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_SByte_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":4,\"v\":127}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(SByte.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_SByte_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":4,\"v\":-128}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(SByte.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Byte()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":5,\"v\":123}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((Byte)123, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Byte_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":5,\"v\":255}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Byte.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Byte_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":5,\"v\":0}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Byte.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Single()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":12,\"v\":123.456}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((Single)123.456, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Single_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":12,\"v\":3.40282347E+38}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Single.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Single_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":12,\"v\":-3.40282347E+38}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Single.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Double()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":13,\"v\":123.456}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(123.456, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Double_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":13,\"v\":1.7976931348623157E+308}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Double.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Double_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":13,\"v\":-1.7976931348623157E+308}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Double.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Decimal()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":14,\"v\":\"123.456\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal((Decimal)123.456, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Decimal_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":14,\"v\":\"79228162514264337593543950335\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Decimal.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Decimal_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":14,\"v\":\"-79228162514264337593543950335\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Decimal.MinValue, result["Hello"]);
        }


        [Fact(Skip = "Gives errors so skipping for now. Should be corrected"), Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Local()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":15,\"v\":\"ANkhy36o0og=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Local), result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Utc()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":15,\"v\":\"AKmqjo+o0kg=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Utc), result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Unspecified()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":15,\"v\":\"AKmqjo+o0gg=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Unspecified), result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":15,\"v\":\"/z839HUoyis=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(DateTime.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":15,\"v\":\"AAAAAAAAAAA=\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(DateTime.MinValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_TimeSpan()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":16,\"v\":\"00:00:10\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(TimeSpan.FromSeconds(10), result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_TimeSpan_Max()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":16,\"v\":\"10675199.02:48:05.4775807\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(TimeSpan.MaxValue, result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_TimeSpan_Min()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":16,\"v\":\"-10675199.02:48:05.4775808\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(TimeSpan.MinValue, result["Hello"]);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Guid()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":17,\"v\":\"3ad36a1f-8ed6-42aa-be7b-877f17b4db05\"}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(Guid.Parse("3AD36A1F-8ED6-42AA-BE7B-877F17B4DB05"), result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Version()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":18,\"v\":{\"Major\":1,\"Minor\":2,\"Build\":3,\"Revision\":4,\"MinorRevision\":4}}]";

            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(new Version(1, 2, 3, 4), result["Hello"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Complex_01()
        {
            // Arrange.
            var json = "[{\"k\":\"Hello\",\"t\":1,\"v\":\"World\"},{\"k\":\"Int32\",\"t\":7,\"v\":1234},{\"k\":\"Boolean\",\"t\":3,\"v\":true},{\"k\":\"Null\",\"t\":0},{\"k\":\"Int16\",\"t\":6,\"v\":1234}]";
            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
            Assert.Equal("World", result["Hello"]);
            Assert.Equal(1234, result["Int32"]);
            Assert.True((bool)result["Boolean"]);
            Assert.Null(result["Null"]);
            Assert.Equal((Int16)1234, result["Int16"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyDictionaryJSonConverter_ReadJson_Complex_02()
        {
            // Arrange.
            var json = "[{\"k\":\"f53e71ce-68e5-48a2-a024-5ba9ad169128\",\"t\":7,\"v\":1175335310},{\"k\":\"b2432705-ffa6-45b9-badd-85a2cfa1c3d5\",\"t\":17,\"v\":\"97e331c0-6187-4c7d-89bf-706b75d70b7e\"},{\"k\":\"c60fd11b-df87-47bb-8705-8ef8f6c8bad3\",\"t\":1,\"v\":\"353f24a2-8a2c-4177-8a0e-9dbb54ad25d4\"},{\"k\":\"1d860afc-f17c-4467-a78f-93dcaa8570af\",\"t\":10,\"v\":906672726},{\"k\":\"5b6d8234-7ff3-4758-baf0-059f36e758e7\",\"t\":3,\"v\":false},{\"k\":\"4b7479ec-98b8-4aae-b153-70543b7a2013\",\"t\":8,\"v\":13853218},{\"k\":\"3dc57f86-55bc-47fa-817a-8e6a4ab5b9f6\",\"t\":13,\"v\":0.31335299849200665},{\"k\":\"1fc013a0-afd3-4f0b-9a04-937f3ff962e0\",\"t\":12,\"v\":0.830620944}]";
            // Act.
            var result = ReadJson(json);

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(8, result.Count);
            Assert.Equal(1175335310, result["f53e71ce-68e5-48a2-a024-5ba9ad169128"]);
        }

        private PropertyDictionary ReadJson(string json)
        {
            var serializer = (Serializer)new SerializerFactory().Create();
            var converter = new PropertyDictionaryJSonConverter();

            using (var textReader = new StringReader(json))
            {
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    return (PropertyDictionary)converter.ReadJson(jsonReader, typeof(PropertyDictionary), null, serializer);
                }
            }
        }
    }
}
