namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Collections;

    internal class RecursiveRemover : IRecursiveRemover
    {
        private readonly IProcessingContext _context;

        public RecursiveRemover(IProcessingContext context)
        {
            _context = context;
        }

        public async Task<RecursiveRemoveResult> Remove(
            Identifier parentId,
            ConstantPathSubjectPart part,
            ExecutionScope scope)
        {
            IEditableEntry newEntry = null;

            var outputObservable = Observable.Create<object>(outputObserver =>
            {
                _context.Logical.Nodes.SelectMany(GraphPath.Create(parentId, GraphRelation.Child, new GraphNode(part.Name)), scope, outputObserver);

                return Disposable.Empty;
            });
            var childrenWithSameName = outputObservable
                .ToEnumerable()
                .ToArray();
            var childWithSameName = childrenWithSameName.FirstOrDefault();
            if (childWithSameName != null)
            {
                if (childrenWithSameName.Multiple())
                {
                    var message = String.Format("Found multiple children with the same name: {0}", part.Name);
                    throw new ScriptProcessingException(message);
                }
                newEntry = (IEditableEntry)await _context.Logical.Nodes.Remove(parentId, part.Name, scope);
                parentId = newEntry.Id;
            }
            else
            {
                parentId = Identifier.Empty;
            }
            return new RecursiveRemoveResult(parentId, newEntry);
        }
    }
}