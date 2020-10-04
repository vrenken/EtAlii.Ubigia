namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    internal interface IScriptProcessor
    {
        IObservable<SequenceProcessingResult> Process(Script script);
    }
}
