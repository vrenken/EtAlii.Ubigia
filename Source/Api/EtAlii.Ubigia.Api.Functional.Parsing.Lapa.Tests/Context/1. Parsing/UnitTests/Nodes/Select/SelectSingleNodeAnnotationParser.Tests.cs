﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
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
            var container = new LapaSchemaParserTestContainerFactory().Create();

            return container.GetInstance<ISelectSingleNodeAnnotationParser>();
        }

        [Fact]
        public void SelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node(person:Doe/John)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/John", nodeAnnotation.Source.ToString());
        }
        [Fact]
        public void SelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node(person:Doe/John )";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/John", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public void SelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = CreateAnnotationParser();
            var text = @"@node( person:Doe/John)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as SelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("person:Doe/John", nodeAnnotation.Source.ToString());
        }

    }
}
