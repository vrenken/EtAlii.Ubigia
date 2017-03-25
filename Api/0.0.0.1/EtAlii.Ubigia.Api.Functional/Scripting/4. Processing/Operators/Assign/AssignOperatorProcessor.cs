﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.xTechnology.Structure;

    internal class AssignOperatorProcessor : IAssignOperatorProcessor
    {
        private readonly ISelector<Subject, Subject, IAssignOperatorSubProcessor> _selector;

        public AssignOperatorProcessor(
            IAssignPathToVariableOperatorSubProcessor assignPathToVariableOperatorSubProcessor, 
            IAssignFunctionToVariableOperatorSubProcessor assignFunctionToVariableOperatorSubProcessor, 
            IAssignConstantToVariableOperatorSubProcessor assignConstantToVariableOperatorSubProcessor,
            IAssignVariableToVariableOperatorSubProcessor assignVariableToVariableOperatorSubProcessor,
            IAssignCombinedToVariableOperatorSubProcessor assignCombinedToVariableOperatorSubProcessor,
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
            IAssignEmptyToFunctionOperatorSubProcessor assignEmptyToFunctionOperatorSubProcessor,
            IAssignPathToOutputOperatorSubProcessor assignPathToOutputOperatorSubProcessor, 
            IAssignFunctionToOutputOperatorSubProcessor assignFunctionToOutputOperatorSubProcessor, 
            IAssignConstantToOutputOperatorSubProcessor assignConstantToOutputOperatorSubProcessor, 
            IAssignVariableToOutputOperatorSubProcessor assignVariableToOutputOperatorSubProcessor,
            IAssignCombinedToOutputOperatorSubProcessor assignCombinedToOutputOperatorSubProcessor,
            IAssignRootToOutputOperatorSubProcessor assignRootToOutputOperatorSubProcessor,
            IAssignEmptyToOutputOperatorSubProcessor assignEmptyToOutputOperatorSubProcessor,
            IAssignRootDefinitionToRootOperatorSubProcessor assignRootDefinitionToRootOperatorSubProcessor,
            IAssignStringConstantToRootOperatorSubProcessor assignStringConstantToRootOperatorSubProcessor,
            IAssignEmptyToRootOperatorSubProcessor assignEmptyToRootOperatorSubProcessor)
        {
            _selector = new Selector2<Subject, Subject, IAssignOperatorSubProcessor>()

                // Output
                .Register((l, r) => l is EmptySubject && r is PathSubject, assignPathToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is FunctionSubject, assignFunctionToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is ConstantSubject, assignConstantToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is VariableSubject, assignVariableToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is CombinedSubject, assignCombinedToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is CombinedSubject, assignRootToOutputOperatorSubProcessor)
                .Register((l, r) => l is EmptySubject && r is EmptySubject, assignEmptyToOutputOperatorSubProcessor)

                // Variable
                .Register((l, r) => l is VariableSubject && r is PathSubject, assignPathToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is FunctionSubject, assignFunctionToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is ConstantSubject, assignConstantToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is VariableSubject, assignVariableToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is CombinedSubject, assignCombinedToVariableOperatorSubProcessor)
                .Register((l, r) => l is VariableSubject && r is EmptySubject, assignEmptyToVariableOperatorSubProcessor)
                
                // Path
                .Register((l, r) => l is PathSubject && r is PathSubject, assignPathToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is FunctionSubject, assignFunctionToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is ConstantSubject, assignConstantToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is VariableSubject, assignVariableToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is CombinedSubject, assignCombinedToPathOperatorSubProcessor)
                .Register((l, r) => l is PathSubject && r is EmptySubject, assignEmptyToPathOperatorSubProcessor)
                
                // Function
                .Register((l, r) => l is FunctionSubject && r is PathSubject, assignPathToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is FunctionSubject, assignFunctionToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is ConstantSubject, assignConstantToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is VariableSubject, assignVariableToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is CombinedSubject, assignCombinedToFunctionOperatorSubProcessor)
                .Register((l, r) => l is FunctionSubject && r is EmptySubject, assignEmptyToFunctionOperatorSubProcessor)
                
                // Roots
                .Register((l, r) => l is RootSubject && r is RootDefinitionSubject, assignRootDefinitionToRootOperatorSubProcessor)
                .Register((l, r) => l is RootSubject && r is StringConstantSubject, assignStringConstantToRootOperatorSubProcessor)
                .Register((l, r) => l is RootSubject && r is EmptySubject, assignEmptyToRootOperatorSubProcessor);
        }

        public void Process(OperatorParameters parameters)
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
            assigner.Assign(parameters);
        }
    }
}
