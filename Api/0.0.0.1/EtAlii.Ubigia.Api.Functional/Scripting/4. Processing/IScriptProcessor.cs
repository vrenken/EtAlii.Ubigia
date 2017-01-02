namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal interface IScriptProcessor
    {
        IObservable<SequenceProcessingResult> Process(Script script);
    }
}
