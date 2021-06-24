// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class TimeSpanValueParserTests : IDisposable
    {
        private ITimeSpanValueParser _parser;

        public TimeSpanValueParserTests()
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            _parser = new TimeSpanValueParser(nodeValidator, nodeFinder);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_01()
        {
            // Arrange.
            const string text = "23:01";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(23, 01, 0), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_02()
        {
            // Arrange.
            const string text = "00:01";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(0, 01, 0), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_03()
        {
            // Arrange.
            const string text = "00:00";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(0, 0, 0), result);
        }

        // Only positive timespans for now.
        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_04()
        {
            // Arrange.
            const string text = "-23:01";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(-23, -01, 0), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_05()
        {
            // Arrange.
            const string text = "27:68";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(27, 68, 0), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_06()
        {
            // Arrange.
            const string text = "27:68:69";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(27, 68, 69), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_07()
        {
            // Arrange.
            const string text = "27:68:69.896";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(0, 27, 68, 69, 896), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_08()
        {
            // Arrange.
            const string text = "201:27:68:69.896";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(201, 27, 68, 69, 896), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_09()
        {
            // Arrange.
            const string text = "201:27:68:69";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(201, 27, 68, 69, 0), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_10()
        {
            // Arrange.
            const string text = "27:68:69";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new TimeSpan(0, 27, 68, 69, 0), result);
        }


    }
}
