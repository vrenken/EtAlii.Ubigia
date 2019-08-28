﻿namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class SelectSingleNodeAnnotationParserTests 
    {
        [Fact]
        public void SelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private ISelectSingleNodeAnnotationParser CreateAnnotationParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<ISelectSingleNodeAnnotationParser>();
        }
        
        [Fact]
        public void SelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node(XXXXXXXX)";
            
            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            
            throw new System.InvalidOperationException();
        }
    }
}
