namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Collections;

    internal class RecursiveAdder : IRecursiveAdder
    {
        private readonly IProcessingContext _context;

        public RecursiveAdder(IProcessingContext context)
        {
            _context = context;
        }

        public async Task<RecursiveAddResult> Add(Identifier parentId, ConstantPathSubjectPart part, IEditableEntry newEntry, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(outputObserver =>
            {
                _context.Logical.Nodes.SelectMany(GraphPath.Create(parentId, GraphRelation.Children, new GraphNode(part.Name)), scope, outputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            var childrenWithSameName = outputObservable
                .ToEnumerable()
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
                newEntry = (IEditableEntry)await _context.Logical.Nodes.Add(parentId, part.Name, scope);
                parentId = newEntry.Id;
            }

            return new RecursiveAddResult(parentId, newEntry);
        }
    }
}
