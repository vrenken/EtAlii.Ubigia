namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.Structure;

    internal class ConstantPathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private readonly ProcessingContext _context;
        private readonly ISelector<object, Func<object, Identifier[]>> _inputConverterSelector;

        public ConstantPathSubjectPartProcessor(
            ProcessingContext context,
            ISelector<object, Func<object, Identifier[]>> inputConverterSelector)
        {
            _context = context;
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
            var result = ids.Select(id => _context.Logical.Nodes.Select(id, GraphPath.Empty))
                .Where(e => e.Type == name)
                .Select(e => new DynamicNode(e))
                .ToArray();
            return result;
        }
    }
}
