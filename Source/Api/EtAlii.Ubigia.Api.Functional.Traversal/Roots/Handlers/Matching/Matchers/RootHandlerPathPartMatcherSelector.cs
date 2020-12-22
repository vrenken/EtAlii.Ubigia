namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Structure;

    internal class RootHandlerPathPartMatcherSelector : Selector<PathSubjectPart, IRootHandlerPathPartMatcher>, IRootHandlerPathPartMatcherSelector
    {
        public RootHandlerPathPartMatcherSelector(
            ITypedRootHandlerPathPartMatcher typedRootHandlerPathPartMatcher,
            IRegexRootHandlerPathPartMatcher regexRootHandlerPathPartMatcher,
            IConstantRootHandlerPathPartMatcher constantRootHandlerPathPartMatcher,
            IAllParentsRootHandlerPathPartMatcher allParentsRootHandlerPathPartMatcher,
            IParentRootHandlerPathPartMatcher parentRootHandlerPathPartMatcher,
            IAllChildrenRootHandlerPathPartMatcher allChildrenRootHandlerPathPartMatcher,
            IChildrenRootHandlerPathPartMatcher childrenRootHandlerPathPartMatcher,
            IAllNextRootHandlerPathPartMatcher allNextRootHandlerPathPartMatcher,
            INextRootHandlerPathPartMatcher nextRootHandlerPathPartMatcher,
            IAllPreviousRootHandlerPathPartMatcher allPreviousRootHandlerPathPartMatcher,
            IPreviousRootHandlerPathPartMatcher previousRootHandlerPathPartMatcher,
            IWildcardRootHandlerPathPartMatcher wildcardRootHandlerPathPartMatcher,
            IVariableRootHandlerPathPartMatcher variableRootHandlerPathPartMatcher,
            IConditionalRootHandlerPathPartMatcher conditionalRootHandlerPathPartMatcher,
            IAllUpdatesRootHandlerPathPartMatcher allUpdatesRootHandlerPathPartMatcher,
            IUpdatesRootHandlerPathPartMatcher updatesRootHandlerPathPartMatcher,
            IAllDowndatesRootHandlerPathPartMatcher allDowndatesRootHandlerPathPartMatcher,
            IDowndateRootHandlerPathPartMatcher downdateRootHandlerPathPartMatcher,
            IIdentifierRootHandlerPathPartMatcher identifierRootHandlerPathPartMatcher)
        {
            Register(part => part is TypedPathSubjectPart, typedRootHandlerPathPartMatcher)
                .Register(part => part is RegexPathSubjectPart, regexRootHandlerPathPartMatcher)
                .Register(part => part is ConstantPathSubjectPart, constantRootHandlerPathPartMatcher)
                .Register(part => part is AllParentsPathSubjectPart, allParentsRootHandlerPathPartMatcher)
                .Register(part => part is ParentPathSubjectPart, parentRootHandlerPathPartMatcher)
                .Register(part => part is AllChildrenPathSubjectPart, allChildrenRootHandlerPathPartMatcher)
                .Register(part => part is ChildrenPathSubjectPart, childrenRootHandlerPathPartMatcher)
                .Register(part => part is AllNextPathSubjectPart, allNextRootHandlerPathPartMatcher)
                .Register(part => part is NextPathSubjectPart, nextRootHandlerPathPartMatcher)
                .Register(part => part is AllPreviousPathSubjectPart, allPreviousRootHandlerPathPartMatcher)
                .Register(part => part is PreviousPathSubjectPart, previousRootHandlerPathPartMatcher)
                .Register(part => part is WildcardPathSubjectPart, wildcardRootHandlerPathPartMatcher)
                .Register(part => part is VariablePathSubjectPart, variableRootHandlerPathPartMatcher)
                .Register(part => part is ConditionalPathSubjectPart, conditionalRootHandlerPathPartMatcher)
                .Register(part => part is AllUpdatesPathSubjectPart, allUpdatesRootHandlerPathPartMatcher)
                .Register(part => part is UpdatesPathSubjectPart, updatesRootHandlerPathPartMatcher)
                .Register(part => part is DowndatePathSubjectPart, downdateRootHandlerPathPartMatcher)
                .Register(part => part is AllDowndatesPathSubjectPart, allDowndatesRootHandlerPathPartMatcher)
                .Register(part => part is IdentifierPathSubjectPart, identifierRootHandlerPathPartMatcher);
        }
    }
}
