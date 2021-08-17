// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

//namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
//[
//    using System.Collections.Generic
//    using System.Linq
//    using System.Reactive.Linq
//    using System.Threading.Tasks
//    using EtAlii.Ubigia.Tests
//

//
//    public class SequenceProcessor_Tests
//    [
//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SequenceProcessor_Constant_To_Variable_Assignment()
//        [
//            // Arrange.
//            var diagnostics = TestDiagnostics.Create()
//            var processedParameters = new Dictionary<int, ProcessParameters<SequencePart, SequencePart>>()
//            var sequencePartProcessor = new TestableSequencePartProcessor((i, p) => processedParameters[i] = p)
//            var scope = new ExecutionScope()

//            var options = new ScriptProcessorOptions()
//                .Use(sequencePartProcessor)
//                .Use(diagnostics)
//                .Use(scope)
//            var processor = new TestScriptProcessorFactory().Create(options)

//            var sequenceParts = new SequencePart[]
//            [
//                new VariableSubject("first"),
//                new AssignOperator(),
//                new StringConstantSubject("second"),

//            ]
//            var script = Create(sequenceParts)

//            // Act.
//            var lastSequence = await processor.Process(script)
//            await lastSequence.Output.LastOrDefaultAsync()

//            // Assert.
//            Assert.IsType(processedParameters[1].Target, typeof(StringConstantSubject))
//            Assert.Equal("second", ((StringConstantSubject)processedParameters[1].Target).Value)
//            Assert.IsType(processedParameters[2].Target, typeof(VariableSubject))
//            Assert.Equal("first", ((VariableSubject)processedParameters[2].Target).Name)
//            Assert.IsType(processedParameters[3].Target, typeof(AssignOperator))
//        ]
//        [Fact, Trait("Category", TestAssembly.Category)]
//        public async Task SequenceProcessor_Path_Addition_To_Path_Into_Variable()
//        [
//            // Arrange.
//            var diagnostics = TestDiagnostics.Create()
//            var processedParameters = new Dictionary<int, ProcessParameters<SequencePart, SequencePart>>()
//            var sequencePartProcessor = new TestableSequencePartProcessor((i, p) => processedParameters[i] = p)
//            var scope = new ExecutionScope()
//            var options = new ScriptProcessorOptions()
//                .Use(sequencePartProcessor)
//                .Use(diagnostics)
//                .Use(scope)
//            var processor = new TestScriptProcessorFactory().Create(options)

//            var sequenceParts = new SequencePart[]
//            [
//                new VariableSubject("first"),
//                new AssignOperator(),
//                new PathSubject(new ConstantPathSubjectPart("second")),
//                new AddOperator(),
//                new StringConstantSubject("third"),
//            ]
//            var script = Create(sequenceParts)

//            // Act.
//            await processor.Process(script)

//            // Assert.
//            Assert.IsType(processedParameters[1].Target, typeof(StringConstantSubject))
//            Assert.Equal("third", ((StringConstantSubject)processedParameters[1].Target).Value)

//            Assert.IsType(processedParameters[2].Target, typeof(PathSubject))
//            Assert.Equal("second", ((PathSubject)processedParameters[2].Target).Parts.Cast<ConstantPathSubjectPart>().First().Name)

//            Assert.IsType(processedParameters[3].Target, typeof(AddOperator))

//            Assert.IsType(processedParameters[4].Target, typeof(VariableSubject))
//            Assert.Equal("first", ((VariableSubject)processedParameters[4].Target).Name)

//            Assert.IsType(processedParameters[5].Target, typeof(AssignOperator))
//        ]
//        private Script Create(SequencePart[] sequenceParts)
//        [
//            var sequence = new Sequence(sequenceParts)
//            var script = new Script(sequence)
//            return script
//        ]
//    ]
//]
