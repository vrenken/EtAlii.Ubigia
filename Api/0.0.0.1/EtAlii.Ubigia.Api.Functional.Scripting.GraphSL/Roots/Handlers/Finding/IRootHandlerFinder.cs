namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IRootHandlerFinder
    {
        Task<IRootHandler> Find(IScriptScope scope, IRootHandlerMapper rootHandlerMapper, RootedPathSubject rootedPathSubject);
    }
}