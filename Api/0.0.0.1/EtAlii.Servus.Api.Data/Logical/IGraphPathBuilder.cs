namespace EtAlii.Servus.Api.Data
{
    public interface IGraphPathBuilder
    {
        IGraphPathBuilder Add(string nodeName);
        IGraphPathBuilder Add(GraphRelation relation);
        GraphPath ToPath();
    }
}