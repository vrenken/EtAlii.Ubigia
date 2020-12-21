namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class RootDefinitionSubjectParserTests : IDisposable
    {
        private IRootDefinitionSubjectParser _parser;

        public RootDefinitionSubjectParserTests()
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            var constantHelper = new ConstantHelper();
            var typeValueParser = new TypeValueParser(nodeValidator, nodeFinder, constantHelper);
            _parser = new RootDefinitionSubjectParser(nodeValidator, nodeFinder, typeValueParser);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDefinitionSubject_Parse_01()
        {
            // Arrange.
            const string text = "EtAlii";
            RootDefinitionSubject result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node) as RootDefinitionSubject;
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(text, result.Type);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDefinitionSubject_Parse_02()
        {
            // Arrange.
            const string text = "EtAlii.Ubigia";
            RootDefinitionSubject result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node) as RootDefinitionSubject;
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(text, result.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDefinitionSubject_Parse_03()
        {
            // Arrange.
            const string text = "EtAlii.Ubigia.Test";
            RootDefinitionSubject result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node) as RootDefinitionSubject;
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(text, result.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDefinitionSubject_Parse_04()
        {
            // Arrange.
            const string type = "EtAlii.Ubigia.Roots.Object";
            //const string schema = "/[Words]/[Number]"
            var text = type;//$"[type]:[schema]"
            RootDefinitionSubject result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node) as RootDefinitionSubject;
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(type, result.Type);
            //Assert.NotNull(result.Schema)
            //Assert.Equal(schema.ToUpper(), result.Schema.ToString())
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDefinitionSubject_Parse_05()
        {
            // Arrange.
            const string type = "EtAlii.Ubigia.Roots.Time";
            //const string schema = "/[Year]/[Month]/[Day]/[Hour]/[Minute]/[Second]"
            var text = type;//$"[type]:[schema]"
            RootDefinitionSubject result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node) as RootDefinitionSubject;
            }

            // Assert.
            Assert.NotNull(result);
            Assert.Equal(type, result.Type);
            //Assert.NotNull(result.Schema)
            //Assert.Equal(schema.ToUpper(), result.Schema.ToString())
        }
    }
}
