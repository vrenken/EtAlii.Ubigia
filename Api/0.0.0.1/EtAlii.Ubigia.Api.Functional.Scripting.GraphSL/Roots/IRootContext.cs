namespace EtAlii.Ubigia.Api.Functional
{
    public interface IRootContext
    {
        IPathSubjectForOutputConverter Converter { get; }
        IAddByNameToExistingPathProcessor AddByNameToExistingPathProcessor { get; }
    }
}