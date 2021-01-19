namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    public static class SchemaProcessorProcessTypedExtension
    {
        public static Task<SchemaProcessingResult<TResult>> Process<TResult>(Schema schema)
        {
            var result = new SchemaProcessingResult<TResult>(null, 0, null);

            return Task.FromResult(result);
        }
    }
}
