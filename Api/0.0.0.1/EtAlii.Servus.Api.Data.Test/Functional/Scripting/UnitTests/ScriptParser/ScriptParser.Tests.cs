namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;

    [TestClass]
    public partial class ScriptParser_Tests
    {
        private IScriptParser _parser;
        
        private static readonly char[] NormalCharacters = new []
        {
            'a', 'b', 'z', '1', '2', '_',
        };

        private static readonly char[] SpecialCharacters = new[]
        {
            '+', '-',
            ' ',
//            '$',
            ':', '@',
        };

        [TestInitialize]
        public void Initialize() 
        {
            var diagnostics = ApiTestHelper.CreateDiagnostics();
            _parser = new ScriptParserFactory().Create(diagnostics);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse()
        {
            // Arrange.
            var query = "$e1 <= /First/Second";

            // Act.
            var script = _parser.Parse(query);

            // Assert.
            Assert.IsNotNull(script);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_N()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/1\n/Second/2\n/Third/3\n$var1 <= /Fourth/4\n/Fifth/5");

            // Assert.
            Assert.AreEqual(5, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_N_Invalid_Script()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("/First/1\n/Second/2\n/Third/3$var1 = /Fourth/4\n/Fifth/5");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_RN()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/1\r\n/Second/2\r\n/Third/3\r\n$var1 <= /Fourth/4\r\n/Fifth/5");

            // Assert.
            Assert.AreEqual(5, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Action()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Action_With_Query()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth");

            // Assert.
            Assert.IsInstanceOfType(script.Sequences.First().Parts.Skip(1).First(), typeof(PathSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Actions_With_Query_And_VariableAssignment()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6");

            // Assert.
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(1).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.ElementAt(0), typeof(VariableSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.ElementAt(2), typeof(PathSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Path_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("/First/Second//Third/Fourth");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_Path_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new System.Action<char>(c =>
            {
                var symbol = String.Format("ThirdIs{0}Cool", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query);
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in NormalCharacters)
            {
                act(character);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Quoted_Path_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new System.Action<char>(c =>
            {
                var symbol = String.Format("\"ThirdIs{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query);
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in NormalCharacters)
            {
                act(character);
            }
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Quoted_Path_With_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new System.Action<char>(c =>
            {
                var symbol = String.Format("\"ThirdIs{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query);
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in SpecialCharacters)
            {
                act(character);
            }
            act('/'); // Let's also check the separator charachter.
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_Path_With_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new System.Action<char>(c =>
            {
                var symbol = String.Format("ThirdIsNot{0}Cool", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query);
                var count = script.Sequences.Count();
            });

            // Assert.
            foreach (var character in SpecialCharacters)
            {
                var c = character;
                ExceptionAssert.Throws<ScriptParserException>(() => act(c));
            }
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_VariableAssignment_With_Path_Error()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth is bad/5\r\n/Sixth/6");
                var count = script.Sequences.Count();
            });


            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_VariableAssignment_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth//5\r\n/Sixth/6");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#thislineissafe");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#thislineissafe #and this line also");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_3()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe\r\n#and this line also");

            // Assert.
            Assert.AreEqual(2, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(1).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.Skip(0).First(), typeof(Comment));

        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_4()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#this line is safe\r\n/and/this/line/also");

            // Assert.
            Assert.AreEqual(2, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(0).First(), typeof(Comment));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.Skip(1).First(), typeof(PathSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_5()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe #and this comment also");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(1).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(2).First(), typeof(Comment));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_6()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe   #and this comment also");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(1).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(2).First(), typeof(Comment));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_7()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe#and this comment also");

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(1).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(2).First(), typeof(Comment));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_With_Error_1()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("thislineisbad #and this should be ok");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());

        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_With_Error_2()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("thislineisbad");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment_With_Error_1()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("#ThisLineIsOk\r\nButThisLineIsBad");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment_With_Error_2()
        {
            // Arrange.

            // Act.
            var act = new System.Action(() =>
            {
                var script = _parser.Parse("#ThisLineIsBad\r\nButThisLineIsOk");
                var count = script.Sequences.Count();
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(() => act());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#ThisLineIsOk\r\n#AndThisLineAlso");

            // Assert.
            Assert.AreEqual(2, script.Sequences.Count());
        }
    }
}