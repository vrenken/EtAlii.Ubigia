// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;

    public class RemoveAndSelectMultipleNodesAnnotationParserTests
    {
        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IRemoveAndSelectMultipleNodesAnnotationParser CreateAnnotationParser()
        {
            var container = new LapaSchemaParserTestContainerFactory().Create();

            return container.GetInstance<IRemoveAndSelectMultipleNodesAnnotationParser>();
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/, Potsdam)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_05()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_06()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_07()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/,'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }


        [Fact]
        public void RemoveAndSelectMultipleNodesAnnotationParser_Parse_08()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-remove(location:DE/Berlin/,""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as RemoveAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

    }
}
