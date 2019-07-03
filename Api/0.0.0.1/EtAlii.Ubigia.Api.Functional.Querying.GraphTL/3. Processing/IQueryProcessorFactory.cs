namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IQueryProcessorFactory
    {
        IQueryProcessor Create(QueryProcessorConfiguration configuration);
    }
}
