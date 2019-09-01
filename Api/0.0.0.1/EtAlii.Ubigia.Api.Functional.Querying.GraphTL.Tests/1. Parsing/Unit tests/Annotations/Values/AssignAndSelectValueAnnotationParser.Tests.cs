namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Xunit;

    public class AssignAndSelectValueAnnotationParserTests 
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
        public void AssignAndSelectValueAnnotationParser_Parse_Value_LastName_01()
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
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
            Assert.Equal("Does2", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }

                
        [Fact]
        public void AssignAndSelectValueAnnotationParser_Parse_Value_LastName_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-assign(\\LastName, ""Does2"")";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Source.ToString());
            Assert.Equal("Does2", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }

                
        [Fact]
        public void AssignAndSelectValueAnnotationParser_Parse_Value_Integer()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-assign(\\Weight, 42)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\Weight",valueAnnotation.Source.ToString());
            Assert.Equal("42", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }

        [Fact]
        public void AssignAndSelectValueAnnotationParser_Parse_Value_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-assign(//Nickname, 'Johnny')";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal("//Nickname", valueAnnotation.Source.ToString());
            Assert.Equal("Johnny", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }

        [Fact]
        public void AssignAndSelectValueAnnotationParser_Parse_Value_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-assign(//Nickname, ""Johnny"")";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as AssignAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal("//Nickname", valueAnnotation.Source.ToString());
            Assert.Equal("Johnny", ((StringConstantSubject)valueAnnotation.Subject).Value);
        }
    }
}
