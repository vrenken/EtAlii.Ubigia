// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaSchemaProcessorFactory : Factory<ISchemaProcessor, SchemaProcessorConfiguration, ISchemaProcessorExtension>, ISchemaProcessorFactory
    {
        protected override IScaffolding[] CreateScaffoldings(SchemaProcessorConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new SchemaProcessingScaffolding(configuration),
                new SchemaExecutionPlanningScaffolding(),
                //new SubjectProcessingScaffolding(configuration.FunctionHandlersProvider),
                //new RootProcessingScaffolding(configuration.RootHandlerMappersProvider),
                //new PathBuildingScaffolding(),
                //new ConstantHelpersScaffolding(),
                //new OperatorProcessingScaffolding(),
                //new ProcessingSelectorsScaffolding(),
                //new FunctionSubjectProcessingScaffolding(),

                // Query Parsing
                new LapaSchemaParserScaffolding(),

                // Additional processing (for path variable parts).
                //new PathSubjectParsingScaffolding(),

            };
        }

        protected override void InitializeInstance(ISchemaProcessor instance, Container container)
        {
//            var pathProcessor = container.GetInstance<IPathProcessor>()
//            var pathSubjectToGraphPathConverter = container.GetInstance<IPathSubjectToGraphPathConverter>()
//
//            var absolutePathSubjectProcessor = container.GetInstance<IAbsolutePathSubjectProcessor>()
//            var relativePathSubjectProcessor = container.GetInstance<IRelativePathSubjectProcessor>()
//            var rootedPathSubjectProcessor = container.GetInstance<IRootedPathSubjectProcessor>()
//
//            var pathSubjectForOutputConverter = container.GetInstance<IPathSubjectForOutputConverter>()
//            var addRelativePathToExistingPathProcessor = container.GetInstance<IAddRelativePathToExistingPathProcessor>()
//
//            container.GetInstance<IProcessingContext>().Initialize(pathSubjectToGraphPathConverter, absolutePathSubjectProcessor, relativePathSubjectProcessor, rootedPathSubjectProcessor, pathProcessor, pathSubjectForOutputConverter, addRelativePathToExistingPathProcessor)
        }
    }
}
