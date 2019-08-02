namespace EtAlii.Ubigia.Api.Functional
{
    internal interface ISchemaParser
    {
        SchemaParseResult Parse(string text);
        SchemaParseResult Parse(string[] text);
    }
}
