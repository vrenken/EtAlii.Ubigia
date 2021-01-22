namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal partial class SubjectProcessingScaffolding : IScaffolding
    {
        public SubjectProcessingScaffolding(
            IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
        }

        public void Register(Container container)
        {
            container.Register<IAbsolutePathSubjectProcessor, AbsolutePathSubjectProcessor>();
            container.Register<IRelativePathSubjectProcessor, RelativePathSubjectProcessor>();
            container.Register<IRootedPathSubjectProcessor, RootedPathSubjectProcessor>();
            container.Register<IVariableSubjectProcessor, VariableSubjectProcessor>();
            container.Register<IPathSubjectForOutputConverter, PathSubjectForOutputConverter>();
            container.Register<IStringConstantSubjectProcessor, StringConstantSubjectProcessor>();
            container.Register<IObjectConstantSubjectProcessor, ObjectConstantSubjectProcessor>();
            container.Register<IConstantSubjectProcessorSelector, ConstantSubjectProcessorSelector>();

            container.Register<IRootSubjectProcessor, RootSubjectProcessor>();
            container.Register<IRootDefinitionSubjectProcessor, RootDefinitionSubjectProcessor>();

            container.Register<IFunctionHandlerFactory, FunctionHandlerFactory>();
            container.Register(() => GetFunctionHandlersProvider(container));
        }
    }
}
