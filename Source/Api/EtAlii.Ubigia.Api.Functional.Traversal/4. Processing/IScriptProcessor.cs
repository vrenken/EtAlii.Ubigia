namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal interface IScriptProcessor
    {
        IObservable<SequenceProcessingResult> Process(Script script);
    }
}
