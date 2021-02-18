namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.Threading.Tasks;

    public static class SchemaProcessorProcessTypedExtension
    {
        public static Task<SchemaProcessingResultSingleItem<TResult>> ProcessSingle<TResult>(Schema schema)
        {
            var result = new SchemaProcessingResultSingleItem<TResult>(null, 0, null);

            return Task.FromResult(result);
        }
        public static Task<SchemaProcessingResultMultipleItems<TResult>> ProcessMultiple<TResult>(Schema schema)
        {
            var result = new SchemaProcessingResultMultipleItems<TResult>(null, 0, null);

            return Task.FromResult(result);
        }
    }
}
