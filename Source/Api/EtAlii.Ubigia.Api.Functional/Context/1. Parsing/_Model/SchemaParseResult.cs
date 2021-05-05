namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class SchemaParseResult
    {
        public string Source { get; }

        public Schema Schema { get; }

        public SchemaParserError[] Errors { get; }

        public SchemaParseResult(string source, Schema schema, SchemaParserError[] errors)
        {
            Source = source;
            Schema = schema;
            Errors = errors;
        }
    }
}
