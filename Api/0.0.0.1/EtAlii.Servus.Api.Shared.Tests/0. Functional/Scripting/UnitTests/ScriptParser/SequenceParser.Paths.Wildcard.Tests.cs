// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class SequenceParser_Paths_Wildcard_Tests
    {
        private ISequenceParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            var container = new Container();

            new ConstantHelpersScaffolding().Register(container); 
            new ScriptParserScaffolding().Register(container);
            new SequenceParsingScaffolding().Register(container);
            new OperatorParsingScaffolding().Register(container);
            new SubjectParsingScaffolding(FunctionHandlersProvider.Empty).Register(container);

            _parser = container.GetInstance<ISequenceParser>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_01()
        {
            // Arrange.
            var text = "/First/Second/*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_02()
        {
            // Arrange.
            var text = "/First/*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_03()
        {
            // Arrange.
            var text = "/First/Second/Thi*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "Thi*");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_04()
        {
            // Arrange.
            var text = "/First/Sec*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "Sec*");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_05()
        {
            // Arrange.
            var text = "/First/Second/*ird";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*ird");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_06()
        {
            // Arrange.
            var text = "/First/*ond/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*ond");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_07()
        {
            // Arrange.
            var text = "/First/Second/'Thi'*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "Thi*");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_08()
        {
            // Arrange.
            var text = "/First/'Sec'*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "Sec*");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_09()
        {
            // Arrange.
            var text = "/First/Second/*'ird'";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*ird");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_10()
        {
            // Arrange.
            var text = "/First/*'ond'/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*ond");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }



        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_11()
        {
            // Arrange.
            var text = "/First/Second/\"Thi\"*";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "Thi*");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_12()
        {
            // Arrange.
            var text = "/First/\"Sec\"*/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "Sec*");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_13()
        {
            // Arrange.
            var text = "/First/Second/*\"ird\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(5).Cast<WildcardPathSubjectPart>().First().Pattern == "*ird");
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Wildcard_14()
        {
            // Arrange.
            var text = "/First/*\"ond\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsTrue(pathSubject.Parts.Skip(3).Cast<WildcardPathSubjectPart>().First().Pattern == "*ond");
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

    }
}