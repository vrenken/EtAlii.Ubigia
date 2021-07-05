// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class AssignOperatorProcessor : IAssignOperatorProcessor
    {
        private readonly IAssignPathToVariableOperatorSubProcessor _assignPathToVariableOperatorSubProcessor;
        private readonly IAssignFunctionToVariableOperatorSubProcessor _assignFunctionToVariableOperatorSubProcessor;
        private readonly IAssignConstantToVariableOperatorSubProcessor _assignConstantToVariableOperatorSubProcessor;
        private readonly IAssignVariableToVariableOperatorSubProcessor _assignVariableToVariableOperatorSubProcessor;
        private readonly IAssignCombinedToVariableOperatorSubProcessor _assignCombinedToVariableOperatorSubProcessor;
        private readonly IAssignTagToVariableOperatorSubProcessor _assignTagToVariableOperatorSubProcessor;
        private readonly IAssignEmptyToVariableOperatorSubProcessor _assignEmptyToVariableOperatorSubProcessor;
        private readonly IAssignVariableToPathOperatorSubProcessor _assignVariableToPathOperatorSubProcessor;
        private readonly IAssignPathToPathOperatorSubProcessor _assignPathToPathOperatorSubProcessor;
        private readonly IAssignFunctionToPathOperatorSubProcessor _assignFunctionToPathOperatorSubProcessor;
        private readonly IAssignConstantToPathOperatorSubProcessor _assignConstantToPathOperatorSubProcessor;
        private readonly IAssignCombinedToPathOperatorSubProcessor _assignCombinedToPathOperatorSubProcessor;
        private readonly IAssignEmptyToPathOperatorSubProcessor _assignEmptyToPathOperatorSubProcessor;
        private readonly IAssignPathToFunctionOperatorSubProcessor _assignPathToFunctionOperatorSubProcessor;
        private readonly IAssignFunctionToFunctionOperatorSubProcessor _assignFunctionToFunctionOperatorSubProcessor;
        private readonly IAssignConstantToFunctionOperatorSubProcessor _assignConstantToFunctionOperatorSubProcessor;
        private readonly IAssignVariableToFunctionOperatorSubProcessor _assignVariableToFunctionOperatorSubProcessor;
        private readonly IAssignCombinedToFunctionOperatorSubProcessor _assignCombinedToFunctionOperatorSubProcessor;
        private readonly IAssignTagToFunctionOperatorSubProcessor _assignTagToFunctionOperatorSubProcessor;
        private readonly IAssignEmptyToFunctionOperatorSubProcessor _assignEmptyToFunctionOperatorSubProcessor;
        private readonly IAssignPathToOutputOperatorSubProcessor _assignPathToOutputOperatorSubProcessor;
        private readonly IAssignFunctionToOutputOperatorSubProcessor _assignFunctionToOutputOperatorSubProcessor;
        private readonly IAssignConstantToOutputOperatorSubProcessor _assignConstantToOutputOperatorSubProcessor;
        private readonly IAssignVariableToOutputOperatorSubProcessor _assignVariableToOutputOperatorSubProcessor;
        private readonly IAssignCombinedToOutputOperatorSubProcessor _assignCombinedToOutputOperatorSubProcessor;
        private readonly IAssignRootToOutputOperatorSubProcessor _assignRootToOutputOperatorSubProcessor;
        private readonly IAssignTagToOutputOperatorSubProcessor _assignTagToOutputOperatorSubProcessor;
        private readonly IAssignEmptyToOutputOperatorSubProcessor _assignEmptyToOutputOperatorSubProcessor;
        private readonly IAssignRootDefinitionToRootOperatorSubProcessor _assignRootDefinitionToRootOperatorSubProcessor;
        private readonly IAssignStringConstantToRootOperatorSubProcessor _assignStringConstantToRootOperatorSubProcessor;
        private readonly IAssignEmptyToRootOperatorSubProcessor _assignEmptyToRootOperatorSubProcessor;

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many specific processing variations. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
        public AssignOperatorProcessor(
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
#pragma warning restore S107
        {
            _assignPathToVariableOperatorSubProcessor = assignPathToVariableOperatorSubProcessor;
            _assignFunctionToVariableOperatorSubProcessor = assignFunctionToVariableOperatorSubProcessor;
            _assignConstantToVariableOperatorSubProcessor = assignConstantToVariableOperatorSubProcessor;
            _assignVariableToVariableOperatorSubProcessor = assignVariableToVariableOperatorSubProcessor;
            _assignCombinedToVariableOperatorSubProcessor = assignCombinedToVariableOperatorSubProcessor;
            _assignTagToVariableOperatorSubProcessor = assignTagToVariableOperatorSubProcessor;
            _assignEmptyToVariableOperatorSubProcessor = assignEmptyToVariableOperatorSubProcessor;
            _assignVariableToPathOperatorSubProcessor = assignVariableToPathOperatorSubProcessor;
            _assignPathToPathOperatorSubProcessor = assignPathToPathOperatorSubProcessor;
            _assignFunctionToPathOperatorSubProcessor = assignFunctionToPathOperatorSubProcessor;
            _assignConstantToPathOperatorSubProcessor = assignConstantToPathOperatorSubProcessor;
            _assignCombinedToPathOperatorSubProcessor = assignCombinedToPathOperatorSubProcessor;
            _assignEmptyToPathOperatorSubProcessor = assignEmptyToPathOperatorSubProcessor;
            _assignPathToFunctionOperatorSubProcessor = assignPathToFunctionOperatorSubProcessor;
            _assignFunctionToFunctionOperatorSubProcessor = assignFunctionToFunctionOperatorSubProcessor;
            _assignConstantToFunctionOperatorSubProcessor = assignConstantToFunctionOperatorSubProcessor;
            _assignVariableToFunctionOperatorSubProcessor = assignVariableToFunctionOperatorSubProcessor;
            _assignCombinedToFunctionOperatorSubProcessor = assignCombinedToFunctionOperatorSubProcessor;
            _assignTagToFunctionOperatorSubProcessor = assignTagToFunctionOperatorSubProcessor;
            _assignEmptyToFunctionOperatorSubProcessor = assignEmptyToFunctionOperatorSubProcessor;
            _assignPathToOutputOperatorSubProcessor = assignPathToOutputOperatorSubProcessor;
            _assignFunctionToOutputOperatorSubProcessor = assignFunctionToOutputOperatorSubProcessor;
            _assignConstantToOutputOperatorSubProcessor = assignConstantToOutputOperatorSubProcessor;
            _assignVariableToOutputOperatorSubProcessor = assignVariableToOutputOperatorSubProcessor;
            _assignCombinedToOutputOperatorSubProcessor = assignCombinedToOutputOperatorSubProcessor;
            _assignRootToOutputOperatorSubProcessor = assignRootToOutputOperatorSubProcessor;
            _assignTagToOutputOperatorSubProcessor = assignTagToOutputOperatorSubProcessor;
            _assignEmptyToOutputOperatorSubProcessor = assignEmptyToOutputOperatorSubProcessor;
            _assignRootDefinitionToRootOperatorSubProcessor = assignRootDefinitionToRootOperatorSubProcessor;
            _assignStringConstantToRootOperatorSubProcessor = assignStringConstantToRootOperatorSubProcessor;
            _assignEmptyToRootOperatorSubProcessor = assignEmptyToRootOperatorSubProcessor;
        }

        public async Task Process(OperatorParameters parameters)
        {
            var lastRightPathSubjectPartHasNoTag = true;
            if (parameters.RightSubject is PathSubject rightSubjectAsPathSubject)
            {
                if (rightSubjectAsPathSubject.Parts.LastOrDefault() is TaggedPathSubjectPart taggedPathSubjectPart)
                {
                    lastRightPathSubjectPartHasNoTag = string.IsNullOrEmpty(taggedPathSubjectPart.Tag);
                }
                else
                {
                    lastRightPathSubjectPartHasNoTag = false;
                }
            }
            IAssignOperatorSubProcessor assigner = (parameters.LeftSubject, parameters.RightSubject, lastRightPathSubjectPartHasNoTag) switch
            {
                //RegisterOutputSubProcessors
                (EmptySubject, PathSubject, true) => _assignTagToOutputOperatorSubProcessor,
                (EmptySubject, PathSubject, _) => _assignPathToOutputOperatorSubProcessor,
                (EmptySubject, FunctionSubject, _) => _assignFunctionToOutputOperatorSubProcessor,
                (EmptySubject, ConstantSubject, _) => _assignConstantToOutputOperatorSubProcessor,
                (EmptySubject, VariableSubject, _) => _assignVariableToOutputOperatorSubProcessor,
                (EmptySubject, CombinedSubject, _) => _assignCombinedToOutputOperatorSubProcessor,
                (EmptySubject, RootSubject, _) => _assignRootToOutputOperatorSubProcessor,
                (EmptySubject, EmptySubject, _) => _assignEmptyToOutputOperatorSubProcessor,

                //RegisterVariableSubProcessors
                (VariableSubject, PathSubject, true) => _assignTagToVariableOperatorSubProcessor,
                (VariableSubject, PathSubject, _) => _assignPathToVariableOperatorSubProcessor,
                (VariableSubject, FunctionSubject, _) => _assignFunctionToVariableOperatorSubProcessor,
                (VariableSubject, ConstantSubject, _) => _assignConstantToVariableOperatorSubProcessor,
                (VariableSubject, VariableSubject, _) => _assignVariableToVariableOperatorSubProcessor,
                (VariableSubject, CombinedSubject, _) => _assignCombinedToVariableOperatorSubProcessor,
                (VariableSubject, EmptySubject, _) => _assignEmptyToVariableOperatorSubProcessor,

                //RegisterPathSubProcessors
                (PathSubject, PathSubject, _) => _assignPathToPathOperatorSubProcessor,
                (PathSubject, FunctionSubject, _) => _assignFunctionToPathOperatorSubProcessor,
                (PathSubject, ConstantSubject, _) => _assignConstantToPathOperatorSubProcessor,
                (PathSubject, VariableSubject, _) => _assignVariableToPathOperatorSubProcessor,
                (PathSubject, CombinedSubject, _) => _assignCombinedToPathOperatorSubProcessor,
                (PathSubject, EmptySubject, _) => _assignEmptyToPathOperatorSubProcessor,

                //RegisterFunctionSubProcessors
                (FunctionSubject, PathSubject, true) => _assignTagToFunctionOperatorSubProcessor,
                (FunctionSubject, PathSubject, _) => _assignPathToFunctionOperatorSubProcessor,
                (FunctionSubject, FunctionSubject, _) => _assignFunctionToFunctionOperatorSubProcessor,
                (FunctionSubject, ConstantSubject, _) => _assignConstantToFunctionOperatorSubProcessor,
                (FunctionSubject, VariableSubject, _) => _assignVariableToFunctionOperatorSubProcessor,
                (FunctionSubject, CombinedSubject, _) => _assignCombinedToFunctionOperatorSubProcessor,
                (FunctionSubject, EmptySubject, _) => _assignEmptyToFunctionOperatorSubProcessor,

                //RegisterRootSubProcessors
                (RootSubject, RootDefinitionSubject, _) => _assignRootDefinitionToRootOperatorSubProcessor,
                (RootSubject, StringConstantSubject, _) => _assignStringConstantToRootOperatorSubProcessor,
                (RootSubject, EmptySubject, _) => _assignEmptyToRootOperatorSubProcessor,
                _ => throw new ScriptProcessingException($"No supported mapping found for the AssignOperatorProcessor to work with (left: {(parameters.LeftSubject == null ? "NULL" : parameters.LeftSubject.ToString())}, right: {(parameters.RightSubject == null ? "NULL" : parameters.RightSubject.ToString())})")
            };

            await assigner.Assign(parameters).ConfigureAwait(false);
        }
    }
}
