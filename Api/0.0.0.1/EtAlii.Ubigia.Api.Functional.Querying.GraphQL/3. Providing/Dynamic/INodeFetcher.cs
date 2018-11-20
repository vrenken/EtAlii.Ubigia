namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal interface INodeFetcher
    {
        Task<IEnumerable<IInternalNode>> FetchAsync(string path);
    }
}