namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IPathParserFactory
    {
        IPathParser Create(TraversalParserConfiguration configuration);
    }
}
