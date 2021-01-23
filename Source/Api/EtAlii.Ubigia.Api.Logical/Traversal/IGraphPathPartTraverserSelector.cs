namespace EtAlii.Ubigia.Api.Logical
{
    public interface IGraphPathPartTraverserSelector
    {
        IGraphPathPartTraverser Select(GraphPathPart part);
    }
}
