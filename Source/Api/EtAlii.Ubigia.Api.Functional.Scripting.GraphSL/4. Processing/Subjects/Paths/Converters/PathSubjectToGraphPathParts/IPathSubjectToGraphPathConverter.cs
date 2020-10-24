namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public interface IPathSubjectToGraphPathConverter
    {
        Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope);
    }
}