namespace EtAlii.Servus.Api.Functional
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    public interface IPathSubjectToGraphPathConverter
    {
        Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope);
    }
}