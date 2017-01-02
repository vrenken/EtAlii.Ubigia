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
    public partial class SequenceParser_Paths_Tests
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
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Absolute_02()
        {
            // Arrange.
            var text = "/First/Second/Third/";

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
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
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
    }
}