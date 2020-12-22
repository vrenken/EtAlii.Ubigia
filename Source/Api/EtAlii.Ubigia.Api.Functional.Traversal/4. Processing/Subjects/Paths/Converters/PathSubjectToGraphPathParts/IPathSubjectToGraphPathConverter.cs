namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public interface IPathSubjectToGraphPathConverter
    {
        Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope);
    }
}
