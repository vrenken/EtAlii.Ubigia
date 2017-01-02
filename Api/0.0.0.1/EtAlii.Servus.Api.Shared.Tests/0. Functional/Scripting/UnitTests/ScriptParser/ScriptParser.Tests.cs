namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            '(',')',
            '.', ',',
        };

        private static readonly char[] SpecialCharacters2 = new[]
        {
            '+', '-',
            ' ',
//            '$',
            ':', '@',
            '(',')',
            '.', ',',
             'ä','ë','ö','ü','á','é','ó','ú','â','ê','ô','û'
        };

        [TestInitialize]
        public void Initialize() 
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
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
            const string query = "$e1 <= /First/Second";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_N()
        {
            // Arrange.
            const string query = "/First/1\n/Second/2\n/Third/3\n$var1 <= /Fourth/4\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(5, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_N_Invalid_Script()
        {
            // Arrange.
            const string query = "/First/1\n/Second/2\n/Third/3$var1 = /Fourth/4\n/Fifth/5";
            
            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Newline_RN()
        {
            // Arrange.
            const string query = "/First/1\r\n/Second/2\r\n/Third/3\r\n$var1 <= /Fourth/4\r\n/Fifth/5";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(5, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Action()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(1, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Action_With_Query()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.IsInstanceOfType(script.Sequences.First().Parts.Skip(1).First(), typeof(PathSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Actions_With_Query_And_VariableAssignment()
        {
            // Arrange.
            const string query = "/First/Second/Third/Fourth\r\n$var1 <= /Fourth/4\r\n/Fifth/5\r\n/Sixth/6";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
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
            var result = _parser.Parse("/First/Second//Third/Fourth");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_Path_With_Normal_Characters()
        {
            // Arrange.

            // Act.
            var act = new Action<char>(c =>
            {
                var symbol = String.Format("ThirdIs{0}Cool", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query).Script;
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
            var act = new Action<char>(c =>
            {
                var symbol = String.Format("\"ThirdIs{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query).Script;
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
            var act = new Action<char>(c =>
            {
                var symbol = String.Format("\"ThirdIs{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                var script = _parser.Parse(query).Script;
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
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = String.Format("ThirdIsNot{0}Cool", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                return _parser.Parse(query);
            });

            // Assert.
            foreach (var character in SpecialCharacters)
            {
                var result = act(character);
                Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Query_Unquoted_Path_With_Quoted_Special_Characters()
        {
            // Arrange.

            // Act.
            var act = new Func<char, ScriptParseResult>(c =>
            {
                var symbol = String.Format("\"ThirdIsNot{0}Cool\"", c);
                var query = String.Format("/First/Second/{0}/Fourth", symbol);
                return _parser.Parse(query);
            });

            // Assert.
            foreach (var character in SpecialCharacters2)
            {
                var result = act(character);
                Assert.IsFalse(result.Errors.Any(e => e.Exception is ScriptParserException));
            }
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_VariableAssignment_With_Path_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth is bad/5\r\n/Sixth/6");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_VariableAssignment_With_Separator_Error()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("/First/Second/Third/Fourth\r\n$var1 = /Fourth/4\r\n/Fifth//5\r\n/Sixth/6");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_1()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#thislineissafe").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_2()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#thislineissafe #and this line also").Script;

            // Assert.
            Assert.AreEqual(1, script.Sequences.Count());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_3()
        {
            // Arrange.
            var query = "/this/line/is/safe\r\n#and this line also";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(2, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(1).First(), typeof(PathSubject));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.Skip(0).First(), typeof(Comment));

        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_4()
        {
            // Arrange.
            const string query = "#this line is safe\r\n/and/this/line/also";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.IsNotNull(script, result.Errors.Select(e => e.Message).FirstOrDefault());
            Assert.AreEqual(2, script.Sequences.Count());
            Assert.IsInstanceOfType(script.Sequences.ElementAt(0).Parts.Skip(0).First(), typeof(Comment));
            Assert.IsInstanceOfType(script.Sequences.ElementAt(1).Parts.Skip(1).First(), typeof(PathSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_5()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("/this/line/is/safe #and this comment also").Script;

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
            var script = _parser.Parse("/this/line/is/safe   #and this comment also").Script;

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
            var script = _parser.Parse("/this/line/is/safe#and this comment also").Script;

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
            var result = _parser.Parse("thislineisbad #and this should be ok");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_Comment_With_Error_2()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("thislineisbad");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment_With_Error_1()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("#ThisLineIsOk\r\nButThisLineIsBad");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment_With_Error_2()
        {
            // Arrange.

            // Act.
            var result = _parser.Parse("#ThisLineIsBad\r\nButThisLineIsOk");

            // Assert.
            Assert.IsTrue(result.Errors.Any(e => e.Exception is ScriptParserException));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScriptParser_Parse_MultiLine_Comment()
        {
            // Arrange.

            // Act.
            var script = _parser.Parse("#ThisLineIsOk\r\n#AndThisLineAlso").Script;

            // Assert.
            Assert.AreEqual(2, script.Sequences.Count());
        }
    }
}