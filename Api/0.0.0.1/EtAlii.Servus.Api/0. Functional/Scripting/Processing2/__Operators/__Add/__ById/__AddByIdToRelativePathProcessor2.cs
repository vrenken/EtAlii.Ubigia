//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    internal class AddByIdToRelativePathProcessor2 : IAddByIdToRelativePathProcessor2
//    {
//        private readonly IItemToIdentifierConverter2 _itemToIdentifierConverter;
//        private readonly IProcessingContext _context;

//        public AddByIdToRelativePathProcessor2(
//            IProcessingContext context,
//            IItemToIdentifierConverter2 itemToIdentifierConverter)
//        {
//            _itemToIdentifierConverter = itemToIdentifierConverter;
//            _context = context;
//        }


//        public void Process(OperatorParameters parameters)
//        {
//            var rightId = Identifier.Empty;

//            var task = Task.Run(async () =>
//            {
//                var rightResult = await parameters.RightInput.SingleAsync();
//                rightId = await _itemToIdentifierConverter.Convert(rightResult, parameters.Scope);
//                if (rightId == Identifier.Empty)
//                {
//                    throw new ScriptProcessingException("The AddByIdToRelativePathProcessor requires a identifier to add");
//                }
//            });
//            task.Wait();

//            parameters.LeftInput.Subscribe(
//                onError: parameters.Output.OnError,
//                onCompleted: parameters.Output.OnCompleted,
//                onNext: o =>
//                {
//                    var task2 = Task.Run(async () =>
//                    {
//                        var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
//                        await Add(leftId, rightId, parameters.Scope, parameters.Output);
//                    });
//                    task2.Wait();
//                });

//                //if (leftIds == null || !leftIds.Any())
//                //{
//                //    throw new ScriptProcessingException("The AddByIdToRelativePathProcessor requires queryable ids from the previous path part");
//                //}
//        }

//        private async Task Add(
//            Identifier id, 
//            Identifier identifierToAdd, 
//            ExecutionScope scope,
//            IObserver<object> output)
//        {
//            var newEntry = await _context.Logical.Nodes.Add(id, identifierToAdd, scope); 
//            var result = new DynamicNode((IReadOnlyEntry)newEntry);
//            output.OnNext(result);
//        }
//    }
//}
