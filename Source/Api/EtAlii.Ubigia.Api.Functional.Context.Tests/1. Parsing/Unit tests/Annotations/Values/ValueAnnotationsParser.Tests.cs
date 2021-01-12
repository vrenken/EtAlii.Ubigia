﻿namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class ValueAnnotationsParserTests
    {
        [Fact]
        public void ValueAnnotationsParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateValueAnnotationsParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IValueAnnotationsParser CreateValueAnnotationsParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IValueAnnotationsParser>();
        }

        [Fact]
        public void ValueAnnotationsParser_Parse_Value_LastName()
        {
            // Arrange.
            var parser = CreateValueAnnotationsParser();
            var text = @"@node(\\LastName)";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectValueAnnotation>(annotation);
            Assert.NotNull(annotation.Source);
            Assert.Equal(@"\\LastName",annotation.Source.ToString());
        }

        [Fact]
        public void ValueAnnotationsParser_Parse_Value()
        {
            // Arrange.
            var parser = CreateValueAnnotationsParser();
            var text = @"@node()";

            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectValueAnnotation>(annotation);
            Assert.Null(annotation.Source);
        }
    }
}
