namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AddByNameToAbsolutePathProcessor : IAddByNameToAbsolutePathProcessor
    {
        private readonly IItemToPathSubjectConverter _itemToPathSubjectConverter;
        private readonly IRecursiveAdder _recursiveAdder;
        private readonly IProcessingContext _context;

        public AddByNameToAbsolutePathProcessor(
            IItemToPathSubjectConverter itemToPathSubjectConverter,
            IRecursiveAdder recursiveAdder,
            IProcessingContext context)
        {
            _itemToPathSubjectConverter = itemToPathSubjectConverter;
            _recursiveAdder = recursiveAdder;
            _context = context;
        }

        public void Process(OperatorParameters parameters)
        {
            PathSubject pathToAdd = null;

            pathToAdd = GetPathToAdd(parameters);
            if (pathToAdd == null)
            {
                throw new ScriptProcessingException("The AddByNameToAbsolutePathProcessor requires a path on the right side");
            }

            if (!pathToAdd.Parts.All(part => part is ConstantPathSubjectPart || part is IsParentOfPathSubjectPart))
            {
                throw new ScriptProcessingException("The AddByNameToAbsolutePathProcessor requires a constant, hierarchical path");
            }

            if (pathToAdd.Parts.Any(part => part is ConstantPathSubjectPart && String.IsNullOrWhiteSpace(((ConstantPathSubjectPart)part).Name)))
            {
                throw new ScriptProcessingException("The AddByNameToAbsolutePathProcessor cannot handle empty parts");
            }

            var task = Task.Run(async () =>
            {
                await Add(pathToAdd, parameters.Scope, parameters.Output);
            });
            task.Wait();
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

        private async Task Add(PathSubject pathToAdd, ExecutionScope scope, IObserver<object> output)
        {
            var partsToAdd = pathToAdd.Parts
                .OfType<ConstantPathSubjectPart>()
                .ToArray();

            var rootPartToAdd = partsToAdd.First();
            var root = await _context.Logical.Roots.Get(rootPartToAdd.Name);

            if (root == null)
            {
                throw new ScriptProcessingException("The AddByNameToAbsolutePathProcessor requires an absolute path to contain a valid root");
            }

            var parentId = root.Identifier;
            IEditableEntry newEntry = null;
            foreach (var partToAdd in partsToAdd.Skip(1))
            {
                var addResult = await _recursiveAdder.Add(parentId, partToAdd, newEntry, scope);
                parentId = addResult.ParentId;
                newEntry = addResult.NewEntry;
            }
            var result = new DynamicNode((IReadOnlyEntry)newEntry);
            output.OnNext(result);
            output.OnCompleted();
        }
    }
}
