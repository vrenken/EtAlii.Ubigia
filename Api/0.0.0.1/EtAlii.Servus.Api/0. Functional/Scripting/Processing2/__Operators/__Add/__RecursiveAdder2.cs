//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;
//    using EtAlii.Servus.Api.Logical;
//    using EtAlii.xTechnology.Collections;

//    internal class RecursiveAdder2 : IRecursiveAdder2
//    {
//        private readonly IProcessingContext _context;

//        public RecursiveAdder2(IProcessingContext context)
//        {
//            _context = context;
//        }

//        public async Task<RecursiveAddResult2> Add(Identifier parentId, ConstantPathSubjectPart part, IEditableEntry newEntry, ExecutionScope scope)
//        {
//            var outputObservable = Observable.Create<object>(outputObserver =>
//            {
//                _context.Logical.Nodes.SelectMany(GraphPath.Create(parentId, GraphRelation.Child, new GraphNode(part.Name)), scope, outputObserver);

//                return Disposable.Empty;
//            });

//            var childrenWithSameName = outputObservable
//                .ToEnumerable()
//                .Cast<IReadOnlyEntry>()
//                .ToArray();
//            var childWithSameName = childrenWithSameName.FirstOrDefault();
//            if (childWithSameName != null)
//            {
//                if (childrenWithSameName.Multiple())
//                {
//                    var message = String.Format("Found multiple children with the same name: {0}", part.Name);
//                    throw new ScriptProcessingException(message);
//                }
//                parentId = childWithSameName.Id;
//            }
//            else
//            {
//                newEntry = (IEditableEntry)await _context.Logical.Nodes.Add(parentId, part.Name, scope);
//                parentId = newEntry.Id;
//            }

//            return new RecursiveAddResult2(parentId, newEntry);
//        }
//    }
//}
