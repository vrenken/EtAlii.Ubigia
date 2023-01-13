// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;
using Xunit;

public class IntegerValueParserTests : IDisposable
{
    private IIntegerValueParser _parser;

    public IntegerValueParserTests()
    {
        var nodeValidator = new NodeValidator();

        _parser = new IntegerValueParser(nodeValidator);
    }

    public void Dispose()
    {
        _parser = null;
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void IntegerValueParser_Parse_01()
    {
        // Arrange.
        const string text = "123456";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(123456, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_02()
    {
        // Arrange.
        const string text = "-123456";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(-123456, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_03()
    {
        // Arrange.
        const string text = "0";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(0, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_04()
    {
        // Arrange.
        const string text = "00";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(0, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_05()
    {
        // Arrange.
        const string text = "000";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(0, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_06()
    {
        // Arrange.
        const string text = "-0";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(0, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_07()
    {
        // Arrange.
        const string text = "a0";

        // Act.
        var act = new Action(() =>
        {
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                _parser.Parse(node);
            }
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }

    [Fact]
    public void IntegerValueParser_Parse_08()
    {
        // Arrange.
        const string text = ".0";

        // Act.
        var act = new Action(() =>
        {
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                _parser.Parse(node);
            }
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }

    [Fact]
    public void IntegerValueParser_Parse_09()
    {
        // Arrange.
        const string text = "0a";
        int? result = 0;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(0, result);
        Assert.Equal("a", node.Rest.ToString());
    }

    [Fact]
    public void IntegerValueParser_Parse_10()
    {
        // Arrange.
        const string text = "a0";

        // Act.
        var act = new Action(() =>
        {
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                _parser.Parse(node);
            }
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }

    [Fact]
    public void IntegerValueParser_Parse_11()
    {
        // Arrange.
        const string text = "0.";
        int? result = null;
        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(0, result);
        Assert.Equal(".", node.Rest.ToString());
    }

    [Fact]
    public void IntegerValueParser_Parse_12()
    {
        // Arrange.
        const string text = "+123456";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(+123456, result);
    }

    [Fact]
    public void IntegerValueParser_Parse_13()
    {
        // Arrange.
        const string text = "123-456";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(123, result);
        Assert.Equal("-456", node.Rest.ToString());
    }

    [Fact]
    public void IntegerValueParser_Parse_14()
    {
        // Arrange.
        const string text = "123+456";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(123, result);
        Assert.Equal("+456", node.Rest.ToString());
    }

    [Fact]
    public void IntegerValueParser_Parse_15()
    {
        // Arrange.
        const string text = "123a456";
        int? result = null;

        // Act.
        var node = _parser.Parser.Do(text);
        if (_parser.CanParse(node))
        {
            result = _parser.Parse(node);
        }

        // Assert.
        Assert.True(result.HasValue);
        Assert.Equal(123, result);
        Assert.Equal("a456", node.Rest.ToString());
    }
}
