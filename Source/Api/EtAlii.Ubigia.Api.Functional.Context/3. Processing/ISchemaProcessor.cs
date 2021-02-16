namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    public interface ISchemaProcessor
    {
        Task<SchemaProcessingResult> Process(Schema schema);
    }
}
