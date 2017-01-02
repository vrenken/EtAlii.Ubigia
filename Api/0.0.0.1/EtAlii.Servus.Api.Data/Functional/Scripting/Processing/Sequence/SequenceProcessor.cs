namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using System.Collections.Generic;
    using System.Linq;

    internal class SequenceProcessor
    {
        private readonly ISequencePartProcessor _sequencePartProcessor;

        internal SequenceProcessor(ISequencePartProcessor sequencePartProcessor)
        {
            _sequencePartProcessor = sequencePartProcessor;
        }

        internal void Process(Sequence sequence)
        {
            var parts = sequence.Parts.Reverse().ToArray();
            
            object rightResult = null;
            object result = null;
            // We process the sequence parts in reversed order.
            for (int i = 0; i < parts.Length; i++)
            {
                var leftPart = i < parts.Length - 1 ? parts[i + 1] : null;
                var futurePart = i < parts.Length - 2 ? parts[i + 2] : null;
                var rightPart = i > 0 ? parts[i - 1] : null;
                var part = parts[i];
                rightResult = result;

                if (part is Operator)
                {
                    result = ProcessOperator(parts, rightResult, result, i, futurePart, leftPart, rightPart, part);
                }
                else if (part is Subject)
                {
                    result = ProcessSubject(rightResult, result, futurePart, leftPart, rightPart, part);
                }
            }
        }

        private object ProcessSubject(object rightResult, object result, SequencePart futurePart, SequencePart leftPart, SequencePart rightPart, SequencePart part)
        {
            if (rightPart is Operator)
            {
                // The right part has already been executed. 
                // Thus the result variable contains the most recent processing output.
                // result = result; 
            }
            else
            {
                var parameters = new ProcessParameters<SequencePart, SequencePart>(part)
                {
                    FuturePart = futurePart,
                    LeftPart = leftPart,
                    RightPart = rightPart,
                    RightResult = rightResult,
                };
                result = _sequencePartProcessor.Process(parameters);
            }
            return result;
        }

        private object ProcessOperator(SequencePart[] parts, object rightResult, object result, int i, SequencePart futurePart, SequencePart leftPart, SequencePart rightPart, SequencePart part)
        {
            object leftResult = null;
            ProcessParameters<SequencePart, SequencePart> parameters;

            // We have a operator. Let's first process the left part (if any).
            if (leftPart != null)
            {
                var leftPart2 = i < parts.Length - 2 ? parts[i + 2] : null;
                var futurePart2 = i < parts.Length - 3 ? parts[i + 3] : null;
                parameters = new ProcessParameters<SequencePart, SequencePart>(leftPart)
                {
                    FuturePart = futurePart2,
                    LeftPart = leftPart2,
                    RightPart = part,
                    RightResult = rightResult,
                };
                leftResult = _sequencePartProcessor.Process(parameters);
            }
            // Ok, now process the operator itself.
            parameters = new ProcessParameters<SequencePart, SequencePart>(part)
            {
                FuturePart = futurePart,
                LeftPart = leftPart,
                LeftResult = leftResult,
                RightPart = rightPart,
                RightResult = rightResult,
            };
            result = _sequencePartProcessor.Process(parameters);
            return result;
        }
    }
}
