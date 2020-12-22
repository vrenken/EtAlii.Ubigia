namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal interface IValueSetter
    {
        Task<Value> Set(string valueName, object value, NodeValueAnnotation annotation, SchemaExecutionScope executionScope, Structure structure);
    }
}
