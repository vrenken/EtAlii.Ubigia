namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IPathValueGetter
    {
        Task<Value> Get(string valueName, Structure structure, PathSubject path, SchemaExecutionScope executionScope);
    }
}
