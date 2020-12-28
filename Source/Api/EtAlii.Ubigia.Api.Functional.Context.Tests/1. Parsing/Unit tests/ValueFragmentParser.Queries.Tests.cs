﻿namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class ValueFragmentParserQueriesTests
    {
        [Fact]
        public void ValueFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateValueFragmentParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private INodeValueFragmentParser CreateValueFragmentParser() => new SchemaParserTestContainerFactory().Create().GetInstance<INodeValueFragmentParser>();

        [Fact]
        public void ValueFragmentParser_Parse_Query_Without_Annotation()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = @"firstname";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Query_With_Value_Annotation()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = @"firstname @node()";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<SelectNodeValueAnnotation>(annotation);
            Assert.Null(annotation.Source);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Query_With_Relative_Value_Annotation()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = @"lastname @node(\\)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Query, valueFragment.Type);

            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<SelectNodeValueAnnotation>(annotation);
            Assert.Equal(@"\\", annotation.Source.ToString());
        }
    }
}