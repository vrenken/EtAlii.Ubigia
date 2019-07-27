namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    internal interface IValueSetter
    {
        Task<Value> Set(string valueName, object value, Annotation annotation, QueryExecutionScope executionScope, Structure structure);
    }
}
