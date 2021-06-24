// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddRelativePathToExistingPathProcessor : AddToExistingPathProcessorBase, IAddRelativePathToExistingPathProcessor
    {
        private readonly IRecursiveAdder _recursiveAdder;

        public AddRelativePathToExistingPathProcessor(
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
                var addResult = await _recursiveAdder.Add(parentId, partToAdd, newEntry, scope).ConfigureAwait(false);
                parentId = addResult.ParentId;
                newEntry = addResult.NewEntry;
            }
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
        }
    }
}
