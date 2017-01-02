namespace EtAlii.Servus.Api.Data.UnitTests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using TestAssembly = EtAlii.Servus.Api.Data.Tests.TestAssembly;

    [TestClass]
    public class SequenceProcessor_Tests
    {

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceProcessor_Constant_To_Variable_Assignment()
        {
            // Arrange.
            var diagnostics = ApiTestHelper.CreateDiagnostics();
            var processedParameters = new Dictionary<int, ProcessParameters<SequencePart, SequencePart>>();
            var sequencePartProcessor = new TestableSequencePartProcessor((i, p) => processedParameters[i] = p);
            var scope = new ScriptScope();
            var processor = new ScriptProcessorFactory().Create(sequencePartProcessor, diagnostics);

            var sequenceParts = new SequencePart[]
            {
                new VariableSubject("first"),
                new AssignOperator(),
                new StringConstantSubject("second"),

            };
            var script = Create(sequenceParts);

            // Act.
            processor.Process(script, scope, null);

            // Assert.
            Assert.IsInstanceOfType(processedParameters[1].Target, typeof(StringConstantSubject));
            Assert.AreEqual("second", ((StringConstantSubject)processedParameters[1].Target).Value);
            Assert.IsInstanceOfType(processedParameters[2].Target, typeof(VariableSubject));
            Assert.AreEqual("first", ((VariableSubject)processedParameters[2].Target).Name);
            Assert.IsInstanceOfType(processedParameters[3].Target, typeof(AssignOperator));
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceProcessor_Path_Addition_To_Path_Into_Variable()
        {
            // Arrange.
            var diagnostics = ApiTestHelper.CreateDiagnostics();
            var processedParameters = new Dictionary<int, ProcessParameters<SequencePart, SequencePart>>();
            var sequencePartProcessor = new TestableSequencePartProcessor((i, p) => processedParameters[i] = p);
            var scope = new ScriptScope();
            var processor = new ScriptProcessorFactory().Create(sequencePartProcessor, diagnostics);

            var sequenceParts = new SequencePart[]
            {
                new VariableSubject("first"),
                new AssignOperator(),
                new PathSubject(new ConstantPathSubjectPart("second")),
                new AddOperator(),
                new StringConstantSubject("third"),
            };
            var script = Create(sequenceParts);

            // Act.
            processor.Process(script, scope, null);

            // Assert.
            Assert.IsInstanceOfType(processedParameters[1].Target, typeof(StringConstantSubject));
            Assert.AreEqual("third", ((StringConstantSubject)processedParameters[1].Target).Value);

            Assert.IsInstanceOfType(processedParameters[2].Target, typeof(PathSubject));
            Assert.AreEqual("second", ((PathSubject)processedParameters[2].Target).Parts.Cast<ConstantPathSubjectPart>().First().Name);

            Assert.IsInstanceOfType(processedParameters[3].Target, typeof(AddOperator));

            Assert.IsInstanceOfType(processedParameters[4].Target, typeof(VariableSubject));
            Assert.AreEqual("first", ((VariableSubject)processedParameters[4].Target).Name);

            Assert.IsInstanceOfType(processedParameters[5].Target, typeof(AssignOperator));
        }

        private Script Create(IEnumerable<SequencePart> sequenceParts)
        {
            var sequence = new Sequence(sequenceParts);
            var script = new Script(sequence);
            return script;
        }
    }
}