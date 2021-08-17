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
    public class ValueFragmentParserMutationsTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public ValueFragmentParserMutationsTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task ValueFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_Space()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "firstname <= \"John\"";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_Tab()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "firstname\t<=\t\"John\"";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_Compact()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "firstname<=\"John\"";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Space()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "Location @node-set( Location:NL/Overijssel/Enschede/Oldebokhoek/52 )";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectValueAnnotation>(annotation);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", ((AssignAndSelectValueAnnotation)annotation).Subject.ToString());
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Compact()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "Location @node-set(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectValueAnnotation>(annotation);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", ((AssignAndSelectValueAnnotation)annotation).Subject.ToString());
        }


        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Tab()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "Location\t@node-set(\tLocation:NL/Overijssel/Enschede/Oldebokhoek/52\t)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectValueAnnotation>(annotation);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", ((AssignAndSelectValueAnnotation)annotation).Subject.ToString());
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Space()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "FirstName @node-set(\"John\")";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectValueAnnotation>(annotation);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Source);
            var subject = Assert.IsType<StringConstantSubject>(((AssignAndSelectValueAnnotation)annotation).Subject);
            Assert.Equal("John", subject.Value);
        }

        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Tab()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "FirstName @node-set(\t\"John\"\t)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectValueAnnotation>(annotation);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Source);
            var subject = Assert.IsType<StringConstantSubject>(((AssignAndSelectValueAnnotation)annotation).Subject);
            Assert.Equal("John", subject.Value);
        }
        [Fact]
        public async Task ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Compact()
        {
            // Arrange.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IValueFragmentParser>(_testContext)
                .ConfigureAwait(false);
            var text = "FirstName @node-set(\"John\")";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectValueAnnotation>(annotation);
            var subject = Assert.IsType<StringConstantSubject>(((AssignAndSelectValueAnnotation)annotation).Subject);
            Assert.Equal("John", subject.Value);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Source);
        }
    }
}
