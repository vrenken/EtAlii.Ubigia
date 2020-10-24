namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal class RootHandlerMappersProvider : IRootHandlerMappersProvider
    {
        public IRootHandlerMapper[] RootHandlerMappers { get; }

        /// <summary>
        /// An empty RootHandlerMappersProvider.
        /// </summary>
        public static IRootHandlerMappersProvider Empty { get; } = new RootHandlerMappersProvider(new IRootHandlerMapper[] {});

        public RootHandlerMappersProvider(IRootHandlerMapper[] rootHandlerMappers)
        {
            RootHandlerMappers = rootHandlerMappers;
        }
    }
}