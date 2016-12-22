namespace EtAlii.Servus.Api.Logical
{
    public interface IGraphPathBuilder
    {
        IGraphPathBuilder Add(Identifier startIdentifier);
        IGraphPathBuilder Add(string nodeName);
        IGraphPathBuilder Add(GraphRelation relation);
        GraphPath ToPath();
    }
}