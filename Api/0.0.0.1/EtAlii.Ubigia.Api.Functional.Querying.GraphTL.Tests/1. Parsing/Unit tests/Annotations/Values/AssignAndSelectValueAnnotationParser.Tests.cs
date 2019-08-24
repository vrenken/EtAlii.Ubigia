namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Xunit;

    public class AssignAndSelectValueAnnotationParser 
    {
        [Fact]
        public void AssignAndSelectValueAnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IAssignAndSelectValueAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IAssignAndSelectValueAnnotationParser>();
        }
        
        [Fact]
        public void AssignAndSelectValueAnnotationParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-assign(\\LastName, 'Does2')";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Target.ToString());
            Assert.Equal("Does2", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }
        
        [Fact]
        public void AssignAndSelectValueAnnotationParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-assign('John2')";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Null(valueAnnotation.Target);
            Assert.Equal("John2", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }
    }
}
