// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal class CommonFunctionalExtension : IExtension
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
            container.Register(() => _options.LogicalContext);
            container.Register<IFunctionalOptions>(() => _options);

            container.Register<IGraphContext, GraphContext>();
            container.Register<ITraversalContext, TraversalContext>();

            // Schema execution planning.
            container.Register<ISchemaExecutionPlanner, SchemaExecutionPlanner>();

            // Schema processing.
            container.Register<IScriptProcessor, ScriptProcessor>();
            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.RegisterInitializer<IScriptProcessingContext>((services, instance) =>
            {
                var pathProcessor = services.GetInstance<IPathProcessor>();
                var pathSubjectToGraphPathConverter = services.GetInstance<IPathSubjectToGraphPathConverter>();

                var absolutePathSubjectProcessor = services.GetInstance<IAbsolutePathSubjectProcessor>();
                var relativePathSubjectProcessor = services.GetInstance<IRelativePathSubjectProcessor>();
                var rootedPathSubjectProcessor = services.GetInstance<IRootedPathSubjectProcessor>();

                var pathSubjectForOutputConverter = services.GetInstance<IPathSubjectForOutputConverter>();
                var addRelativePathToExistingPathProcessor = services.GetInstance<IAddRelativePathToExistingPathProcessor>();

                instance.Initialize(
                    pathSubjectToGraphPathConverter,
                    absolutePathSubjectProcessor,
                    relativePathSubjectProcessor,
                    rootedPathSubjectProcessor,
                    pathProcessor,
                    pathSubjectForOutputConverter,
                    addRelativePathToExistingPathProcessor);
            });

            container.Register<ISchemaProcessor, SchemaProcessor>();

            container.Register<IQueryValueProcessor, QueryValueProcessor>();
            container.Register<IMutationValueProcessor, MutationValueProcessor>();
            container.Register<IValueGetter, ValueGetter>();
            container.Register<IValueSetter, ValueSetter>();
            container.Register<IPropertiesValueGetter, PropertiesValueGetter>();
            container.Register<IPropertiesValueSetter, PropertiesValueSetter>();
            container.Register<IPathValueGetter, PathValueGetter>();
            container.Register<IPathValueSetter, PathValueSetter>();

            container.Register<IQueryStructureProcessor, QueryStructureProcessor>();
            container.Register<IMutationStructureProcessor, MutationStructureProcessor>();

            container.Register<IRelatedIdentityFinder, RelatedIdentityFinder>();
            container.Register<IPathDeterminer, PathDeterminer>();
            container.Register<IPathStructureBuilder, PathStructureBuilder>();
            container.Register<IPathCorrecter, PathCorrecter>();


            // Script execution planning.
            container.Register<IScriptExecutionPlanner, ScriptExecutionPlanner>();
            container.Register<ISequenceExecutionPlanner, SequenceExecutionPlanner>();

            container.Register<ICommentExecutionPlanner, CommentExecutionPlanner>();
            container.Register<ISubjectExecutionPlannerSelector, SubjectExecutionPlannerSelector>();
            container.Register<IAbsolutePathSubjectExecutionPlanner, AbsolutePathSubjectExecutionPlanner>();
            container.Register<IRelativePathSubjectExecutionPlanner, RelativePathSubjectExecutionPlanner>();
            container.Register<IRootedPathSubjectExecutionPlanner, RootedPathSubjectExecutionPlanner>();
            container.Register<IFunctionSubjectExecutionPlanner, FunctionSubjectExecutionPlanner>();
            container.Register<IConstantSubjectExecutionPlanner, ConstantSubjectExecutionPlanner>();
            container.Register<IVariableSubjectExecutionPlanner, VariableSubjectExecutionPlanner>();
            container.Register<IRootSubjectExecutionPlanner, RootSubjectExecutionPlanner>();
            container.Register<IRootDefinitionSubjectExecutionPlanner, RootDefinitionSubjectExecutionPlanner>();

            container.Register<IOperatorExecutionPlannerSelector, OperatorExecutionPlannerSelector>();
            container.Register<IRemoveOperatorExecutionPlanner, RemoveOperatorExecutionPlanner>();
            container.Register<IAddOperatorExecutionPlanner, AddOperatorExecutionPlanner>();
            container.Register<IAssignOperatorExecutionPlanner, AssignOperatorExecutionPlanner>();

            container.Register<ISequencePartExecutionPlannerSelector, SequencePartExecutionPlannerSelector>();

            container.Register<IExecutionPlanCombinerSelector, ExecutionPlanCombinerSelector>();
            container.Register<ISubjectExecutionPlanCombiner, SubjectExecutionPlanCombiner>();
            container.Register<IOperatorExecutionPlanCombiner, OperatorExecutionPlanCombiner>();


            // Subject processing.
            new SubjectProcessingScaffolding(_options.FunctionHandlersProvider).Register(container);

            // Root processing.
            new RootProcessingScaffolding(_options.RootHandlerMappersProvider).Register(container);

            // Path building.
            container.Register<IPathVariableExpander, PathVariableExpander>();

            container.Register<IConstantPathSubjectPartToGraphPathPartsConverter, ConstantPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IWildcardPathSubjectPartToGraphPathPartsConverter, WildcardPathSubjectPartToGraphPathPartsConverter>();
            container.Register<ITaggedPathSubjectPartToGraphPathPartsConverter, TaggedPathSubjectPartToGraphPathPartsConverter>();
            container.Register<ITraversingWildcardPathSubjectPartToGraphPathPartsConverter, TraversingWildcardPathSubjectPartToGraphPathPartsConverter>();

            container.Register<IConditionalPathSubjectPartToGraphPathPartsConverter, ConditionalPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IEqualPredicateFactory, EqualPredicateFactory>();
            container.Register<INotEqualPredicateFactory, NotEqualPredicateFactory>();
            container.Register<ILessThanPredicateFactory, LessThanPredicateFactory>();
            container.Register<ILessThanOrEqualPredicateFactory, LessThanOrEqualPredicateFactory>();
            container.Register<IMoreThanPredicateFactory, MoreThanPredicateFactory>();
            container.Register<IMoreThanOrEqualPredicateFactory, MoreThanOrEqualPredicateFactory>();

            container.Register<IIdentifierPathSubjectPartToGraphPathPartsConverter, IdentifierPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllParentsPathSubjectPartToGraphPathPartsConverter, AllParentsPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IParentPathSubjectPartToGraphPathPartsConverter, ParentPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllChildrenPathSubjectPartToGraphPathPartsConverter, AllChildrenPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IChildrenPathSubjectPartToGraphPathPartsConverter, ChildrenPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllDowndatesPathSubjectPartToGraphPathPartsConverter, AllDowndatesPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IDowndatePathSubjectPartToGraphPathPartsConverter, DowndatePathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllUpdatesPathSubjectPartToGraphPathPartsConverter, AllUpdatesPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IUpdatesPathSubjectPartToGraphPathPartsConverter, UpdatesPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IPathSubjectToGraphPathConverter, PathSubjectToGraphPathConverter>();
            container.Register<IPathProcessor, PathProcessor>();

            container.Register<IVariablePathSubjectPartToPathConverter, VariablePathSubjectPartToPathConverter>();
            container.Register<IVariablePathSubjectPartToGraphPathPartsConverter, VariablePathSubjectPartToGraphPathPartsConverter>();


            // Operator processing.
            new OperatorProcessingScaffolding().Register(container);

            // Processing selectors.
            container.Register<IItemToIdentifierConverter, ItemToIdentifierConverter>();
            container.Register<IItemToPathSubjectConverter, ItemToPathSubjectConverter>();
            container.Register<IEntriesToDynamicNodesConverter, EntriesToDynamicNodesConverter>();


            // Functional subjects.
            container.Register<IFunctionSubjectProcessor, FunctionSubjectProcessor>();
            container.Register<IParameterSetFinder, ParameterSetFinder>();
            container.Register<IFunctionHandlerFinder, FunctionHandlerFinder>();
            container.Register<IArgumentSetFinder, ArgumentSetFinder>();

            container.Register<IFunctionContext, FunctionContext>();

            container.Register<INonRootedPathSubjectFunctionParameterConverter, NonRootedPathSubjectFunctionParameterConverter>();
            container.Register<IRootedPathSubjectFunctionParameterConverter, RootedPathSubjectFunctionParameterConverter>();
            container.Register<IConstantSubjectFunctionParameterConverter, ConstantSubjectFunctionParameterConverter>();
            container.Register<IVariableSubjectFunctionParameterConverter, VariableSubjectFunctionParameterConverter>();

        }
    }
}
