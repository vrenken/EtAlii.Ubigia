namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal partial class RootProcessingScaffolding : IScaffolding
    {
        public RootProcessingScaffolding(IRootHandlerMappersProvider rootHandlerMappersProvider)
        {
            _rootHandlerMappersProvider = rootHandlerMappersProvider;
        }

        public void Register(Container container)
        {
            container.Register<IRootContext, RootContext>();
            container.Register<IRootPathProcessor, RootPathProcessor>();
            container.Register<IRootHandlerMapperFinder, RootHandlerMapperFinder>();
            container.Register<IRootHandlerFinder, RootHandlerFinder>();

            container.Register<IRootHandlerPathMatcher, RootHandlerPathMatcher>();
            container.Register<IRootHandlerPathPartMatcherSelector, RootHandlerPathPartMatcherSelector>();
            container.Register<IPathSubjectPartContentGetter, PathSubjectPartContentGetter>();

            container.Register<ITypedRootHandlerPathPartMatcher, TypedRootHandlerPathPartMatcher>();
            container.Register<IRegexRootHandlerPathPartMatcher, RegexRootHandlerPathPartMatcher>();
            container.Register<IConstantRootHandlerPathPartMatcher, ConstantRootHandlerPathPartMatcher>();
            container.Register<IIsParentOfRootHandlerPathPartMatcher, IsParentOfRootHandlerPathPartMatcher>();
            container.Register<IIsChildOfRootHandlerPathPartMatcher, IsChildOfRootHandlerPathPartMatcher>();
            container.Register<IWildcardRootHandlerPathPartMatcher, WildcardRootHandlerPathPartMatcher>();
            container.Register<IVariableRootHandlerPathPartMatcher, VariableRootHandlerPathPartMatcher>();
            container.Register<IConditionalRootHandlerPathPartMatcher, ConditionalRootHandlerPathPartMatcher>();
            container.Register<IUpdateRootHandlerPathPartMatcher, UpdateRootHandlerPathPartMatcher>();
            container.Register<IDowndateRootHandlerPathPartMatcher, DowndateRootHandlerPathPartMatcher>();
            container.Register<IIdentifierRootHandlerPathPartMatcher, IdentifierRootHandlerPathPartMatcher>();

            container.Register<IRootHandlerMapperFactory, RootHandlerMapperFactory>();
            container.Register<IRootHandlerMappersProvider>(() => GetRootHandlerMappersProvider(container));

        }
    }
}
