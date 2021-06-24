// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    internal class AssignOperatorProcessor : IAssignOperatorProcessor
    {
        private readonly ISelector<Subject, Subject, IAssignOperatorSubProcessor> _selector;

        public AssignOperatorProcessor(
            //IProcessingContext processingContext,
            IAssignPathToVariableOperatorSubProcessor assignPathToVariableOperatorSubProcessor,
            IAssignFunctionToVariableOperatorSubProcessor assignFunctionToVariableOperatorSubProcessor,
            IAssignConstantToVariableOperatorSubProcessor assignConstantToVariableOperatorSubProcessor,
            IAssignVariableToVariableOperatorSubProcessor assignVariableToVariableOperatorSubProcessor,
            IAssignCombinedToVariableOperatorSubProcessor assignCombinedToVariableOperatorSubProcessor,
            IAssignTagToVariableOperatorSubProcessor assignTagToVariableOperatorSubProcessor,
            IAssignEmptyToVariableOperatorSubProcessor assignEmptyToVariableOperatorSubProcessor,

            IAssignVariableToPathOperatorSubProcessor assignVariableToPathOperatorSubProcessor,
            IAssignPathToPathOperatorSubProcessor assignPathToPathOperatorSubProcessor,
            IAssignFunctionToPathOperatorSubProcessor assignFunctionToPathOperatorSubProcessor,
            IAssignConstantToPathOperatorSubProcessor assignConstantToPathOperatorSubProcessor,
            IAssignCombinedToPathOperatorSubProcessor assignCombinedToPathOperatorSubProcessor,
            IAssignEmptyToPathOperatorSubProcessor assignEmptyToPathOperatorSubProcessor,

            IAssignPathToFunctionOperatorSubProcessor assignPathToFunctionOperatorSubProcessor,
            IAssignFunctionToFunctionOperatorSubProcessor assignFunctionToFunctionOperatorSubProcessor,
            IAssignConstantToFunctionOperatorSubProcessor assignConstantToFunctionOperatorSubProcessor,
            IAssignVariableToFunctionOperatorSubProcessor assignVariableToFunctionOperatorSubProcessor,
            IAssignCombinedToFunctionOperatorSubProcessor assignCombinedToFunctionOperatorSubProcessor,
            IAssignTagToFunctionOperatorSubProcessor assignTagToFunctionOperatorSubProcessor,
            IAssignEmptyToFunctionOperatorSubProcessor assignEmptyToFunctionOperatorSubProcessor,

            IAssignPathToOutputOperatorSubProcessor assignPathToOutputOperatorSubProcessor,
            IAssignFunctionToOutputOperatorSubProcessor assignFunctionToOutputOperatorSubProcessor,
            IAssignConstantToOutputOperatorSubProcessor assignConstantToOutputOperatorSubProcessor,
            IAssignVariableToOutputOperatorSubProcessor assignVariableToOutputOperatorSubProcessor,
            IAssignCombinedToOutputOperatorSubProcessor assignCombinedToOutputOperatorSubProcessor,
            IAssignRootToOutputOperatorSubProcessor assignRootToOutputOperatorSubProcessor,
            IAssignTagToOutputOperatorSubProcessor assignTagToOutputOperatorSubProcessor,
            IAssignEmptyToOutputOperatorSubProcessor assignEmptyToOutputOperatorSubProcessor,

            IAssignRootDefinitionToRootOperatorSubProcessor assignRootDefinitionToRootOperatorSubProcessor,
            IAssignStringConstantToRootOperatorSubProcessor assignStringConstantToRootOperatorSubProcessor,
            IAssignEmptyToRootOperatorSubProcessor assignEmptyToRootOperatorSubProcessor)
        {
            _selector = new Selector2<Subject, Subject, IAssignOperatorSubProcessor>()

            //RegisterOutputSubProcessors
                .Register((l, r) => l is EmptySubject && r is PathSubject pathSubject && pathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart && string.IsNullOrEmpty(taggedPathSubjectPart.Tag), assignTagToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is PathSubject, assignPathToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is FunctionSubject, assignFunctionToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is ConstantSubject, assignConstantToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is VariableSubject, assignVariableToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is CombinedSubject, assignCombinedToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is CombinedSubject, assignRootToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is EmptySubject, assignEmptyToOutputOperatorSubProcessor)

            //RegisterVariableSubProcessors
                .Register((l, r) => l is VariableSubject && r is PathSubject pathSubject && pathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart && string.IsNullOrEmpty(taggedPathSubjectPart.Tag), assignTagToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is PathSubject, assignPathToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is FunctionSubject, assignFunctionToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is ConstantSubject, assignConstantToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is VariableSubject, assignVariableToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is CombinedSubject, assignCombinedToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is EmptySubject, assignEmptyToVariableOperatorSubProcessor)

            //RegisterPathSubProcessors
                .Register((l, r) => l is PathSubject && r is PathSubject, assignPathToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is FunctionSubject, assignFunctionToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is ConstantSubject, assignConstantToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is VariableSubject, assignVariableToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is CombinedSubject, assignCombinedToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is EmptySubject, assignEmptyToPathOperatorSubProcessor)

            //RegisterFunctionSubProcessors
                .Register((l, r) => l is FunctionSubject && r is PathSubject pathSubject && pathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart && string.IsNullOrEmpty(taggedPathSubjectPart.Tag), assignTagToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is PathSubject, assignPathToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is FunctionSubject, assignFunctionToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is ConstantSubject, assignConstantToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is VariableSubject, assignVariableToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is CombinedSubject, assignCombinedToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is EmptySubject, assignEmptyToFunctionOperatorSubProcessor)

            //RegisterRootSubProcessors
                .Register((l, r) => l is RootSubject && r is RootDefinitionSubject, assignRootDefinitionToRootOperatorSubProcessor)
                .Register((l, r) => l is RootSubject && r is StringConstantSubject, assignStringConstantToRootOperatorSubProcessor)
                .Register((l, r) => l is RootSubject && r is EmptySubject, assignEmptyToRootOperatorSubProcessor);
        }

        public async Task Process(OperatorParameters parameters)
        {
            var assigner = _selector.TrySelect(parameters.LeftSubject, parameters.RightSubject);
            if (assigner == null)
            {
                var left = parameters.LeftSubject == null ? "NULL" : parameters.LeftSubject.ToString();
                var right = parameters.RightSubject == null ? "NULL" : parameters.RightSubject.ToString();
                var message =
                    $"No supported mapping found for the AssignOperatorProcessor to work with (left: {left}, right: {right})";
                throw new ScriptProcessingException(message);
            }
            await assigner.Assign(parameters).ConfigureAwait(false);
        }
    }
}
