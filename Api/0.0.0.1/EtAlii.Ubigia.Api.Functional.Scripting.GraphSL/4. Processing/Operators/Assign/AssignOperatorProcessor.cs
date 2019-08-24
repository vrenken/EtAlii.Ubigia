﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
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
            ISelector<Subject, Subject, IAssignOperatorSubProcessor> selector = new Selector2<Subject, Subject, IAssignOperatorSubProcessor>();
            
            selector = RegisterOutputSubProcessors(selector, assignPathToOutputOperatorSubProcessor, assignFunctionToOutputOperatorSubProcessor, assignConstantToOutputOperatorSubProcessor, assignVariableToOutputOperatorSubProcessor, assignCombinedToOutputOperatorSubProcessor, assignRootToOutputOperatorSubProcessor, assignTagToOutputOperatorSubProcessor, assignEmptyToOutputOperatorSubProcessor); 
            selector = RegisterVariableSubProcessors(selector, assignPathToVariableOperatorSubProcessor, assignFunctionToVariableOperatorSubProcessor, assignConstantToVariableOperatorSubProcessor, assignVariableToVariableOperatorSubProcessor, assignCombinedToVariableOperatorSubProcessor, assignTagToVariableOperatorSubProcessor, assignEmptyToVariableOperatorSubProcessor); 
            selector = RegisterPathSubProcessors(selector, assignVariableToPathOperatorSubProcessor, assignPathToPathOperatorSubProcessor, assignFunctionToPathOperatorSubProcessor, assignConstantToPathOperatorSubProcessor, assignCombinedToPathOperatorSubProcessor, assignEmptyToPathOperatorSubProcessor);
            selector = RegisterFunctionSubProcessors(selector, assignPathToFunctionOperatorSubProcessor, assignFunctionToFunctionOperatorSubProcessor, assignConstantToFunctionOperatorSubProcessor, assignVariableToFunctionOperatorSubProcessor, assignCombinedToFunctionOperatorSubProcessor, assignTagToFunctionOperatorSubProcessor, assignEmptyToFunctionOperatorSubProcessor);
            selector = RegisterRootSubProcessors(selector, assignRootDefinitionToRootOperatorSubProcessor, assignStringConstantToRootOperatorSubProcessor, assignEmptyToRootOperatorSubProcessor);
            _selector = selector;
        }

        private ISelector<Subject, Subject, IAssignOperatorSubProcessor> RegisterOutputSubProcessors(
            ISelector<Subject, Subject, IAssignOperatorSubProcessor> selector,
            IAssignPathToOutputOperatorSubProcessor assignPathToOutputOperatorSubProcessor, 
            IAssignFunctionToOutputOperatorSubProcessor assignFunctionToOutputOperatorSubProcessor, 
            IAssignConstantToOutputOperatorSubProcessor assignConstantToOutputOperatorSubProcessor, 
            IAssignVariableToOutputOperatorSubProcessor assignVariableToOutputOperatorSubProcessor,
            IAssignCombinedToOutputOperatorSubProcessor assignCombinedToOutputOperatorSubProcessor,
            IAssignRootToOutputOperatorSubProcessor assignRootToOutputOperatorSubProcessor,
            IAssignTagToOutputOperatorSubProcessor assignTagToOutputOperatorSubProcessor,
            IAssignEmptyToOutputOperatorSubProcessor assignEmptyToOutputOperatorSubProcessor)
        {
            return selector.Register((l, r) => l is EmptySubject && r is PathSubject pathSubject && pathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart && string.IsNullOrEmpty(taggedPathSubjectPart.Tag), assignTagToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is PathSubject, assignPathToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is FunctionSubject, assignFunctionToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is ConstantSubject, assignConstantToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is VariableSubject, assignVariableToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is CombinedSubject, assignCombinedToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is CombinedSubject, assignRootToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is EmptySubject, assignEmptyToOutputOperatorSubProcessor);
        }

        private ISelector<Subject, Subject, IAssignOperatorSubProcessor> RegisterVariableSubProcessors(
            ISelector<Subject, Subject, IAssignOperatorSubProcessor> selector,
            IAssignPathToVariableOperatorSubProcessor assignPathToVariableOperatorSubProcessor, 
            IAssignFunctionToVariableOperatorSubProcessor assignFunctionToVariableOperatorSubProcessor, 
            IAssignConstantToVariableOperatorSubProcessor assignConstantToVariableOperatorSubProcessor,
            IAssignVariableToVariableOperatorSubProcessor assignVariableToVariableOperatorSubProcessor,
            IAssignCombinedToVariableOperatorSubProcessor assignCombinedToVariableOperatorSubProcessor,
            IAssignTagToVariableOperatorSubProcessor assignTagToVariableOperatorSubProcessor,
            IAssignEmptyToVariableOperatorSubProcessor assignEmptyToVariableOperatorSubProcessor)
        {
            return selector.Register((l, r) => l is VariableSubject && r is PathSubject pathSubject && pathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart && string.IsNullOrEmpty(taggedPathSubjectPart.Tag), assignTagToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is PathSubject, assignPathToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is FunctionSubject, assignFunctionToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is ConstantSubject, assignConstantToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is VariableSubject, assignVariableToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is CombinedSubject, assignCombinedToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is EmptySubject, assignEmptyToVariableOperatorSubProcessor);
        }

        private ISelector<Subject, Subject, IAssignOperatorSubProcessor> RegisterPathSubProcessors(
            ISelector<Subject, Subject, IAssignOperatorSubProcessor> selector,
            IAssignVariableToPathOperatorSubProcessor assignVariableToPathOperatorSubProcessor,
            IAssignPathToPathOperatorSubProcessor assignPathToPathOperatorSubProcessor, 
            IAssignFunctionToPathOperatorSubProcessor assignFunctionToPathOperatorSubProcessor,
            IAssignConstantToPathOperatorSubProcessor assignConstantToPathOperatorSubProcessor,
            IAssignCombinedToPathOperatorSubProcessor assignCombinedToPathOperatorSubProcessor,
            IAssignEmptyToPathOperatorSubProcessor assignEmptyToPathOperatorSubProcessor)
        {
            return selector.Register((l, r) => l is PathSubject && r is PathSubject, assignPathToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is FunctionSubject, assignFunctionToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is ConstantSubject, assignConstantToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is VariableSubject, assignVariableToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is CombinedSubject, assignCombinedToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is EmptySubject, assignEmptyToPathOperatorSubProcessor);
        }

        private ISelector<Subject, Subject, IAssignOperatorSubProcessor> RegisterFunctionSubProcessors(
            ISelector<Subject, Subject, IAssignOperatorSubProcessor> selector,
            IAssignPathToFunctionOperatorSubProcessor assignPathToFunctionOperatorSubProcessor, 
            IAssignFunctionToFunctionOperatorSubProcessor assignFunctionToFunctionOperatorSubProcessor, 
            IAssignConstantToFunctionOperatorSubProcessor assignConstantToFunctionOperatorSubProcessor,
            IAssignVariableToFunctionOperatorSubProcessor assignVariableToFunctionOperatorSubProcessor,
            IAssignCombinedToFunctionOperatorSubProcessor assignCombinedToFunctionOperatorSubProcessor,
            IAssignTagToFunctionOperatorSubProcessor assignTagToFunctionOperatorSubProcessor,
            IAssignEmptyToFunctionOperatorSubProcessor assignEmptyToFunctionOperatorSubProcessor)
        {
            return selector.Register((l, r) => l is FunctionSubject && r is PathSubject pathSubject && pathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart && string.IsNullOrEmpty(taggedPathSubjectPart.Tag), assignTagToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is PathSubject, assignPathToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is FunctionSubject, assignFunctionToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is ConstantSubject, assignConstantToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is VariableSubject, assignVariableToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is CombinedSubject, assignCombinedToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is EmptySubject, assignEmptyToFunctionOperatorSubProcessor);
        }

        private ISelector<Subject, Subject, IAssignOperatorSubProcessor> RegisterRootSubProcessors(
            ISelector<Subject, Subject, IAssignOperatorSubProcessor> selector,
            IAssignRootDefinitionToRootOperatorSubProcessor assignRootDefinitionToRootOperatorSubProcessor,
            IAssignStringConstantToRootOperatorSubProcessor assignStringConstantToRootOperatorSubProcessor,
            IAssignEmptyToRootOperatorSubProcessor assignEmptyToRootOperatorSubProcessor)
        {
            return selector.Register((l, r) => l is RootSubject && r is RootDefinitionSubject, assignRootDefinitionToRootOperatorSubProcessor)
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
            await assigner.Assign(parameters);
        }
    }
}
