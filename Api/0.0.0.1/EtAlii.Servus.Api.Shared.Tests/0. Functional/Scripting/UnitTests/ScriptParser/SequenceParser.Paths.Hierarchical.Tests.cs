// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class SequenceParser_Paths_Hierarchical_Tests
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
        public void SequenceParser_Parse_PathSubject_Absolute_01()
        {
            // Arrange.
            var text = "/First";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(2, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof (ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_03()
        {
            // Arrange.
            var text = "/First/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof (ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof (ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof (ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_04()
        {
            // Arrange.
            var text = "$var <= /First/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.IsNotNull(variableSubject);
            Assert.AreEqual("var", variableSubject.Name);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof (AssignOperator));

            var pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof (ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof (ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof (IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof (ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart) pathSubject.Parts.ElementAt(1)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart) pathSubject.Parts.ElementAt(3)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart) pathSubject.Parts.ElementAt(5)).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_01()
        {
            // Arrange.
            var text = "/\"First\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(2, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_02()
        {
            // Arrange.
            var text = "/\"First\"/\"Second\"/\"Third\"/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(IsParentOfPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_03()
        {
            // Arrange.
            var text = "/\"First\"/\"Second\"/\"Third\"";

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
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_04()
        {
            // Arrange.
            var text = "$var <= /\"First\"/\"Second\"/\"Third\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.IsNotNull(variableSubject);
            Assert.AreEqual("var", variableSubject.Name);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));

            var pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_05()
        {
            // Arrange.
            var text = "$var <= /First/\"Second\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.IsNotNull(variableSubject);
            Assert.AreEqual("var", variableSubject.Name);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));

            var pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_01()
        {
            // Arrange.
            var text = "/Contacts += First/\"Second\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(5, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(0)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(2)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(4)).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_02()
        {
            // Arrange.
            var text = "/Contacts += First/'Second'/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(5, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(0)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(2)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(4)).Name);
        }




        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_03()
        {
            // Arrange.
            var text = "/Contacts += \"First\"/\"Second\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(5, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(0)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(2)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(4)).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_04()
        {
            // Arrange.
            var text = "/Contacts += 'First'/'Second'/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(5, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(0)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(2)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(4)).Name);
        }



        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_05()
        {
            // Arrange.
            var text = "/Contacts += \"First\"/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(5, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(0)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(2)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(4)).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_06()
        {
            // Arrange.
            var text = "/Contacts += 'First'/Second/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(5, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(0)).Name);
            Assert.AreEqual("Second", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(2)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(4)).Name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_07_Blank_SingleQuotes()
        {
            // Arrange.
            var text = "/Contacts += ''";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            var stringConstantSubject = sequence.Parts.ElementAt(2) as StringConstantSubject;
            Assert.IsNotNull(stringConstantSubject);
            Assert.AreEqual("", stringConstantSubject.Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Relative_Quoted_07_Blank_DoubleQuotes()
        {
            // Arrange.
            var text = "/Contacts += \"\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var pathSubject = sequence.Parts.ElementAt(0) as PathSubject;
            Assert.IsNotNull(pathSubject);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AddOperator));

            var stringConstantSubject = sequence.Parts.ElementAt(2) as StringConstantSubject;
            Assert.IsNotNull(stringConstantSubject);
            Assert.AreEqual("", stringConstantSubject.Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_01()
        {
            // Arrange.
            var text = "/\"F.irst\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(2, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_02()
        {
            // Arrange.
            var text = "/\".First\"/\"S.econd\"/\"Third.\"/";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(IsParentOfPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_03()
        {
            // Arrange.
            var text = "/\".First\"/\"S.econd\"/\"Third.\"";

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
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_04()
        {
            // Arrange.
            var text = "$var <= /\".First\"/\"S.econd\"/\"Third.\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.IsNotNull(variableSubject);
            Assert.AreEqual("var", variableSubject.Name);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));

            var pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.AreEqual(".First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.AreEqual("S.econd", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.AreEqual("Third.", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Point_05()
        {
            // Arrange.
            var text = "$var <= /First/\"S.econd\"/Third";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);

            var variableSubject = sequence.Parts.ElementAt(0) as VariableSubject;
            Assert.IsNotNull(variableSubject);
            Assert.AreEqual("var", variableSubject.Name);

            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));

            var pathSubject = sequence.Parts.ElementAt(2) as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.AreEqual("First", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(1)).Name);
            Assert.AreEqual("S.econd", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(3)).Name);
            Assert.AreEqual("Third", ((ConstantPathSubjectPart)pathSubject.Parts.ElementAt(5)).Name);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_01()
        {
            // Arrange.
            var text = "/\"\"";

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_02()
        {
            // Arrange.
            var text = "/\"\"/\"Second\"/\"\"/";

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_03()
        {
            // Arrange.
            var text = "/\"First\"/\"\"/\"\"";

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Quoted_With_Empty_Path_04()
        {
            // Arrange.
            var text = "$var <= /\"\"/\"\"/\"\"";

            // Act.
            var act = new Action(() =>
            {
                var sequence = _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent()
        {
            // Arrange.
            var text = "/Contacts/Does/John\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Single(), typeof(PathSubject));
            var path = sequence.Parts.Skip(1).Cast<PathSubject>().Single();

            Assert.AreEqual(7, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts.Skip(6).Single(), typeof(IsChildOfPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent_With_Condition_bool()
        {
            // Arrange.
            var text = "/Contacts/Does/.IsMale=true\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Single(), typeof(PathSubject));
            var path = sequence.Parts.Skip(1).Cast<PathSubject>().Single();

            Assert.AreEqual(7, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts.Skip(6).Single(), typeof(IsChildOfPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent_With_Condition_DateTime()
        {
            // Arrange.
            var text = "/Contacts/Does/.Birthdate=1980-03-22\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Single(), typeof(PathSubject));
            var path = sequence.Parts.Skip(1).Cast<PathSubject>().Single();

            Assert.AreEqual(7, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts.Skip(6).Single(), typeof(IsChildOfPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_Parent_Child_Parent_With_Condition_Float()
        {
            // Arrange.
            var text = "/Contacts/Does/.Weight=76.23\\";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);
            Assert.IsInstanceOfType(sequence.Parts.Skip(1).Single(), typeof(PathSubject));
            var path = sequence.Parts.Skip(1).Cast<PathSubject>().Single();

            Assert.AreEqual(7, path.Parts.Length);
            Assert.IsInstanceOfType(path.Parts.Skip(6).Single(), typeof(IsChildOfPathSubjectPart));
        }

    }
}