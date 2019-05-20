namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IQueryParserFactory
    {
        IQueryParser Create(QueryParserConfiguration configuration);
    }
}