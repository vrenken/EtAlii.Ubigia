namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal interface IPathValueSetter
    {
        Task<Value> Set(string valueName, string value, Structure structure, PathSubject path, SchemaExecutionScope executionScope);
    }
}
