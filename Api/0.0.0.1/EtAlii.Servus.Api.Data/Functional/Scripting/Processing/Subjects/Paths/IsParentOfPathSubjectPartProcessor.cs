namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class IsParentOfPathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private readonly ProcessingContext _context;
        private readonly ITimeTraverser _timeTraverser;
        private readonly ISelector<object, Func<object, Identifier[]>> _inputConverterSelector;

        public IsParentOfPathSubjectPartProcessor(
            ProcessingContext context,
            ITimeTraverser timeTraverser,
            ISelector<object, Func<object, Identifier[]>> inputConverterSelector  
 )
        {
            _context = context;
            _timeTraverser = timeTraverser;
            _inputConverterSelector = inputConverterSelector;
        }

        public object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            object result = null;
            if (parameters.LeftPart == null)
            {
                // We need to fetch the root if the path starts with a /.
                // TODO: This needs to be implemented differently.
                var root = _context.Connection.Roots.Get("Time");
                var parentId = _context.Connection.Entries.Get(root).Parent.Id;
                result = _context.Connection.Entries.GetRelated(parentId, EntryRelation.Child)
                    .Select(e => _timeTraverser.Traverse(e))
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

                result = ids.SelectMany(id => _context.Connection.Entries.GetRelated(id, EntryRelation.Child)
                    .Select(e => _timeTraverser.Traverse(e))
                    .Select(e => new DynamicNode(e)))
                    .ToArray();
            }

            return result;
        }

    }
}
