namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddAbsolutePathToExistingPathProcessor : AddToExistingPathProcessorBase, IAddAbsolutePathToExistingPathProcessor
    {
        private readonly IRecursiveAdder _recursiveAdder;

        public AddAbsolutePathToExistingPathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IItemToIdentifierConverter itemToIdentifierConverter,
            IRecursiveAdder recursiveAdder)
            : base(itemToPathSubjectConverter, itemToIdentifierConverter)
        {
            _recursiveAdder = recursiveAdder;
        }

        protected override async Task Add(Identifier id, PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var parentId = id;
            IEditableEntry newEntry = null;
            foreach (var partToAdd in pathToAdd.Parts.OfType<ConstantPathSubjectPart>())
            {
                var addResult = await _recursiveAdder.Add(parentId, partToAdd, newEntry, scope);
                parentId = addResult.ParentId;
                newEntry = addResult.NewEntry;
            }
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
        }
    }
}
