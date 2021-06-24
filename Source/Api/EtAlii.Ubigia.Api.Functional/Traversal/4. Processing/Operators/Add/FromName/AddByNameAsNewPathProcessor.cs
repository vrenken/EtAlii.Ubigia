// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddByNameAsNewPathProcessor : IAddByNameAsNewPathProcessor
    {
        private readonly IItemToPathSubjectConverter _itemToPathSubjectConverter;
        private readonly IRecursiveAdder _recursiveAdder;
        private readonly IScriptProcessingContext _context;

        public AddByNameAsNewPathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IRecursiveAdder recursiveAdder,
            IScriptProcessingContext context)
        {
            _itemToPathSubjectConverter = itemToPathSubjectConverter;
            _recursiveAdder = recursiveAdder;
            _context = context;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var pathToAdd = await GetPathToAdd(parameters).ConfigureAwait(false);
            if (pathToAdd == null)
            {
                throw new ScriptProcessingException($"The {GetType().Name} requires a path on the right side");
            }

            if (!pathToAdd.Parts.All(part => part is ConstantPathSubjectPart || part is ParentPathSubjectPart))
            {
                throw new ScriptProcessingException($"The {GetType().Name} requires a constant, hierarchical path");
            }

            if (pathToAdd.Parts.Any(part => part is ConstantPathSubjectPart constantPathSubjectPart && string.IsNullOrWhiteSpace(constantPathSubjectPart.Name)))
            {
                throw new ScriptProcessingException($"The {GetType().Name} cannot handle empty parts");
            }

            await Add(pathToAdd, parameters.Scope, parameters.Output).ConfigureAwait(false);
        }

        private async Task<PathSubject> GetPathToAdd(OperatorParameters parameters)
        {
            PathSubject pathToAdd = null;

            if (parameters.RightSubject is PathSubject pathSubject)
            {
                pathToAdd = pathSubject;
            }
            else
            {
                var rightResult = await parameters.RightInput.SingleOrDefaultAsync();

                _itemToPathSubjectConverter.TryConvert(rightResult, out pathToAdd);
            }

            return pathToAdd;
        }

        private async Task Add(PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var partsToAdd = pathToAdd.Parts
                .OfType<ConstantPathSubjectPart>()
                .ToArray();

            var rootPartToAdd = partsToAdd.First();
            var root = await _context.Logical.Roots.Get(rootPartToAdd.Name).ConfigureAwait(false);

            if (root == null)
            {
                throw new ScriptProcessingException("The AddByNameToAbsolutePathProcessor requires an absolute path to contain a valid root");
            }

            var parentId = root.Identifier;
            IEditableEntry newEntry = null;
            foreach (var partToAdd in partsToAdd.Skip(1))
            {
                var addResult = await _recursiveAdder.Add(parentId, partToAdd, newEntry, scope).ConfigureAwait(false);
                parentId = addResult.ParentId;
                newEntry = addResult.NewEntry;
            }
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
            output.OnCompleted();
        }
    }
}
