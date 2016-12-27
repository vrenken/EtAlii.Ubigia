namespace EtAlii.Servus.Api.Functional
{
    internal interface IRootHandlerMapperFinder
    {
        IRootHandlerMapper Find(RootedPathSubject rootedPathSubject);
    }
}