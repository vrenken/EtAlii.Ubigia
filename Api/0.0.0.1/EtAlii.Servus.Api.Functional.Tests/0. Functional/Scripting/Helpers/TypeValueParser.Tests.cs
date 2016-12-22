namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    
    using Moppet.Lapa;
    using Xunit;


    public partial class TypeValueParser_Tests : IDisposable
    {
        private ITypeValueParser _parser;

        public TypeValueParser_Tests()
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            var constantHelper = new ConstantHelper();
            _parser = new TypeValueParser(nodeValidator, nodeFinder, constantHelper);
        }

        public void Dispose()
        {
            _parser = null;
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
        //{
        //    // Arrange.
        //    const string text = "EtAlii..";

        //    // Act.
        //    var act = new Action(() =>
        //    {
        //        var node = _parser.Parser.Do(text);
        //        if (_parser.CanParse(node))
        //        {
        //            _parser.Parse(node);
        //        }
        //    });

        //    // Assert.
        //    ExceptionAssert.Throws<ScriptParserException>(act);
        //}

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_03()
        {
            // Arrange.
            const string text = "EtAlii.Servus";
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
            const string text = "EtAlii.Servus.Test";
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
        //{
        //    // Arrange.
        //    const string text = "EtAlii.";

        //    // Act.
        //    var act = new Action(() =>
        //    {
        //        var node = _parser.Parser.Do(text);
        //        if (_parser.CanParse(node))
        //        {
        //            _parser.Parse(node);
        //        }
        //    });

        //    // Assert.
        //    ExceptionAssert.Throws<ScriptParserException>(act);
        //}

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void TypeValueParser_Parse_06()
        //{
        //    // Arrange.
        //    const string text = "EtAlii..Servus";

        //    // Act.
        //    var act = new Action(() =>
        //    {
        //        var node = _parser.Parser.Do(text);
        //        if (_parser.CanParse(node))
        //        {
        //            _parser.Parse(node);
        //        }
        //    });

        //    // Assert.
        //    ExceptionAssert.Throws<ScriptParserException>(act);
        //}

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_07()
        {
            // Arrange.
            const string text = ".EtAlii.Servus";

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
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        //[Fact, Trait("Category", TestAssembly.Category)]
        //public void TypeValueParser_Parse_08()
        //{
        //    // Arrange.
        //    const string text = "EtAlii. .Servus";

        //    // Act.
        //    var act = new Action(() =>
        //    {
        //        var node = _parser.Parser.Do(text);
        //        if (_parser.CanParse(node))
        //        {
        //            _parser.Parse(node);
        //        }
        //    });

        //    // Assert.
        //    ExceptionAssert.Throws<ScriptParserException>(act);
        //}


        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeValueParser_Parse_09()
        {
            // Arrange.
            const string text = " EtAlii..Servus";

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
            ExceptionAssert.Throws<ScriptParserException>(act);
        }


    }
}