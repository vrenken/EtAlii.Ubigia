// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class AddAndSelectSingleNodeAnnotationParserTests : IClassFixture<FunctionalUnitTestContext>
    {
        private readonly FunctionalUnitTestContext _testContext;

        public AddAndSelectSingleNodeAnnotationParserTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_01()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/, Potsdam)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_02()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_03()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_04()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/, ""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_05()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_06()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/, 'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam",nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }

        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_07()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/,'Potsdam')";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam", nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }


        [Fact]
        public async Task AddAndSelectSingleNodeAnnotationParser_Parse_08()
        {
            // Arrange.
            var parser = await _testContext
                .CreateComponentOnNewSpace<IAddAndSelectSingleNodeAnnotationParser>()
                .ConfigureAwait(false);
            var text = @"@node-add(location:DE/Berlin/,""Potsdam"")";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.Empty(node.Rest);
            var nodeAnnotation = annotation as AddAndSelectSingleNodeAnnotation;
            Assert.NotNull(nodeAnnotation);
            Assert.Equal("Potsdam", nodeAnnotation.Identity.Name);
            Assert.False(nodeAnnotation.Identity.IsVariable);
            Assert.Equal("location:DE/Berlin/", nodeAnnotation.Source.ToString());
        }
    }
}
