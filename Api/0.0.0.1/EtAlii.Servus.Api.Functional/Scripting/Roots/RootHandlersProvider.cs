namespace EtAlii.Servus.Api.Functional
{
    internal class RootHandlersProvider : IRootHandlersProvider
    {
        public IRootHandler[] RootHandlers { get { return _rootHandlers; } }
        private readonly IRootHandler[] _rootHandlers;

        public static readonly IRootHandlersProvider Empty = new RootHandlersProvider(new IRootHandler[] {});

        public RootHandlersProvider(IRootHandler[] rootHandlers)
        {
            _rootHandlers = rootHandlers;
        }
    }
}