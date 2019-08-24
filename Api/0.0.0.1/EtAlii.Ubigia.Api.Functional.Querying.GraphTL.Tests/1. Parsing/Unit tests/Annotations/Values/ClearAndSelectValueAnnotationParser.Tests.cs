﻿namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class ClearAndSelectValueAnnotationParser 
    {
        [Fact]
        public void ClearAndSelectValueAnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IClearAndSelectValueAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IClearAndSelectValueAnnotationParser>();
        }
        
        [Fact]
        public void ClearAndSelectValueAnnotationParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-clear(\\LastName)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"\\LastName",valueAnnotation.Target.ToString());
        }

        [Fact]
        public void ClearAndSelectValueAnnotationParser_Parse_Value_NickName()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-clear(NickName)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Equal(@"NickName",valueAnnotation.Target.ToString());
        }

        [Fact]
        public void ClearAndSelectValueAnnotationParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@value-clear()";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var valueAnnotation = annotation as ClearAndSelectValueAnnotation;
            Assert.NotNull(valueAnnotation);
            Assert.Null(valueAnnotation.Target);
        }
    }
}
