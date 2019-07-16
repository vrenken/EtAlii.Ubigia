namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    internal interface IQueryProcessor
    {
        Task<QueryProcessingResult> Process(Query query);
    }
}
