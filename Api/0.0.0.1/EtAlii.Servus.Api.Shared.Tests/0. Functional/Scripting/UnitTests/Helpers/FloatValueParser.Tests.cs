namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class FloatValueParser_Tests
    {
        private IFloatValueParser _parser;
        
        [TestInitialize]
        public void Initialize() 
        {
            var nodeValidator = new NodeValidator();
            _parser = new FloatValueParser(nodeValidator);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_01()
        {
            // Arrange.
            const string text = "123.456";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(123.456f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_02()
        {
            // Arrange.
            const string text = "-123.456";
            float? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(-123.456f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_03()
        {
            // Arrange.
            const string text = "0.0";
            float? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0.0f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_04()
        {
            // Arrange.
            const string text = "0.00";
            float? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0.0f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_05()
        {
            // Arrange.
            const string text = "00.0";
            float? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0.0f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_06()
        {
            // Arrange.
            const string text = "-0.0";
            float? result = null;
            
            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0.0f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_07()
        {
            // Arrange.
            const string text = "a.0";

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
        public void FloatValueParser_Parse_08()
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
        public void FloatValueParser_Parse_09()
        {
            // Arrange.
            const string text = "0.a";

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
        public void FloatValueParser_Parse_10()
        {
            // Arrange.
            const string text = "a.0";

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
        public void FloatValueParser_Parse_11()
        {
            // Arrange.
            const string text = "0.";

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
        public void FloatValueParser_Parse_12()
        {
            // Arrange.
            const string text = "+123.456";
            float? result = null;

            // Act.
            var node = _parser.Parser.Do(text);
            if (_parser.CanParse(node))
            {
                result = _parser.Parse(node);
            }

            // Assert.
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(+123.456f, result);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FloatValueParser_Parse_13()
        {
            // Arrange.
            const string text = "123-456";

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
        public void FloatValueParser_Parse_14()
        {
            // Arrange.
            const string text = "123+456";

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
        public void FloatValueParser_Parse_15()
        {
            // Arrange.
            const string text = "123a456";

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