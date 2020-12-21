namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRootHandlerFinder
    {
        Task<IRootHandler> Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject);
    }
}
