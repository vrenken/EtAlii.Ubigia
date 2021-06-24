// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class AssignCombinedToPathOperatorSubProcessor : AssignToPathOperatorSubProcessorBase, IAssignCombinedToPathOperatorSubProcessor
    {
        public AssignCombinedToPathOperatorSubProcessor(
            IItemToIdentifierConverter itemToIdentifierConverter,
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IEntriesToDynamicNodesConverter entriesToDynamicNodesConverter,
            IScriptProcessingContext context)
            : base(itemToIdentifierConverter, pathSubjectToGraphPathConverter, entriesToDynamicNodesConverter, context)
        {
        }
    }
}
