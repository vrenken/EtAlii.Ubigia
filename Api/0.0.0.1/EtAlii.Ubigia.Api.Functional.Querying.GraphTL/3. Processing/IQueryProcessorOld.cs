namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal interface IQueryProcessorOld
    {
        IObservable<QueryProcessingResult> Process(Query query);
    }
}