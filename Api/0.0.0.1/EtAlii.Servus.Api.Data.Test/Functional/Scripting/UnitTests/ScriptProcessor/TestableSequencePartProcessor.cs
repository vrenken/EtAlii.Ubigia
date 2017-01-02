namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    public class TestableSequencePartProcessor : ISequencePartProcessor
    {
        private readonly Action<int, ProcessParameters<SequencePart, SequencePart>> _logMethod;
        private int step = 0;

        public TestableSequencePartProcessor(Action<int, ProcessParameters<SequencePart, SequencePart>> logMethod)
        {
            _logMethod = logMethod;
        }

        public object Process(ProcessParameters<SequencePart, SequencePart> parameters)
        {
            step += 1;
            _logMethod(step, parameters);
            return step;
        }
    }
}