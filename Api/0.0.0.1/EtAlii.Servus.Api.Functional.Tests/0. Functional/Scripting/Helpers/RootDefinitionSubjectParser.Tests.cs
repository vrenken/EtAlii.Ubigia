namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    
    using Moppet.Lapa;
    using Xunit;

    
    public partial class RootDefinitionSubjectParser_Tests : IDisposable
    {
        private IRootDefinitionSubjectParser _parser;

        public RootDefinitionSubjectParser_Tests()
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            var constantHelper = new ConstantHelper();
            var typeValueParser = new TypeValueParser(nodeValidator, nodeFinder, constantHelper);

            var integerValueParser = new IntegerValueParser(nodeValidator);
            var quotedTextParser = new QuotedTextParser(nodeValidator, nodeFinder, constantHelper);
            var dateTimeValueParser = new DateTimeValueParser(nodeValidator);
            var timeSpanValueParser = new TimeSpanValueParser(nodeValidator, nodeFinder);
            var booleanValueParser = new BooleanValueParser(nodeValidator, nodeFinder);
            var floatValueParser = new FloatValueParser(nodeValidator);
            var newLineParser = new NewLineParser();

            var pathRelationParserBuilder = new PathRelationParserBuilder();

            var conditionParser = new ConditionParser(nodeValidator, quotedTextParser, dateTimeValueParser, timeSpanValueParser, booleanValueParser, integerValueParser, floatValueParser, nodeFinder);
            var traversingWildcardPathSubjectPartParser = new TraversingWildcardPathSubjectPartParser(nodeValidator, nodeFinder, integerValueParser);
            var wildcardPathSubjectPartParser = new WildcardPathSubjectPartParser(nodeValidator, constantHelper, nodeFinder);
            var conditionalPathSubjectPartParser = new ConditionalPathSubjectPartParser(nodeValidator, conditionParser, newLineParser, nodeFinder);
            var constantPathSubjectPartParser = new ConstantPathSubjectPartParser(nodeValidator, constantHelper, nodeFinder);
            var variablePathSubjectPartParser = new VariablePathSubjectPartParser(nodeValidator, nodeFinder);
            var identifierPathSubjectPartParser = new IdentifierPathSubjectPartParser(nodeValidator, nodeFinder);
            var isParentOfPathSubjectPartParser = new IsParentOfPathSubjectPartParser(nodeValidator, pathRelationParserBuilder);
            var isChildOfPathSubjectPartParser = new IsChildOfPathSubjectPartParser(nodeValidator, pathRelationParserBuilder);
            var downdateOfPathSubjectPartParser = new DowndateOfPathSubjectPartParser(nodeValidator, pathRelationParserBuilder);
            var updatesOfPathSubjectPartParser = new UpdatesOfPathSubjectPartParser(nodeValidator, pathRelationParserBuilder);
            var typedPathSubjectPartParser = new TypedPathSubjectPartParser(nodeValidator, nodeFinder);
            var pathSubjectPartsParser = new PathSubjectPartsParser(traversingWildcardPathSubjectPartParser, wildcardPathSubjectPartParser, conditionalPathSubjectPartParser, constantPathSubjectPartParser, variablePathSubjectPartParser, identifierPathSubjectPartParser, isParentOfPathSubjectPartParser, isChildOfPathSubjectPartParser, downdateOfPathSubjectPartParser, updatesOfPathSubjectPartParser, typedPathSubjectPartParser, nodeValidator);
            _parser = new RootDefinitionSubjectParser(nodeValidator, nodeFinder, typeValueParser, pathSubjectPartsParser);
        }

        public void Dispose()
        {
            _parser = null;
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
            const string text = "EtAlii.Servus";
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
            const string text = "EtAlii.Servus.Test";
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
            const string type = "EtAlii.Servus.Roots.Object";
            //const string schema = "/[Words]/[Number]";
            var text = type;//$"{type}:{schema}";
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
            //Assert.NotNull(result.Schema);
            //Assert.Equal(schema.ToUpper(), result.Schema.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootDefinitionSubject_Parse_05()
        {
            // Arrange.
            const string type = "EtAlii.Servus.Roots.Time";
            //const string schema = "/[Year]/[Month]/[Day]/[Hour]/[Minute]/[Second]";
            var text = type;//$"{type}:{schema}";
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
            //Assert.NotNull(result.Schema);
            //Assert.Equal(schema.ToUpper(), result.Schema.ToString());
        }
    }
}