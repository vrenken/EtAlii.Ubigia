﻿namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class SelectMultipleNodesAnnotationParserTests 
    {
        [Fact]
        public void SelectMultipleNodesAnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private ISelectMultipleNodesAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<ISelectMultipleNodesAnnotationParser>();
        }
        
        [Fact]
        public void SelectMultipleNodesAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes(XXXXXXXX)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            
            throw new System.InvalidOperationException();
        }
    }
}
