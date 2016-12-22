﻿namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.xTechnology.Structure;

    public class RootHandlerPathPartMatcherSelector : Selector<PathSubjectPart, IRootHandlerPathPartMatcher>, IRootHandlerPathPartMatcherSelector
    {
        public RootHandlerPathPartMatcherSelector(
            ITypedRootHandlerPathPartMatcher typedRootHandlerPathPartMatcher,
            IConstantRootHandlerPathPartMatcher constantRootHandlerPathPartMatcher,
            IIsParentOfRootHandlerPathPartMatcher isParentOfRootHandlerPathPartMatcher,
            IIsChildOfRootHandlerPathPartMatcher isChildOfRootHandlerPathPartMatcher,
            IWildcardRootHandlerPathPartMatcher wildcardRootHandlerPathPartMatcher,
            IVariableRootHandlerPathPartMatcher variableRootHandlerPathPartMatcher,
            IConditionalRootHandlerPathPartMatcher conditionalRootHandlerPathPartMatcher,
            IUpdateRootHandlerPathPartMatcher updateRootHandlerPathPartMatcher,
            IDowndateRootHandlerPathPartMatcher downdateRootHandlerPathPartMatcher,
            IIdentifierRootHandlerPathPartMatcher identifierRootHandlerPathPartMatcher)
        {
            this.Register(part => part is TypedPathSubjectPart, typedRootHandlerPathPartMatcher)
                .Register(part => part is ConstantPathSubjectPart, constantRootHandlerPathPartMatcher)
                .Register(part => part is IsParentOfPathSubjectPart, isParentOfRootHandlerPathPartMatcher)
                .Register(part => part is IsChildOfPathSubjectPart, isChildOfRootHandlerPathPartMatcher)
                .Register(part => part is WildcardPathSubjectPart, wildcardRootHandlerPathPartMatcher)
                .Register(part => part is VariablePathSubjectPart, variableRootHandlerPathPartMatcher)
                .Register(part => part is ConditionalPathSubjectPart, conditionalRootHandlerPathPartMatcher)
                .Register(part => part is UpdatesOfPathSubjectPart, updateRootHandlerPathPartMatcher)
                .Register(part => part is DowndateOfPathSubjectPart, downdateRootHandlerPathPartMatcher)
                .Register(part => part is IdentifierPathSubjectPart, identifierRootHandlerPathPartMatcher);
        }
    }
}