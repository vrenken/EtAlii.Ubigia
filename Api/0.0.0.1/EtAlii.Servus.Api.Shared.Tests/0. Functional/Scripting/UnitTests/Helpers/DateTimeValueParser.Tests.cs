namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moppet.Lapa;
    using Moppet.Lapa.Parsers;

    [TestClass]
    public partial class DateTimeValueParser_Tests
    {
        private IDateTimeValueParser _parser;
        
        [TestInitialize]
        public void Initialize() 
        {
            var nodeValidator = new NodeValidator();
            _parser = new DateTimeValueParser(nodeValidator);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new DateTime(2015, 07, 28, 23, 01, 0), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new DateTime(2015, 07, 28), result);
        }
    }
}