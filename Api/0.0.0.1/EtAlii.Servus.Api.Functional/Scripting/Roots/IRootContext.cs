namespace EtAlii.Servus.Api.Functional
{
    public interface IRootContext
    {
        IPathSubjectForOutputConverter Converter { get; }
        IAddByNameToRelativePathProcessor AddByNameToRelativePathProcessor { get; }
    }
}