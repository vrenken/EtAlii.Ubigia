//namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
//[
//    using System
//    using System.Threading.Tasks

//    public class TestableSequencePartProcessor : ISequencePartProcessor
//    [
//        private readonly Action<int, ProcessParameters<SequencePart, SequencePart>> _logMethod
//        private int step = 0

//        public TestableSequencePartProcessor(Action<int, ProcessParameters<SequencePart, SequencePart>> logMethod)
//        [
//            _logMethod = logMethod
//        ]
//        public Task<object> Process(
//            ProcessParameters<SequencePart, SequencePart> parameters,
//            ExecutionScope scope,
//            IObserver<object> output)
//        [
//            step += 1
//            _logMethod(step, parameters)
//            return Task.FromResult<object>(step)
//        ]
//    ]
//]
