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
    public partial class TimeSpanValueParser_Tests
    {
        private ITimeSpanValueParser _parser;
        
        [TestInitialize]
        public void Initialize() 
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            _parser = new TimeSpanValueParser(nodeValidator, nodeFinder);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_01()
        {
            // Arrange.
            const string text = "23:01";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(23, 01, 0), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_02()
        {
            // Arrange.
            const string text = "00:01";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(0, 01, 0), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_03()
        {
            // Arrange.
            const string text = "00:00";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(0, 0, 0), result);
        }

        // Only positive timespans for now.
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_04()
        {
            // Arrange.
            const string text = "-23:01";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(-23, -01, 0), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_05()
        {
            // Arrange.
            const string text = "27:68";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(27, 68, 0), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_06()
        {
            // Arrange.
            const string text = "27:68:69";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(27, 68, 69), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_07()
        {
            // Arrange.
            const string text = "27:68:69.896";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(0, 27, 68, 69, 896), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_08()
        {
            // Arrange.
            const string text = "201:27:68:69.896";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(201, 27, 68, 69, 896), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_09()
        {
            // Arrange.
            const string text = "201:27:68:69";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(201, 27, 68, 69, 0), result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeSpanValueParser_Parse_10()
        {
            // Arrange.
            const string text = "27:68:69";
            TimeSpan? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(new TimeSpan(0, 27, 68, 69, 0), result);
        }


    }
}