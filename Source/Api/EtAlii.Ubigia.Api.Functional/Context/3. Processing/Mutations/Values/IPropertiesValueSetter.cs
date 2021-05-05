namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal interface IPropertiesValueSetter
    {
        Task<Value> Set(string valueName, Structure structure, object value, SchemaExecutionScope executionScope);
    }
}
