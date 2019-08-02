namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    internal interface IPathValueGetter
    {
        Task<Value> Get(string valueName, Structure structure, PathSubject path, SchemaExecutionScope executionScope);
    }
}
