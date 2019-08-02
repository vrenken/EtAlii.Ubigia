namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    internal interface IValueGetter
    {
        Task<Value> Get(string valueName, Annotation annotation, SchemaExecutionScope executionScope, Structure structure);
    }
}
