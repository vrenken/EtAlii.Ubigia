namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal interface IQueryProcessor
    {
        IObservable<QueryProcessingResult> Process(Query query);
    }
}
