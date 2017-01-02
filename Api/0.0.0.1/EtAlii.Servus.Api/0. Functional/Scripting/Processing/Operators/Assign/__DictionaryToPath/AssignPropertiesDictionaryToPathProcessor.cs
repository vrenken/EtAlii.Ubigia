//namespace EtAlii.Servus.Api.Functional
//{
//    using System.Linq;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    internal class AssignPropertiesDictionaryToPathProcessor : IAssignDictionaryToPathProcessor
//    {
//        private readonly IProcessingContext _context;
//        private readonly IUpdateEntryFactory _updateEntryFactory;

//        private readonly INodeToPathInputConverterSelector _inputConverterSelector;

//        public AssignPropertiesDictionaryToPathProcessor(
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

//                    var values = (IPropertyDictionary)o;
//                    var result = await Assign(values, entry, parameters.Scope);
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
//            //        throw new ScriptProcessingException("The AssignPropertiesDictionaryToPathProcessor requires queryable entries from the previous path part");
//            //    }

//            //    var values = (IPropertyDictionary)o;
//            //    foreach (var entry in entries)
//            //    {
//            //        var result = await Assign(values, entry, parameters.Scope);
//            //        parameters.Output.OnNext(result);
//            //    }
//            //    parameters.Output.OnCompleted();
//            //});
//            //task.Wait();
//        }

//        private async Task<INode> Assign(IPropertyDictionary newProperties, IReadOnlyEntry entry, ExecutionScope scope)
//        {
//            var oldProperties = await _context.Logical.Properties.Get(entry.Id, scope) ?? new PropertyDictionary();

//            if (oldProperties.CompareTo(newProperties) == 0)
//            {
//                // The two propertydictionaries are the same. Let's return the old node.
//                return (IInternalNode)new DynamicNode((IReadOnlyEntry)entry, oldProperties);
//            }
//            else
//            {
//                var newEntry = await _updateEntryFactory.Create(entry, scope);

//                // We want to do an addition, so lets combine the old with the new properties.
//                var properties = new PropertyDictionary(oldProperties);

//                // Lets remove all properties that should be set to null.
//                foreach (var newProperty in newProperties)
//                {
//                    if (newProperty.Value == null)
//                    {
//                        properties.Remove(newProperty.Key);
//                    }
//                    else
//                    {
//                        properties[newProperty.Key] = newProperty.Value;
//                    }
//                }

//                await _context.Logical.Properties.Set(newEntry.Id, properties, scope);

//                var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry, properties);
//                return newNode;
//            }
//        }
//    }
//}
