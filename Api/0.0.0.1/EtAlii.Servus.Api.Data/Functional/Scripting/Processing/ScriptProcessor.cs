namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    internal class ScriptProcessor : IScriptProcessor
    {
        private readonly SequenceProcessor _sequenceProcessor;
        private readonly ProcessingContext _processingContext;

        public ScriptProcessor(
            SequenceProcessor sequenceProcessor,
            ProcessingContext processingContext)
        {
            _sequenceProcessor = sequenceProcessor;
            _processingContext = processingContext;
        }

        public void Process(Script script, ScriptScope scope, IDataConnection connection)
        {
            _processingContext.Setup(scope, connection);

            foreach (var sequence in script.Sequences)
            {
                _sequenceProcessor.Process(sequence);
            }
        }
    }
}
