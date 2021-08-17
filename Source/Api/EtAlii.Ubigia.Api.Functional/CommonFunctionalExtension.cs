// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class CommonFunctionalExtension : IFunctionalExtension
    {
        private readonly FunctionalOptions _options;

        public CommonFunctionalExtension(FunctionalOptions options)
        {
            _options = options;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            // Let's ensure that the function handler configuration is in fact valid.
            var functionHandlersProvider = _options.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid.
            var rootHandlerMappersProvider = _options.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            container.Register(() => _options.ConfigurationRoot);

            new GraphContextScaffolding(_options).Register(container);
            new TraversalContextScaffolding(_options).Register(container);

            new SchemaExecutionPlanningScaffolding().Register(container);
            new SchemaProcessingScaffolding().Register(container);

            new ScriptExecutionPlanningScaffolding().Register(container);
            new SubjectProcessingScaffolding(_options.FunctionHandlersProvider).Register(container);
            new RootProcessingScaffolding(_options.RootHandlerMappersProvider).Register(container);
            new PathBuildingScaffolding().Register(container);
            new OperatorProcessingScaffolding().Register(container);
            new ProcessingSelectorsScaffolding().Register(container);
            new FunctionSubjectProcessingScaffolding().Register(container);
        }
    }
}
