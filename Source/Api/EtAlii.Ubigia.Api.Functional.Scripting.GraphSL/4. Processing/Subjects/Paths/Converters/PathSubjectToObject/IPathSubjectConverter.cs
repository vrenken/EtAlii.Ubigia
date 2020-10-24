namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IPathSubjectConverter
    {
        Task<object> Convert(PathSubject pathSubject, ProcessParameters<Subject, SequencePart> parameters, ExecutionScope scope);
    }
}