// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Collections;

    internal class RecursiveAdder : IRecursiveAdder
    {
        private readonly IScriptProcessingContext _context;

        public RecursiveAdder(IScriptProcessingContext context)
        {
            _context = context;
        }

        public async Task<RecursiveAddResult> Add(Identifier parentId, ConstantPathSubjectPart part, IEditableEntry newEntry, ExecutionScope scope)
        {
            var parentIdToSelect = parentId;
            var outputObservable = Observable.Create<object>(outputObserver =>
            {
                _context.Logical.Nodes.SelectMany(GraphPath.Create(parentIdToSelect, GraphRelation.Children, new GraphNode(part.Name)), scope, outputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            var childrenWithSameName = await outputObservable
                .Cast<IReadOnlyEntry>()
                .ToArray();
            var childWithSameName = childrenWithSameName.FirstOrDefault();
            if (childWithSameName != null)
            {
                if (childrenWithSameName.Multiple())
                {
                    var message = $"Found multiple children with the same name: {part.Name}";
                    throw new ScriptProcessingException(message);
                }
                parentId = childWithSameName.Id;
            }
            else
            {
                newEntry = (IEditableEntry)await _context.Logical.Nodes.Add(parentId, part.Name, scope).ConfigureAwait(false);
                parentId = newEntry.Id;
            }

            return new RecursiveAddResult(parentId, newEntry);
        }
    }
}
