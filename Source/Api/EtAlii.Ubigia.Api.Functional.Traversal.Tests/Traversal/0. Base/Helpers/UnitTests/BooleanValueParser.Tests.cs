namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class BooleanValueParserTests : IDisposable
    {
        private IBooleanValueParser _parser;

        public BooleanValueParserTests()
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            _parser = new BooleanValueParser(nodeValidator, nodeFinder);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_01()
        {
            // Arrange.
            const string text = "true";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.True(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_02()
        {
            // Arrange.
            const string text = "TRUE";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.True(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_03()
        {
            // Arrange.
            const string text = "True";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.True(result);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_04()
        {
            // Arrange.
            const string text = "false";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.False(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_05()
        {
            // Arrange.
            const string text = "FALSE";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.False(result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_06()
        {
            // Arrange.
            const string text = "False";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.False(result);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void BooleanValueParser_Parse_07()
        {
            // Arrange.
            const string text = "F alse";

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
        public void BooleanValueParser_Parse_08()
        {
            // Arrange.
            const string text = "T rue";

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
        public void BooleanValueParser_Parse_09()
        {
            // Arrange.
            const string text = "F1alse";

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
        public void BooleanValueParser_Parse_10()
        {
            // Arrange.
            const string text = "T1rue";

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
