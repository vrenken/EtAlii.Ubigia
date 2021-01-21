namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal interface IValueGetter
    {
        Task<Value> Get(string valueName, ValueAnnotation annotation, SchemaExecutionScope executionScope, Structure structure);
    }
}
