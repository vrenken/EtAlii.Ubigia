namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddVariableToExistingPathProcessor : IAddVariableToExistingPathProcessor
    {
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IScriptProcessingContext _context;

        public AddVariableToExistingPathProcessor(
            IScriptProcessingContext context,
            IItemToIdentifierConverter itemToIdentifierConverter)
        {
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _context = context;
        }


        public async Task Process(OperatorParameters parameters)
        {
            var rightResult = await parameters.RightInput.SingleAsync(); // We only want to add one item at a time.

            parameters.LeftInput.SubscribeAsync(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: async o =>
                {
                    var leftIdentifier = _itemToIdentifierConverter.Convert(o);

                    switch (rightResult)
                    {
                        case Identifier identifier:
                            await AddFromIdentifier(leftIdentifier, identifier, parameters).ConfigureAwait(false);
                            break;
                        case IReadOnlyEntry entry:
                            await AddFromIdentifier(leftIdentifier, entry.Id, parameters).ConfigureAwait(false);
                            break;
                        case INode node:
                            await AddFromIdentifier(leftIdentifier, node.Id, parameters).ConfigureAwait(false);
                            break;
                        case string s:
                            await AddFromString(leftIdentifier, s, parameters).ConfigureAwait(false);
                            break;
                        default:
                            throw new ScriptProcessingException($"The {GetType().Name} requires a identifier or string to add");
                    }
                });
        }

        private async Task AddFromString(Identifier leftIdentifier, string s, OperatorParameters parameters)
        {
            await _context.Logical.Nodes.Add(leftIdentifier, s, parameters.Scope).ConfigureAwait(false);
        }

        private async Task AddFromIdentifier(Identifier leftIdentifier, Identifier identifierToAdd, OperatorParameters parameters)
        {
            var outputObservable = Observable.Create<object>(observer =>
            {
                _context.Logical.Nodes.SelectMany(GraphPath.Create(identifierToAdd), parameters.Scope, observer);
                return Disposable.Empty;
            });
            var entry = await outputObservable.SingleAsync();

            var latestIdentifierToAdd = ((IEditableEntry)entry).Id;

            var newEntry = await _context.Logical.Nodes.Add(leftIdentifier, latestIdentifierToAdd, parameters.Scope).ConfigureAwait(false);
            var result = new DynamicNode(newEntry);
            parameters.Output.OnNext(result);
        }
    }
}
