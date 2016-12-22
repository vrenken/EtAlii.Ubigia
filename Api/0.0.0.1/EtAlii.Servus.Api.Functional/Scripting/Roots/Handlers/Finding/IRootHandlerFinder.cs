namespace EtAlii.Servus.Api.Functional
{
    public interface IRootHandlerFinder
    {
        IRootHandler Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject);
    }
}