// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;

    public class AddAndSelectMultipleNodesAnnotationParserTests
    {
        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IAddAndSelectMultipleNodesAnnotationParser CreateAnnotationParser()
        {
            var container = new LapaSchemaParserTestContainerFactory().Create();

            return container.GetInstance<IAddAndSelectMultipleNodesAnnotationParser>();
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/, Potsdam)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_05()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_06()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_07()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/,'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }


        [Fact]
        public void AddAndSelectMultipleNodesAnnotationParser_Parse_08()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@nodes-add(location:DE/Berlin/,""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectMultipleNodesAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }
    }
}
