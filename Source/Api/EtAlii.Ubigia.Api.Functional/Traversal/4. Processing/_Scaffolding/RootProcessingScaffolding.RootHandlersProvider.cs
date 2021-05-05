namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class RootProcessingScaffolding
    {
        private readonly IRootHandlerMappersProvider _rootHandlerMappersProvider;

        private IRootHandlerMappersProvider GetRootHandlerMappersProvider(Container container)
        {

            var defaultRootHandlerMappers = container.GetInstance<IRootHandlerMapperFactory>().CreateDefaults();

            var rootHandlerMappers = defaultRootHandlerMappers
                .Concat(_rootHandlerMappersProvider.RootHandlerMappers)
                .ToArray();

            var doubles = rootHandlerMappers
                .GroupBy(fh => fh.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToArray();
            if (doubles.Any())
            {
                var message = $"Double registered root handler mappers detected: {string.Join(", ", doubles)}";
                throw new ScriptParserException(message);
            }

            return new RootHandlerMappersProvider(rootHandlerMappers);
        }
    }
}
