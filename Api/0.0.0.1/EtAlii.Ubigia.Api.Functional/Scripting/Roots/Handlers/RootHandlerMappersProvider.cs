namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootHandlerMappersProvider : IRootHandlerMappersProvider
    {
        public IRootHandlerMapper[] RootHandlerMappers { get { return _rootHandlerMappers; } }
        private readonly IRootHandlerMapper[] _rootHandlerMappers;

        public static readonly IRootHandlerMappersProvider Empty = new RootHandlerMappersProvider(new IRootHandlerMapper[] {});

        public RootHandlerMappersProvider(IRootHandlerMapper[] rootHandlerMappers)
        {
            _rootHandlerMappers = rootHandlerMappers;
        }
    }
}