namespace EtAlii.Ubigia.Api.Logical
{
    public interface IGraphPathTraverserFactory
    {
        IGraphPathTraverser Create(GraphPathTraverserConfiguration configuration);
    }
}