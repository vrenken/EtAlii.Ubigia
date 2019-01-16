namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootHandlerMappersProvider : IRootHandlerMappersProvider
    {
        public IRootHandlerMapper[] RootHandlerMappers { get; }

        public static readonly IRootHandlerMappersProvider Empty = new RootHandlerMappersProvider(new IRootHandlerMapper[] {});

        public RootHandlerMappersProvider(IRootHandlerMapper[] rootHandlerMappers)
        {
            RootHandlerMappers = rootHandlerMappers;
        }
    }
}