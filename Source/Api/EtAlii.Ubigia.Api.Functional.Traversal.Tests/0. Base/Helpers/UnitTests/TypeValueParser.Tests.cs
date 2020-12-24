namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class TypeValueParserTests : IDisposable
    {
        private ITypeValueParser _parser;

        public TypeValueParserTests()
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            var constantHelper = new ConstantHelper();
            _parser = new TypeValueParser(nodeValidator, nodeFinder, constantHelper);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_01()
        {
            // Arrange.
            const string text = "EtAlii";
            string result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(text, result);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void TypeValueParser_Parse_02()
        //[
        //    // Arrange.
        //    const string text = "EtAlii.."

        //    // Act.
        //    var act = new Action(() =>
        //    [
        //        var node = _parser.Parser.Do(text)
        //        if [_parser.CanParse[node]]
        //        [
        //            _parser.Parse(node)
        //        ]
        //    ])

        //    // Assert.
        //    Assert.Throws<ScriptParserException>(act)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_03()
        {
            // Arrange.
            const string text = "EtAlii.Ubigia";
            string result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(text, result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_04()
        {
            // Arrange.
            const string text = "EtAlii.Ubigia.Test";
            string result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(text, result);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void TypeValueParser_Parse_05()
        //[
        //    // Arrange.
        //    const string text = "EtAlii."

        //    // Act.
        //    var act = new Action(() =>
        //    [
        //        var node = _parser.Parser.Do(text)
        //        if [_parser.CanParse[node]]
        //        [
        //            _parser.Parse(node)
        //        ]
        //    ])

        //    // Assert.
        //    Assert.Throws<ScriptParserException>(act)
        //]
        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void TypeValueParser_Parse_06()
        //[
        //    // Arrange.
        //    const string text = "EtAlii..Ubigia"

        //    // Act.
        //    var act = new Action(() =>
        //    [
        //        var node = _parser.Parser.Do(text)
        //        if [_parser.CanParse[node]]
        //        [
        //            _parser.Parse(node)
        //        ]
        //    ])

        //    // Assert.
        //    Assert.Throws<ScriptParserException>(act)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_07()
        {
            // Arrange.
            const string text = ".EtAlii.Ubigia";

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

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void TypeValueParser_Parse_08()
        //[
        //    // Arrange.
        //    const string text = "EtAlii. .Ubigia"

        //    // Act.
        //    var act = new Action(() =>
        //    [
        //        var node = _parser.Parser.Do(text)
        //        if [_parser.CanParse[node]]
        //        [
        //            _parser.Parse(node)
        //        ]
        //    ])

        //    // Assert.
        //    Assert.Throws<ScriptParserException>(act)
        //]
        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_09()
        {
            // Arrange.
            const string text = " EtAlii..Ubigia";

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
