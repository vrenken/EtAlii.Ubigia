namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Structure;

    internal class AddOperatorProcessor : IOperatorProcessor
    {
        private readonly ProcessingContext _context;
        private readonly ISelector<object, Func<object, IEnumerable<Identifier>>> _inputConverterSelector;

        public AddOperatorProcessor(
            ProcessingContext context,
            ISelector<object, Func<object, IEnumerable<Identifier>>> inputConverterSelector)
        {
            _context = context;
            _inputConverterSelector = inputConverterSelector;
        }

        public object Process(ProcessParameters<Operator, SequencePart> parameters)
        {
            var items = parameters.LeftResult;
            var inputConvertor = _inputConverterSelector.Select(items);
            var ids = inputConvertor(items);
            if (ids == null || !ids.Any())
            {
                throw new ScriptProcessingException("The AddOperatorProcessor requires queryable ids from the previous path part");
            }

            var pathToAdd = parameters.RightResult as PathSubject;
            if (pathToAdd == null)
            {
                throw new ScriptProcessingException("The AddOperatorProcessor requires a path on the right side");
            }

            var result = new List<INode>();

            foreach (var id in ids)
            {
                var parentId = id;
                IEditableEntry newEntry = null;
                foreach (var part in pathToAdd.Parts.OfType<ConstantPathSubjectPart>())
                {
                    newEntry = _context.Connection.Entries.Prepare();
                    newEntry.Parent = Relation.NewRelation(parentId);
                    newEntry.Type = part.Name;
                    _context.Connection.Entries.Change(newEntry);
                    parentId = newEntry.Id;
                }
                result.Add(new DynamicNode((IReadOnlyEntry)newEntry));
            }
            return result.ToArray();
        }
    }
}
