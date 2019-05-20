namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IQueryParser
    {
        QueryParseResult Parse(string text);
        QueryParseResult Parse(string[] text);
    }
}
