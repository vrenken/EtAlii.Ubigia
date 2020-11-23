namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Collections;

    internal class RecursiveRemover : IRecursiveRemover
    {
        private readonly IScriptProcessingContext _context;

        public RecursiveRemover(IScriptProcessingContext context)
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
                _context.Logical.Nodes.SelectMany(GraphPath.Create(parentId, GraphRelation.Children, new GraphNode(part.Name)), scope, outputObserver);

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
                    var message = $"Found multiple children with the same name: {part.Name}";
                    throw new ScriptProcessingException(message);
                }
                newEntry = (IEditableEntry)await _context.Logical.Nodes.Remove(parentId, part.Name, scope).ConfigureAwait(false);
                parentId = newEntry.Id;
            }
            else
            {
                parentId = Identifier.Empty;
            }

            return new RecursiveRemoveResult(newEntry); //(parentId, newEntry)
        }
    }
}