namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    internal interface IPathValueSetter
    {
        Task<Value> Set(string valueName, string value, Structure structure, PathSubject path, SchemaExecutionScope executionScope);
    }
}
