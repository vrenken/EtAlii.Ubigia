namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moppet.Lapa;

    [TestClass]
    public partial class BooleanValueParser_Tests
    {
        private IBooleanValueParser _parser;
        
        [TestInitialize]
        public void Initialize() 
        {
            var nodeValidator = new NodeValidator();
            var nodeFinder = new NodeFinder();
            _parser = new BooleanValueParser(nodeValidator, nodeFinder);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_01()
        {
            // Arrange.
            const string text = "true";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(true, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_02()
        {
            // Arrange.
            const string text = "TRUE";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(true, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_03()
        {
            // Arrange.
            const string text = "True";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(true, result);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_04()
        {
            // Arrange.
            const string text = "false";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(false, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_05()
        {
            // Arrange.
            const string text = "FALSE";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(false, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_06()
        {
            // Arrange.
            const string text = "False";
            bool? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(false, result);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_07()
        {
            // Arrange.
            const string text = "F alse";

            // Act.
            var act = new Action(() =>
            {
                var node = _parser.Parser.Do(text);
                if (_parser.CanParse(node))
                {
                    _parser.Parse(node);
                }
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_08()
        {
            // Arrange.
            const string text = "T rue";

            // Act.
            var act = new Action(() =>
            {
                var node = _parser.Parser.Do(text);
                if (_parser.CanParse(node))
                {
                    _parser.Parse(node);
                }
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_09()
        {
            // Arrange.
            const string text = "F1alse";

            // Act.
            var act = new Action(() =>
            {
                var node = _parser.Parser.Do(text);
                if (_parser.CanParse(node))
                {
                    _parser.Parse(node);
                }
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BooleanValueParser_Parse_10()
        {
            // Arrange.
            const string text = "T1rue";

            // Act.
            var act = new Action(() =>
            {
                var node = _parser.Parser.Do(text);
                if (_parser.CanParse(node))
                {
                    _parser.Parse(node);
                }
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }


    }
}