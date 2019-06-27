namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class AnnotationParserTests 
    {
        [Fact]
        public void AnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IAnnotationParser CreateAnnotationParser()
        {

            var scaffoldings = new IScaffolding[]
            {
                new PathSubjectParsingScaffolding(),
                new ConstantHelpersScaffolding(),
                new QueryParserScaffolding(),
            };
            
            var container = new Container();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<IAnnotationParser>();
        }
        
        [Fact]
        public void AnnotationParser_Parse_Node_Person()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node(Person:Stark/Tony)";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Node,annotation.Type);
            Assert.Equal("Person:Stark/Tony",annotation.Path.ToString());
        }
        [Fact]
        public void AnnotationParser_Parse_Nodes_Persons()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes(Person:Doe/*)";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Nodes,annotation.Type);
            Assert.Equal("Person:Doe/*",annotation.Path.ToString());
        }

        [Fact]
        public void AnnotationParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value(\\LastName)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value,annotation.Type);
            Assert.Equal(@"\\LastName",annotation.Path.ToString());
        }
        
        [Fact]
        public void AnnotationParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value()";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.Equal(AnnotationType.Value,annotation.Type);
            Assert.Null(annotation.Path);
        }
    }
}
