namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    internal class RootHandlerMapperFinder : IRootHandlerMapperFinder
    {
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        public RootHandlerMapperFinder(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
        }

        public IRootHandlerMapper Find(string root)
        {
            var rootHandlerMapper = _rootHandlerMappersProvider.RootHandlerMappers.SingleOrDefault(rhp => string.Equals(rhp.Name, root, System.StringComparison.OrdinalIgnoreCase));
            if (rootHandlerMapper == null)
            {
                throw new ScriptParserException($"No root handler found with name '{root}'");
            }
            return rootHandlerMapper;
        }
    }
}
