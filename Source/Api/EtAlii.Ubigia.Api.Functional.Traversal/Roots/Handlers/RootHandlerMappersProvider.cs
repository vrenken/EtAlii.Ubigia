namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal class RootHandlerMappersProvider : IRootHandlerMappersProvider
    {
        public IRootHandlerMapper[] RootHandlerMappers { get; }

        /// <summary>
        /// An empty RootHandlerMappersProvider.
        /// </summary>
        public static IRootHandlerMappersProvider Empty { get; } = new RootHandlerMappersProvider(Array.Empty<IRootHandlerMapper>());

        public RootHandlerMappersProvider(IRootHandlerMapper[] rootHandlerMappers)
        {
            RootHandlerMappers = rootHandlerMappers;
        }
    }
}
