namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class SubjectProcessingScaffolding : IScaffolding
    {
        private IRootHandlersProvider _rootHandlersProvider;

        private IRootHandlersProvider GetRootHandlersProvider(Container container)
        {

            var defaultRootHandlers = container.GetInstance<IRootHandlerFactory>().CreateDefaults();

            var rootHandlers = defaultRootHandlers
                .Concat(_rootHandlersProvider.RootHandlers)
                .ToArray();

            var doubles = rootHandlers
                .GroupBy(fh => fh.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToArray();
            if (doubles.Any())
            {
                var message = String.Format("Double registered root handlers detected: {0}", String.Join(", ", doubles));
                throw new ScriptParserException(message);
            }

            return new RootHandlersProvider(rootHandlers);
        }
    }
}
