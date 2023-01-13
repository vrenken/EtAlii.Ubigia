// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization.Tests;

using System;
using System.IO;
using Newtonsoft.Json;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class PropertyDictionaryJSonConverterReadJSonTests
{
    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_No_StartObject_Token()
    {
        // Arrange.
        var json = "\"d\":\"AAAAAA==\"}";

        // Act.
        var act = new Action(() => ReadJson(json));

        // Assert.
        Assert.Throws<JsonException>(act);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_No_PropertyName_Token()
    {
        // Arrange.
        var json = "{ }";

        // Act.
        var act = new Action(() => ReadJson(json));

        // Assert.
        Assert.Throws<JsonException>(act);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_No_Correct_Property_Name()
    {
        // Arrange.
        var json = "{\"d2\":\"AAAAAA==\"}";

        // Act.
        var act = new Action(() => ReadJson(json));

        // Assert.
        Assert.Throws<JsonException>(act);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_No_EndObject_Token()
    {
        // Arrange.
        var json = "{\"d2\":\"AAAAAA==\"";

        // Act.
        var act = new Action(() => ReadJson(json));

        // Assert.
        Assert.Throws<JsonException>(act);
    }


    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Empty()
    {
        // Arrange.
        var json = "{\"d\":\"AAAAAA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_String()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwEFV29ybGQ=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("World", result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int16()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwZ7AA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((short)123, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int16_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwb/fw==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(short.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int16_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwYAgA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(short.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int32()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwd7AAAA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(123, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int32_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwf///9/\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(int.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int32_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwcAAACA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(int.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int64()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwh7AAAAAAAAAA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((long)123, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int64_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwj/////////fw==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(long.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Int64_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwgAAAAAAAAAgA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(long.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt16()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwnSBA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((ushort)1234, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt16_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwn//w==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(ushort.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt16_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwkAAA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(ushort.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt32()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwrSBAAA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((uint)1234, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt32_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwr/////\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(uint.MaxValue, result["Hello"]);
    }


    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt32_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwoAAAAA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(uint.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt64()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwvSBAAAAAAAAA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((ulong)1234, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt64_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwv//////////w==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(ulong.MaxValue, result["Hello"]);
    }


    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_UInt64_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwsAAAAAAAAAAA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(ulong.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_None()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwA=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Null(result["Hello"]);
    }


    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Char()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwJh\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal('a', result["Hello"]);
    }


    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Char_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwLvv78=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(char.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Char_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwIA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(char.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Boolean_True()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwMB\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(true, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Boolean_False()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwMA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(false, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_SByte()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwR7\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((sbyte)123, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_SByte_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwR/\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(sbyte.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_SByte_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwSA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(sbyte.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Byte()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwV7\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((byte)123, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Byte_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwX/\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(byte.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Byte_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwUA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(byte.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Single()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwx56fZC\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((float)123.456, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Single_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwz//39/\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(float.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Single_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbwz//3//\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(float.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Double()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw13vp8aL91eQA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(123.456, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Double_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw3////////vfw==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(double.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Double_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw3////////v/w==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(double.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Decimal()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw5A4gEAAAAAAAAAAAAAAAMA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal((decimal)123.456, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Decimal_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw7///////////////8AAAAA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(decimal.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Decimal_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw7///////////////8AAACA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(decimal.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Local()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw8CAKmqjo+o0gg=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Local), result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Utc()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw8BAKmqjo+o0gg=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Utc), result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Unspecified()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw8AAKmqjo+o0gg=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(new DateTime(2015, 8, 19, 12, 13, 14, DateTimeKind.Unspecified), result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw8A/z839HUoyis=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(DateTime.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_DateTime_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbw8AAAAAAAAAAAA=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(DateTime.MinValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_TimeSpan()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbxAA4fUFAAAAAA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(TimeSpan.FromSeconds(10), result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_TimeSpan_Max()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbxD/////////fw==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(TimeSpan.MaxValue, result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_TimeSpan_Min()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbxAAAAAAAAAAgA==\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(TimeSpan.MinValue, result["Hello"]);
    }


    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Guid()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbxEfatM61o6qQr57h38XtNsF\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(Guid.Parse("3AD36A1F-8ED6-42AA-BE7B-877F17B4DB05"), result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Version()
    {
        // Arrange.
        var json = "{\"d\":\"AQAAAAVIZWxsbxIBAAAAAgAAAAMAAAAEAAAA\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(new Version(1, 2, 3, 4), result["Hello"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Complex_01()
    {
        // Arrange.
        var json = "{\"d\":\"BQAAAAVIZWxsbwEFV29ybGQFSW50MzIH0gQAAAdCb29sZWFuAwEETnVsbAAFSW50MTYG0gQ=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
        Assert.Equal("World", result["Hello"]);
        Assert.Equal(1234, result["Int32"]);
        Assert.True((bool)result["Boolean"]);
        Assert.Null(result["Null"]);
        Assert.Equal((short)1234, result["Int16"]);
    }

    [Fact]
    public void PropertyDictionaryJSonConverter_ReadJson_Complex_02()
    {
        // Arrange.
        var json = "{\"d\":\"BgAAAAVIZWxsbwEFV29ybGQFSW50MzIH0gQAAAdCb29sZWFuAwEETnVsbAAFSW50MTYG0gQkZjUzZTcxY2UtNjhlNS00OGEyLWEwMjQtNWJhOWFkMTY5MTI4B44xDkY=\"}";

        // Act.
        var result = ReadJson(json);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(6, result.Count);
        Assert.Equal("World", result["Hello"]);
        Assert.Equal(1234, result["Int32"]);
        Assert.Equal(true, result["Boolean"]);
        Assert.Null(result["Null"]);
        Assert.Equal((short) 1234, result["Int16"]);
        Assert.Equal(1175335310, result["f53e71ce-68e5-48a2-a024-5ba9ad169128"]);
    }

    private PropertyDictionary ReadJson(string json)
    {
        var serializer = (Serializer)Serializer.Default;
        var converter = new PropertyDictionaryJSonConverter();

        using var textReader = new StringReader(json);
        using var jsonReader = new JsonTextReader(textReader);

        return (PropertyDictionary)converter.ReadJson(jsonReader, typeof(PropertyDictionary), null, serializer);
    }
}
