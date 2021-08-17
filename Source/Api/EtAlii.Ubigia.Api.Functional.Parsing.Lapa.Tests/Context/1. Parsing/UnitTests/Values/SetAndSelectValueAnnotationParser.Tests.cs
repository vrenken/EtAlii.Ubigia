// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class SetAndSelectValueAnnotationParserTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public SetAndSelectValueAnnotationParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task SetAndSelectValueAnnotationParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISetAndSelectValueAnnotationParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task SetAndSelectValueAnnotationParser_Parse_Value_LastName_01()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISetAndSelectValueAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-set(\\LastName, 'Does2')";

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
        public async Task SetAndSelectValueAnnotationParser_Parse_Value_LastName_02()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISetAndSelectValueAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-set(\\LastName, ""Does2"")";

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
        public async Task SetAndSelectValueAnnotationParser_Parse_Value_Integer()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISetAndSelectValueAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-set(\\Weight, 42)";

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
        public async Task SetAndSelectValueAnnotationParser_Parse_Value_01()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISetAndSelectValueAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-set(//Nickname, 'Johnny')";

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
        public async Task SetAndSelectValueAnnotationParser_Parse_Value_02()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<ISetAndSelectValueAnnotationParser>(_testContext)
                .ConfigureAwait(false);
            var text = @"@node-set(//Nickname, ""Johnny"")";

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
