namespace EtAlii.Servus.Api.Functional
{
    public interface IRootHandlerMapperFinder
    {
        IRootHandlerMapper Find(RootedPathSubject rootedPathSubject);
    }
}