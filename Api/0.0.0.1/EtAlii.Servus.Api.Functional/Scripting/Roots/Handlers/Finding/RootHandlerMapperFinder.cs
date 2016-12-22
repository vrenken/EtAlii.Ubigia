namespace EtAlii.Servus.Api.Functional
{
    using System.Linq;

    class RootHandlerMapperFinder : IRootHandlerMapperFinder
    {
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        public RootHandlerMapperFinder(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
        }

        public IRootHandlerMapper Find(RootedPathSubject rootedPathSubject)
        {
            var rootHandlerMapper = _rootHandlerMappersProvider.RootHandlerMappers.SingleOrDefault(rhp => System.String.Equals(rhp.Name, rootedPathSubject.Root, System.StringComparison.OrdinalIgnoreCase));
            if (rootHandlerMapper == null)
            {
                var message = System.String.Format("No root handler found with name '{0}'", rootedPathSubject.Root);
                throw new ScriptParserException(message);
            }
            return rootHandlerMapper;
        }
    }
}