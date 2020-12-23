namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    public interface IScriptProcessor
    {
        IObservable<SequenceProcessingResult> Process(Script script);
    }
}
