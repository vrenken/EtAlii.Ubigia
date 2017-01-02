namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class ConstantPathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private readonly ProcessingContext _context;
        private readonly ITimeTraverser _timeTraverser;
        private readonly ISelector<object, Func<object, Identifier[]>> _inputConverterSelector;

        public ConstantPathSubjectPartProcessor(
            ProcessingContext context,
            ITimeTraverser timeTraverser,
            ISelector<object, Func<object, Identifier[]>> inputConverterSelector)
        {
            _context = context;
            _timeTraverser = timeTraverser;
            _inputConverterSelector = inputConverterSelector;
        }

        public object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            var items = parameters.LeftResult;
            var inputConvertor = _inputConverterSelector.Select(items);
            var ids = inputConvertor(items);
            if (ids == null || !ids.Any())
            {
                throw new ScriptProcessingException("The ConstantPathSubjectPartProcessor requires queryable ids from the previous path part");
            }
            var name = ((ConstantPathSubjectPart)parameters.Target).Name;
            var result = _context.Connection.Entries.Get(ids)
                .Select(entry => _timeTraverser.Traverse(entry))
                .Where(e => e.Type == name)
                .Select(e => new DynamicNode(e))
                .ToArray();
            return result;
        }
    }
}
