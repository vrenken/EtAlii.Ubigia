namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="GraphXLContext"/> can be used to execute GXL queries on a Ubigia space.
    /// </summary>
    public interface IGraphXLContext
    {
        /// <summary>
        /// Parse the specified text into a GXL query.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        SchemaParseResult Parse(string text);

        // TODO: Add non-async methods.
        //void Process(Query query, IQueryScope scope)
        //void Process(Query query, IQueryScope scope, IProgress<QueryProcessingProgress> progress)
        //IEnumerable<object> Process(string text, IProgress<QueryProcessingProgress> progress)
        //IEnumerable<object> Process(string text, params object[] args)
        //IEnumerable<object> Process(string text, IProgress<QueryProcessingProgress> progress, params object[] args)

        // TODO: Rename to ProcessAsync.
        Task<SchemaProcessingResult> Process(Schema schema, ISchemaScope scope);
        //Task Process(Query query, IQueryScope scope, IProgress<QueryProcessingProgress> progress)
        //Task<IEnumerable<object>> Process(string text, IProgress<QueryProcessingProgress> progress)
        Task<SchemaProcessingResult> Process(string text, params object[] args);
        Task<SchemaProcessingResult> Process(string[] text);
        Task<SchemaProcessingResult> Process(string[] text, ISchemaScope scope);

        Task<SchemaProcessingResult> Process(string text);
        //Task<IEnumerable<object>> Process(string text, IProgress<QueryProcessingProgress> progress, params object[] args)

    }
}
