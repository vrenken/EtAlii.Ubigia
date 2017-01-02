namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Tests;
    
    using Moppet.Lapa;
    using Moppet.Lapa.Parsers;
    using Xunit;

    
    public partial class DateTimeValueParser_Tests : IDisposable
    {
        private IDateTimeValueParser _parser;

        public DateTimeValueParser_Tests()
        {
            var nodeValidator = new NodeValidator();
            _parser = new DateTimeValueParser(nodeValidator);
        }

        public void Dispose()
        {
            _parser = null;
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