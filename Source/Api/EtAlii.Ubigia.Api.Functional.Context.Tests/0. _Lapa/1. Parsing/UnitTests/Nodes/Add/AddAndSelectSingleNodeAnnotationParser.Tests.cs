namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class AddAndSelectSingleNodeAnnotationParserTests
    {
        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateAnnotationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IAddAndSelectSingleNodeAnnotationParser CreateAnnotationParser()
        {
            var container = new LapaSchemaParserTestContainerFactory().Create();

            return container.GetInstance<IAddAndSelectSingleNodeAnnotationParser>();
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/, Potsdam)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_05()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_06()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_07()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/,'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }


        [Fact]
        public void AddAndSelectSingleNodeAnnotationParser_Parse_08()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node-add(location:DE/Berlin/,""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Name);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }
    }
}
