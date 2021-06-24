// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class FloatValueParserTests : IDisposable
    {
        private IFloatValueParser _parser;

        public FloatValueParserTests()
        {
            var nodeValidator = new NodeValidator();
            _parser = new FloatValueParser(nodeValidator);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_01()
        {
            // Arrange.
            const string text = "123.456";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(123.456f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_02()
        {
            // Arrange.
            const string text = "-123.456";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(-123.456f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_03()
        {
            // Arrange.
            const string text = "0.0";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(0.0f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_04()
        {
            // Arrange.
            const string text = "0.00";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(0.0f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_05()
        {
            // Arrange.
            const string text = "00.0";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(0.0f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_06()
        {
            // Arrange.
            const string text = "-0.0";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(0.0f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_07()
        {
            // Arrange.
            const string text = "a.0";

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_08()
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


        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_09()
        {
            // Arrange.
            const string text = "0.a";

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


        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_10()
        {
            // Arrange.
            const string text = "a.0";

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


        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_11()
        {
            // Arrange.
            const string text = "0.";

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


        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_12()
        {
            // Arrange.
            const string text = "+123.456";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(+123.456f, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_13()
        {
            // Arrange.
            const string text = "123-456";

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_14()
        {
            // Arrange.
            const string text = "123+456";

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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FloatValueParser_Parse_15()
        {
            // Arrange.
            const string text = "123a456";

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

    }
}
