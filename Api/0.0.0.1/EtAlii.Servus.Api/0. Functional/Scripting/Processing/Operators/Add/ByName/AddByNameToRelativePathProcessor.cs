namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    internal class AddByNameToRelativePathProcessor : IAddByNameToRelativePathProcessor
    {
        private readonly IItemToPathSubjectConverter _itemToPathSubjectConverter;
        private readonly IItemToIdentifierConverter _itemToIdentifierConverter;
        private readonly IRecursiveAdder _recursiveAdder;

        public AddByNameToRelativePathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IItemToIdentifierConverter itemToIdentifierConverter,
            IRecursiveAdder recursiveAdder)
        {
            _itemToPathSubjectConverter = itemToPathSubjectConverter;
            _itemToIdentifierConverter = itemToIdentifierConverter;
            _recursiveAdder = recursiveAdder;
        }

        public void Process(OperatorParameters parameters)
        {
            PathSubject pathToAdd = null;

            pathToAdd = GetPathToAdd(parameters);
            if (pathToAdd == null)
            {
                throw new ScriptProcessingException("The AddByNameToRelativePathProcessor requires a path on the right side");
            }

            if (!pathToAdd.Parts.All(part => part is ConstantPathSubjectPart || part is IsParentOfPathSubjectPart))
            {
                throw new ScriptProcessingException("The AddByNameToRelativePathProcessor requires a constant, hierarchical path");
            }

            if (pathToAdd.Parts.Any(part => part is ConstantPathSubjectPart && String.IsNullOrWhiteSpace(((ConstantPathSubjectPart)part).Name)))
            {
                throw new ScriptProcessingException("The AddByNameToRelativePathProcessor cannot handle empty parts");
            }

            parameters.LeftInput.Subscribe(
                onError: parameters.Output.OnError,
                onCompleted: parameters.Output.OnCompleted,
                onNext: o =>
                {
                    var task2 = Task.Run(async () =>
                        {
                        var leftId = await _itemToIdentifierConverter.Convert(o, parameters.Scope);
                        await Add(leftId, pathToAdd, parameters.Scope, parameters.Output);
                    });
                    task2.Wait();
                });
        }

        private PathSubject GetPathToAdd(OperatorParameters parameters)
        {
            PathSubject pathToAdd = null;

            var task = Task.Run(async () =>
            {
                if (parameters.RightSubject is PathSubject)
                {
                    pathToAdd = (PathSubject)parameters.RightSubject;
                }
                else
                {
                    var rightResult = await parameters.RightInput.SingleOrDefaultAsync();

                    _itemToPathSubjectConverter.TryConvert(rightResult, out pathToAdd);
                }
            });
            task.Wait();

            return pathToAdd;
        }

        private async Task Add(Identifier id, PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var parentId = id;
            IEditableEntry newEntry = null;
            foreach (var partToAdd in pathToAdd.Parts.OfType<ConstantPathSubjectPart>())
            {
                var addResult = await _recursiveAdder.Add(parentId, partToAdd, newEntry, scope);
                parentId = addResult.ParentId;
                newEntry = addResult.NewEntry;
            }
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
        }
    }
}
