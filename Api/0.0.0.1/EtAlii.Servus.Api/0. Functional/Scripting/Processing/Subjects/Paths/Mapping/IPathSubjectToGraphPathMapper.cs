namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    internal interface IPathSubjectToGraphPathMapper
    {
        GraphPath Map(PathSubject pathSubject);
    }
}
