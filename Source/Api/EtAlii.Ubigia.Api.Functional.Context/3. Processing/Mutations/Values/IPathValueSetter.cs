namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IPathValueSetter
    {
        Task<Value> Set(string valueName, string value, Structure structure, PathSubject path, SchemaExecutionScope executionScope);
    }
}
