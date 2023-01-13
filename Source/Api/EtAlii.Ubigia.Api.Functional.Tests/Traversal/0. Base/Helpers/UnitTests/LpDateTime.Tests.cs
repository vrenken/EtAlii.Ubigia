// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;
using Moppet.Lapa.Parsers;
using Xunit;

public class LpDateTimeTests
{
    [Fact]
    public void LpDateTime_Parse_TimeSpan_00()
    {
        // Arrange.
        var parser = LpDateTime.TimeSpan();
        var text = "23:01";
        var result = TimeSpan.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseTimeSpan(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new TimeSpan(0, 23, 1, 0), result);
    }

    [Fact]
    public void LpDateTime_Parse_TimeSpan_01()
    {
        // Arrange.
        var parser = LpDateTime.TimeSpan();
        var text = "23:01";
        var result = TimeSpan.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseTimeSpan(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new TimeSpan(0, 23, 1, 0), result);
    }

    [Fact]
    public void LpDateTime_Parse_TimeSpan_02()
    {
        // Arrange.
        var parser = LpDateTime.TimeSpan();
        var text = "-23:01";
        var result = TimeSpan.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseTimeSpan(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(-new TimeSpan(0, 23, 1, 0), result);
    }

    [Fact]
    public void LpDateTime_Parse_TimeSpan_03()
    {
        // Arrange.
        var parser = LpDateTime.TimeSpan();
        var text = "2:23:01:20.123";
        var result = TimeSpan.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseTimeSpan(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new TimeSpan(2, 23, 01, 20, 123), result);
    }

    [Fact]
    public void LpDateTime_Parse_DateTime_00()
    {
        // Arrange.
        var parser = LpDateTime.DateTime();
        var text = "2015-07-28";
        var result = DateTime.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseDateTime(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new DateTime(2015, 07, 28), result);
    }

    [Fact]
    public void LpDateTime_Parse_DateTime_01()
    {
        // Arrange.
        var parser = LpDateTime.DateTime();
        var text = "2015-7-28 11:09";
        var result = DateTime.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseDateTime(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new DateTime(2015, 07, 28, 11, 09, 0), result);
    }

    [Fact]
    public void LpDateTime_Parse_DateTime_02()
    {
        // Arrange.
        var parser = LpDateTime.DateTime();
        var text = "2015-07-28 11:09:28";
        var result = DateTime.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseDateTime(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new DateTime(2015, 07, 28, 11, 09, 28), result);
    }

    [Fact]
    public void LpDateTime_Parse_DateTime_03()
    {
        // Arrange.
        var parser = LpDateTime.DateTime();
        var text = "2015-07-28 11:09:28.123";
        var result = DateTime.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseDateTime(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new DateTime(2015, 07, 28, 11, 09, 28, 123), result);
    }

    [Fact]
    public void LpDateTime_Parse_DateTime_04()
    {
        // Arrange.
        var parser = LpDateTime.DateTime();
        var text = "2015-07-28 11:09:28.123";
        var result = DateTime.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseDateTime(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new DateTime(2015, 07, 28, 11, 09, 28, 123), result);
    }

    [Fact]
    public void LpDateTime_Parse_DateTime_05()
    {
        // Arrange.
        var parser = LpDateTime.DateTime();
        var text = "2015-7-28   11:09";
        var result = DateTime.MinValue;

        // Act.
        var node = parser.Do(text);
        var success = LpDateTime.TryParseDateTime(node, ref result);

        // Assert.
        Assert.True(success);
        Assert.Equal(new DateTime(2015, 07, 28, 11, 09, 0), result);
    }

}
