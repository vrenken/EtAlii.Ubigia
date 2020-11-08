using System.Linq;
namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface IQueryHandler<TResult>
    {
        IQueryable<TResult> Handle(IQuery<TResult> query);
    }
}
