namespace EtAlii.Servus.Api.Logical
{
    public interface IGraphPathTraverserFactory
    {
        IGraphPathTraverser Create(IGraphPathTraverserConfiguration configuration);
    }
}