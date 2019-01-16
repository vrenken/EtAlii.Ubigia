﻿namespace EtAlii.Ubigia.Api.Functional
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
            container.Register<IAllParentsRootHandlerPathPartMatcher, AllParentsRootHandlerPathPartMatcher>();
            container.Register<IParentRootHandlerPathPartMatcher, ParentRootHandlerPathPartMatcher>();
            container.Register<IAllChildrenRootHandlerPathPartMatcher, AllChildrenRootHandlerPathPartMatcher>();
            container.Register<IChildrenRootHandlerPathPartMatcher, ChildrenRootHandlerPathPartMatcher>();
            container.Register<IAllNextRootHandlerPathPartMatcher, AllNextRootHandlerPathPartMatcher>();
            container.Register<INextRootHandlerPathPartMatcher, NextRootHandlerPathPartMatcher>();
            container.Register<IAllPreviousRootHandlerPathPartMatcher, AllPreviousRootHandlerPathPartMatcher>();
            container.Register<IPreviousRootHandlerPathPartMatcher, PreviousRootHandlerPathPartMatcher>();
            container.Register<IAllUpdatesRootHandlerPathPartMatcher, AllUpdatesRootHandlerPathPartMatcher>();
            container.Register<IUpdatesRootHandlerPathPartMatcher, UpdatesRootHandlerPathPartMatcher>();
            container.Register<IAllDowndatesRootHandlerPathPartMatcher, AllDowndatesRootHandlerPathPartMatcher>();
            container.Register<IDowndateRootHandlerPathPartMatcher, DowndateRootHandlerPathPartMatcher>();
            container.Register<IWildcardRootHandlerPathPartMatcher, WildcardRootHandlerPathPartMatcher>();
            container.Register<IVariableRootHandlerPathPartMatcher, VariableRootHandlerPathPartMatcher>();
            container.Register<IConditionalRootHandlerPathPartMatcher, ConditionalRootHandlerPathPartMatcher>();
            container.Register<IIdentifierRootHandlerPathPartMatcher, IdentifierRootHandlerPathPartMatcher>();

            container.Register<IRootHandlerMapperFactory, RootHandlerMapperFactory>();
            container.Register(() => GetRootHandlerMappersProvider(container));

        }
    }
}
