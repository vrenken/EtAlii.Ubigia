namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System;
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.Servus.Api.Data.Model;
    using System.Collections.Generic;

    [TestClass]
    public partial class SequenceParser_Tests
    {
        private SequenceParser _parser;
        
        [TestInitialize]
        public void Initialize()
        {
            var container = new Container();
            // Sequence
            container.Register<IEnumerable<ISequencePartParser>>(() => ScriptParserFactory.GetSequencePartParser(container), Lifestyle.Singleton);
            // Operators
            container.Register<IEnumerable<IOperatorParser>>(() => ScriptParserFactory.GetOperatorParsers(container), Lifestyle.Singleton);
            // Subjects.
            container.Register<IEnumerable<ISubjectParser>>(() => ScriptParserFactory.GetSubjectParsers(container), Lifestyle.Singleton);
            container.Register<IEnumerable<IConstantSubjectParser>>(() => ScriptParserFactory.GetConstantSubjectParsers(container), Lifestyle.Singleton);
            container.Register<IEnumerable<IPathSubjectPartParser>>(() => ScriptParserFactory.GetPathSubjectParsers(container), Lifestyle.Singleton);
            // Helpers
            container.Register<IParserHelper, ParserHelper>(Lifestyle.Singleton);
            container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>(Lifestyle.Singleton);
            container.Register<IConstantHelper, ConstantHelper>(Lifestyle.Singleton);
            _parser = container.GetInstance<SequenceParser>();

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
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_02()
        {
            // Arrange.
            var text = @"""First"" <= ""Second"" <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Constants_03()
        {
            // Arrange.
            var text = @"""First"" <= 'Second' <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
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
            var text = @"$first <= 'Second' <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(VariableSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(StringConstantSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(4), typeof(StringConstantSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_03()
        {
            // Arrange.
            var text = @"""Second"" <= $first";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 3);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(StringConstantSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(VariableSubject));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_Variable_04()
        {
            // Arrange.
            var text = @"'Second' <= $first <= ""Third""";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 5);
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(0), typeof(StringConstantSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(1), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(2), typeof(VariableSubject));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(3), typeof(AssignOperator));
            Assert.IsInstanceOfType(sequence.Parts.ElementAt(4), typeof(StringConstantSubject));
        }
    }
}