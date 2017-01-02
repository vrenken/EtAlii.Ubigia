//namespace EtAlii.Servus.Api.Functional
//{
//    using System.Linq;
//    using System.Reactive.Linq;
//    using System.Reflection;
//    using System.Threading.Tasks;

//    internal class AssignDynamicObjectToPathProcessor : IAssignDynamicObjectToPathProcessor
//    {
//        private readonly IProcessingContext _context;
//        private readonly IUpdateEntryFactory _updateEntryFactory;
//        private readonly IDynamicObjectToPathInputConverterSelector _inputConverterSelector;

//        public AssignDynamicObjectToPathProcessor(
//            IProcessingContext context,
//            IUpdateEntryFactory updateEntryFactory,
//            IDynamicObjectToPathInputConverterSelector inputConverterSelector)
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

//                    var dynamicObject = o;
//                    var result = await Assign(dynamicObject, entry, parameters.Scope);
//                    parameters.Output.OnNext(result);
//                }
//            });
//            task.Wait();

//            //var task = Task.Run(async () =>
//            //{

//            //    var leftResult = parameters.LeftInput.ToEnumerable();
//            //    var inputConvertor = _inputConverterSelector.Select(leftResult);
//            //    var entries = await inputConvertor(leftResult, parameters.Scope);
//            //    if (entries == null || !entries.Any())
//            //    {
//            //        throw new ScriptProcessingException("The AssignDynamicObjectToPathProcessor requires queryable entries from the previous path part");
//            //    }

//            //    var dynamicObject = o;
//            //    foreach (var entry in entries)
//            //    {
//            //        var result = await Assign(dynamicObject, entry, parameters.Scope);
//            //        parameters.Output.OnNext(result);
//            //    }
//            //    parameters.Output.OnCompleted();
//            //});
//            //task.Wait();
//        }

//        private async Task<INode> Assign(object dynamicObject, IReadOnlyEntry entry, ExecutionScope scope)
//        {
//            var newEntry = await _updateEntryFactory.Create(entry, scope);

//            var properties = ToDictionary(dynamicObject);
//            await _context.Logical.Properties.Set(newEntry.Id, properties, scope);

//            var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry, properties);
//            return newNode;
//        }

//        private PropertyDictionary ToDictionary(object data)
//        {
//            var result = new PropertyDictionary();

//            var runtimeProperties = data
//                .GetType()
//                .GetRuntimeProperties()
//                .Where(p => p.CanRead);

//            foreach (var runtimeProperty in runtimeProperties)
//            {
//                var propertyName = runtimeProperty.Name;
//                var propertyValue = runtimeProperty.GetValue(data, null);
//                result[propertyName] = propertyValue;
//            }
//            return result;
//        }
//    }
//}
