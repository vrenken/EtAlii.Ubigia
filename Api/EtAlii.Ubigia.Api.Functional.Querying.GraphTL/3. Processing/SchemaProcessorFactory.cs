// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaProcessorFactory : Factory<ISchemaProcessor, SchemaProcessorConfiguration, ISchemaProcessorExtension>, ISchemaProcessorFactory
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
                new SchemaParserScaffolding(),
                
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
