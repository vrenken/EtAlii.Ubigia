using SimpleInjector;
namespace EtAlii.xTechnology.Workflow
{
    public interface IQuery<TResult>
    {
        IQueryHandler<TResult> GetHandler(Container container);
    }
}
