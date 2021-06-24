// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class SubjectProcessingScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;

        private IFunctionHandlersProvider GetFunctionHandlersProvider(Container container)
        {

            var defaultFunctionHandlers = container.GetInstance<IFunctionHandlerFactory>().CreateDefaults();

            var functionHandlers = defaultFunctionHandlers
                .Concat(_functionHandlersProvider.FunctionHandlers)
                .ToArray();

            var doubles = functionHandlers
                .GroupBy(fh => fh.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToArray();
            if (doubles.Any())
            {
                var message = $"Double registered function handlers detected: {string.Join(", ", doubles)}";
                throw new ScriptParserException(message);
            }

            return new FunctionHandlersProvider(functionHandlers);
        }
    }
}
