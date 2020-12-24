namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using Xunit;

    public class DateTimeValueParserTests : IDisposable
    {
        private IDateTimeValueParser _parser;

        public DateTimeValueParserTests()
        {
            var nodeValidator = new NodeValidator();
            _parser = new DateTimeValueParser(nodeValidator);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void DateTimeValueParser_Parse_01()
        {
            // Arrange.
            const string text = "2015-07-28 23:01";
            DateTime? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new DateTime(2015, 07, 28, 23, 01, 0), result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void DateTimeValueParser_Parse_02()
        {
            // Arrange.
            const string text = "2015-07-28";
            DateTime? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.True(result.HasValue);
            Assert.Equal(new DateTime(2015, 07, 28), result);
        }
    }
}
