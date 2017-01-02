namespace EtAlii.Servus.Api.Functional
{
    internal interface IPathSubjectPartProcessor
    {
        object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters);
    }
}
