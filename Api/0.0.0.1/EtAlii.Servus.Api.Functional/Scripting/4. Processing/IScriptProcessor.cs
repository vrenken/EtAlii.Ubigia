namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal interface IScriptProcessor
    {
        IObservable<SequenceProcessingResult> Process(Script script);
    }
}
