//namespace EtAlii.Servus.Api.Functional
//{
//    using System.Linq;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    internal class AssignNodeToPathProcessor : IAssignNodeToPathProcessor
//    {
//        private readonly IProcessingContext _context;
//        private readonly IUpdateEntryFactory _updateEntryFactory;

//        private readonly INodeToPathInputConverterSelector _inputConverterSelector;

//        public AssignNodeToPathProcessor(
//            IProcessingContext context,
//            IUpdateEntryFactory updateEntryFactory,
//            INodeToPathInputConverterSelector inputConverterSelector)
//        {
//            _context = context;
//            _updateEntryFactory = updateEntryFactory;
//            _inputConverterSelector = inputConverterSelector;
//        }

//        public void Assign(OperatorParameters parameters, object o)
//        {
//            var leftResults = parameters.LeftInput
//                .ToEnumerable()
//                .ToArray();

//            var task = Task.Run(async () =>
//            {
//                foreach (var leftResult in leftResults)
//                {
//                    var inputConvertor = _inputConverterSelector.Select(leftResult);
//                    var entry = await inputConvertor(leftResult, parameters.Scope);
//                    var nodeToAssign = (IInternalNode)o;
//                    var result = await Assign(nodeToAssign, entry, parameters.Scope);
//                    parameters.Output.OnNext(result);
//                }
//            });
//            task.Wait();

//            //var leftResult = parameters.LeftInput.ToEnumerable();
//            //var inputConvertor = _inputConverterSelector.Select(leftResult);
//            //var entries = await inputConvertor(leftResult, parameters.Scope);
//            //if (entries == null || !entries.Any())
//            //{
//            //    throw new ScriptProcessingException("The AssignNodeToPathProcessor requires queryable entries from the previous path part");
//            //}

//            //var nodeToAssign = (IInternalNode) o;
//            //foreach (var entry in entries)
//            //{
//            //    var result = await Assign(nodeToAssign, entry, parameters.Scope);
//            //    parameters.Output.OnNext(result);
//            //}
//            //parameters.Output.OnCompleted();
//        }

//        private async Task<INode> Assign(IInternalNode nodeToAssign, IReadOnlyEntry entry, ExecutionScope scope)
//        {
//            var newEntry = await _updateEntryFactory.Create(entry, scope);
//            var properties = nodeToAssign.GetProperties();

//            await _context.Logical.Properties.Set(newEntry.Id, properties, scope);

//            var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry, properties);
//            return newNode;
//        }
//    }
//}
