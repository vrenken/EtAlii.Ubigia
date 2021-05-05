namespace EtAlii.Ubigia.Api.Functional.Context
{
    /// <summary>
    /// The SchemaParser is responsible of creating a querying or mutating schema for a given text.
    /// </summary>
    public interface ISchemaParser
    {
        SchemaParseResult Parse(string text);
        SchemaParseResult Parse(string[] text);
    }
}
