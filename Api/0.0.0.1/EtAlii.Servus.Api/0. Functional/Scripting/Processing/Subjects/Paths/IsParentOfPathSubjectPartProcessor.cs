namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class IsParentOfPathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private readonly ProcessingContext _context;
        private readonly ISelector<object, Func<object, Identifier[]>> _inputConverterSelector;

        public IsParentOfPathSubjectPartProcessor(
            ProcessingContext context,
            ISelector<object, Func<object, Identifier[]>> inputConverterSelector  
 )
        {
            _context = context;
            _inputConverterSelector = inputConverterSelector;
        }

        public object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            object result = null;
            if (parameters.LeftPart == null)
            {
                // We need to fetch the root if the path starts with a /.
                // TODO: This needs to be implemented differently.
                var root = _context.Logical.Roots.Get("Time");
                var parentId = _context.Logical.Nodes.Select(root.Identifier, GraphPath.Empty).Parent.Id;
                result = _context.Logical.Nodes.SelectMany(parentId, new GraphPath(GraphRelation.Child))
                    .Select(e => new DynamicNode(e))
                    .ToArray();
            }
            else
            {
                var items = parameters.LeftResult;
                var inputConvertor = _inputConverterSelector.Select(items);
                var ids = inputConvertor(items).ToArray();
                if (ids == null || !ids.Any())
                {
                    throw new ScriptProcessingException("The IsParentOfPathSubjectPartProcessor requires queryable ids from the previous path part");
                }

                result = ids.SelectMany(id => _context.Logical.Nodes.SelectMany(id, new GraphPath(GraphRelation.Child))
                    .Select(e => new DynamicNode(e)))
                    .ToArray();
            }

            return result;
        }

    }
}
