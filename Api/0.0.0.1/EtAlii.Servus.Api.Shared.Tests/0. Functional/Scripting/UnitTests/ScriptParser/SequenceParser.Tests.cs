// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class SequenceParser_Tests
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
        public void SequenceParser_Parse_Constants_01()
        {
            // Arrange.
            var text = "'First' <= 'Second' <= 'Third'";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_02()
        {
            // Arrange.
            var text = @"""First"" <= ""Second"" <= ""Third""";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);

        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_03()
        {
            // Arrange.
            var text = @"""First"" <= 'Second' <= ""Third""";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);

        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_01()
        {
            // Arrange.
            var text = @"$first <= ""Second""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(VariableSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(StringConstantSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_02()
        {
            // Arrange.
            var text = @"/'Second' <= $first <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(VariableSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(4), typeof(StringConstantSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_03_Invalid()
        {
            // Arrange.
            var text = @"""Second"" <= $first";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_03()
        {
            // Arrange.
            var text = @"/""Second"" <= $first";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(VariableSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_04_Invalid()
        {
            // Arrange.
            var text = @"'Second' <= $first <= ""Third""";

            // Act.
            var act = new Action(() =>
            {
                _parser.Parse(text);
            });

            // Assert.
            ExceptionAssert.Throws<ScriptParserException>(act);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_04()
        {
            // Arrange.
            var text = @"/'Second' <= $first <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(PathSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(VariableSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(4), typeof(StringConstantSubject));
        }
    }
}