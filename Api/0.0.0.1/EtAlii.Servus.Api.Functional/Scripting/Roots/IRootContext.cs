namespace EtAlii.Servus.Api.Functional
{
    public interface IRootContext
    {
        IPathProcessor PathProcessor { get; }
        IToIdentifierConverter ToIdentifierConverter { get; }
        IPathSubjectForOutputConverter Converter { get; }
    }
}