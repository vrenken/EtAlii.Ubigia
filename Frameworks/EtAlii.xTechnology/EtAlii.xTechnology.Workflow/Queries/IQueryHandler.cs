using System.Linq;
namespace EtAlii.xTechnology.Workflow
{
    public interface IQueryHandler<TResult>
    {
        IQueryable<TResult> Handle(IQuery<TResult> query);
    }
}
