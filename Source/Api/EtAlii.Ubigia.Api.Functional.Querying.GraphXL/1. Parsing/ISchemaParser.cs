namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// The SchemaParser is responsible of creating a querying or mutating schema for a given text.
    /// </summary>
    internal interface ISchemaParser
    {
        SchemaParseResult Parse(string text);
        SchemaParseResult Parse(string[] text);
    }
}
