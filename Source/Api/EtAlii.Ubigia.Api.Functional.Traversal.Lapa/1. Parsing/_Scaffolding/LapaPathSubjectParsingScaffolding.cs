namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaPathSubjectParsingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<INonRootedPathSubjectParser, NonRootedPathSubjectParser>();
            container.Register<IRootedPathSubjectParser, RootedPathSubjectParser>();
            container.Register<IPathSubjectPartsParser, PathSubjectPartsParser>();
            container.Register<ITaggedPathSubjectPartParser, TaggedPathSubjectPartParser>();
            container.Register<IWildcardPathSubjectPartParser, WildcardPathSubjectPartParser>();
            container.Register<ITraversingWildcardPathSubjectPartParser, TraversingWildcardPathSubjectPartParser>();

            container.Register<IConditionalPathSubjectPartParser, ConditionalPathSubjectPartParser>();
            container.Register<IConditionParser, ConditionParser>();

            container.Register<IConstantPathSubjectPartParser, ConstantPathSubjectPartParser>();
            container.Register<IVariablePathSubjectPartParser, VariablePathSubjectPartParser>();
            container.Register<IIdentifierPathSubjectPartParser, IdentifierPathSubjectPartParser>();

            container.Register<IAllParentsPathSubjectPartParser, AllParentsPathSubjectPartParser>();
            container.Register<IParentPathSubjectPartParser, ParentPathSubjectPartParser>();
            container.Register<IAllChildrenPathSubjectPartParser, AllChildrenPathSubjectPartParser>();
            container.Register<IChildrenPathSubjectPartParser, ChildrenPathSubjectPartParser>();

            container.Register<IAllDowndatesPathSubjectPartParser, AllDowndatesPathSubjectPartParser>();
            container.Register<IDowndatePathSubjectPartParser, DowndatePathSubjectPartParser>();
            container.Register<IAllUpdatesPathSubjectPartParser, AllUpdatesPathSubjectPartParser>();
            container.Register<IUpdatesPathSubjectPartParser, UpdatesPathSubjectPartParser>();

            container.Register<IAllPreviousPathSubjectPartParser, AllPreviousPathSubjectPartParser>();
            container.Register<IPreviousPathSubjectPartParser, PreviousPathSubjectPartParser>();
            container.Register<IAllNextPathSubjectPartParser, AllNextPathSubjectPartParser>();
            container.Register<INextPathSubjectPartParser, NextPathSubjectPartParser>();

            container.Register<ITypedPathSubjectPartParser, TypedPathSubjectPartParser>();
            container.Register<IRegexPathSubjectPartParser, RegexPathSubjectPartParser>();

            // Path helpers
            container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>();

        }
    }
}
