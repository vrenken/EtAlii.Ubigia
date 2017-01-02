//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using System.Threading.Tasks;

//    internal class SequenceProcessor2 : ISequenceProcessor2
//    {
//        private readonly ISequencePartProcessor2 _sequencePartProcessor;

//        internal SequenceProcessor2(ISequencePartProcessor2 sequencePartProcessor)
//        {
//            _sequencePartProcessor = sequencePartProcessor;
//        }

//        public async Task Process(
//            Sequence sequence,
//            IObserver<object> output)
//        {
//            // TODO: We cannot use the executionscope for the whole sequence (yet). 
//            //var executionScope = new ExecutionScope();

//            var parts = sequence.Parts.Reverse().ToArray();
            
//            object rightResult = null;
//            object result = null;
//            // We process the sequence parts in reversed order.
//            for (int i = 0; i < parts.Length; i++)
//            {
//                var leftPart = i < parts.Length - 1 ? parts[i + 1] : null;
//                var futurePart = i < parts.Length - 2 ? parts[i + 2] : null;
//                var rightPart = i > 0 ? parts[i - 1] : null;
//                var part = parts[i];
//                rightResult = result;

//                if (part is Operator)
//                {
//                    result = await ProcessOperator(parts, rightResult, i, futurePart, leftPart, rightPart, part, output);
//                }
//                else if (part is Subject)
//                {
//                    result = await ProcessSubject(rightResult, result, futurePart, leftPart, rightPart, part, output);
//                }
//            }
//        }

//        private async Task<object> ProcessSubject(
//            object rightResult, 
//            object result, 
//            SequencePart futurePart, 
//            SequencePart leftPart, 
//            SequencePart rightPart, 
//            SequencePart part,
//            IObserver<object> output)
//        {
//            if (rightPart is Operator && (part is FunctionSubject) == false)
//            {
//                // The right part has already been executed. 
//                // Thus the result variable contains the most recent processing output.
//                // result = result; 
//            }
//            else
//            {
//                var parameters = new ProcessParameters<SequencePart, SequencePart>(part)
//                {
//                    FuturePart = futurePart,
//                    LeftPart = leftPart,
//                    RightPart = rightPart,
//                    RightResult = rightResult,
//                };

//                var executionScope = new ExecutionScope(true);
//                result = await _sequencePartProcessor.Process(parameters, executionScope, output);
//            }
//            return result;
//        }

//        private async Task<object> ProcessOperator(
//            SequencePart[] parts, 
//            object rightResult, 
//            int i, 
//            SequencePart futurePart, 
//            SequencePart leftPart, 
//            SequencePart rightPart, 
//            SequencePart part,
//            IObserver<object> output)
//        {
//            ExecutionScope executionScope;

//            object leftResult = null;
//            ProcessParameters<SequencePart, SequencePart> parameters;

//            // We have a operator. Let's first process the left part (if any).
//            if (leftPart != null && (leftPart is FunctionSubject) == false)
//            {
//                var leftPart2 = i < parts.Length - 2 ? parts[i + 2] : null;
//                var futurePart2 = i < parts.Length - 3 ? parts[i + 3] : null;
//                parameters = new ProcessParameters<SequencePart, SequencePart>(leftPart)
//                {
//                    FuturePart = futurePart2,
//                    LeftPart = leftPart2,
//                    RightPart = part,
//                    RightResult = rightResult,
//                };
//                executionScope = new ExecutionScope(true);
//                leftResult = await _sequencePartProcessor.Process(parameters, executionScope, output);
//            }
//            // Ok, now process the operator itself.
//            parameters = new ProcessParameters<SequencePart, SequencePart>(part)
//            {
//                FuturePart = futurePart,
//                LeftPart = leftPart,
//                LeftResult = leftResult,
//                RightPart = rightPart,
//                RightResult = rightResult,
//            };
//            executionScope = new ExecutionScope(false);
//            var result = await _sequencePartProcessor.Process(parameters, executionScope, output);
//            return result;
//        }
//    }
//}
