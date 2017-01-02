
namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class IntegerValueParser_Tests
    {
        private IIntegerValueParser _parser;
        
        [TestInitialize]
        public void Initialize() 
        {
            var nodeValidator = new NodeValidator();

            _parser = new IntegerValueParser(nodeValidator);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_01()
        {
            // Arrange.
            const string text = "123456";
            int? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(123456, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_02()
        {
            // Arrange.
            const string text = "-123456";
            int? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(-123456, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_03()
        {
            // Arrange.
            const string text = "0";
            int? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_04()
        {
            // Arrange.
            const string text = "00";
            int? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_05()
        {
            // Arrange.
            const string text = "000";
            int? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_06()
        {
            // Arrange.
            const string text = "-0";
            int? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_07()
        {
            // Arrange.
            const string text = "a0";

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
        public void IntegerValueParser_Parse_08()
        {
            // Arrange.
            const string text = ".0";

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
        public void IntegerValueParser_Parse_09()
        {
            // Arrange.
            const string text = "0a";
            int? result = 0;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
            Assert.AreEqual("a", node.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_10()
        {
            // Arrange.
            const string text = "a0";

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
        public void IntegerValueParser_Parse_11()
        {
            // Arrange.
            const string text = "0.";
            int? result = null;
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
            Assert.AreEqual(".", node.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_12()
        {
            // Arrange.
            const string text = "+123456";
            int? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(+123456, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_13()
        {
            // Arrange.
            const string text = "123-456";
            int? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(123, result);
            Assert.AreEqual("-456", node.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_14()
        {
            // Arrange.
            const string text = "123+456";
            int? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(123, result);
            Assert.AreEqual("+456", node.Rest.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IntegerValueParser_Parse_15()
        {
            // Arrange.
            const string text = "123a456";
            int? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(123, result);
            Assert.AreEqual("a456", node.Rest.ToString());
        }
    }
}