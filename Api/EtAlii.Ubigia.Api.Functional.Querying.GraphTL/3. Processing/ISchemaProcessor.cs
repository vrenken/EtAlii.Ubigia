namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Threading.Tasks;

    internal interface ISchemaProcessor
    {
        Task<SchemaProcessingResult> Process(Schema schema);
    }
}
