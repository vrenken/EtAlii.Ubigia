// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class OperatorProcessingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            // Processing.
            container.Register<IAssignOperatorProcessor, AssignOperatorProcessor>();

            container.Register<IAssignPathToVariableOperatorSubProcessor, AssignPathToVariableOperatorSubProcessor>();
            container.Register<IAssignFunctionToVariableOperatorSubProcessor, AssignFunctionToVariableOperatorSubProcessor>();
            container.Register<IAssignConstantToVariableOperatorSubProcessor, AssignConstantToVariableOperatorSubProcessor>();
            container.Register<IAssignVariableToVariableOperatorSubProcessor, AssignVariableToVariableOperatorSubProcessor>();
            container.Register<IAssignCombinedToVariableOperatorSubProcessor, AssignCombinedToVariableOperatorSubProcessor>();
            container.Register<IAssignTagToVariableOperatorSubProcessor, AssignTagToVariableOperatorSubProcessor>();
            container.Register<IAssignEmptyToVariableOperatorSubProcessor, AssignEmptyToVariableOperatorSubProcessor>();

            container.Register<IAssignVariableToPathOperatorSubProcessor, AssignVariableToPathOperatorSubProcessor>();
            container.Register<IAssignPathToPathOperatorSubProcessor, AssignPathToPathOperatorSubProcessor>();
            container.Register<IAssignFunctionToPathOperatorSubProcessor, AssignFunctionToPathOperatorSubProcessor>();
            container.Register<IAssignConstantToPathOperatorSubProcessor, AssignConstantToPathOperatorSubProcessor>();
            container.Register<IAssignCombinedToPathOperatorSubProcessor, AssignCombinedToPathOperatorSubProcessor>();
            container.Register<IAssignEmptyToPathOperatorSubProcessor, AssignEmptyToPathOperatorSubProcessor>();

            container.Register<IAssignPathToFunctionOperatorSubProcessor, AssignPathToFunctionOperatorSubProcessor>();
            container.Register<IAssignFunctionToFunctionOperatorSubProcessor, AssignFunctionToFunctionOperatorSubProcessor>();
            container.Register<IAssignConstantToFunctionOperatorSubProcessor, AssignConstantToFunctionOperatorSubProcessor>();
            container.Register<IAssignVariableToFunctionOperatorSubProcessor, AssignVariableToFunctionOperatorSubProcessor>();
            container.Register<IAssignCombinedToFunctionOperatorSubProcessor, AssignCombinedToFunctionOperatorSubProcessor>();
            container.Register<IAssignTagToFunctionOperatorSubProcessor, AssignTagToFunctionOperatorSubProcessor>();
            container.Register<IAssignEmptyToFunctionOperatorSubProcessor, AssignEmptyToFunctionOperatorSubProcessor>();

            container.Register<IAssignPathToOutputOperatorSubProcessor, AssignPathToOutputOperatorSubProcessor>();
            container.Register<IAssignFunctionToOutputOperatorSubProcessor, AssignFunctionToOutputOperatorSubProcessor>();
            container.Register<IAssignConstantToOutputOperatorSubProcessor, AssignConstantToOutputOperatorSubProcessor>();
            container.Register<IAssignVariableToOutputOperatorSubProcessor, AssignVariableToOutputOperatorSubProcessor>();
            container.Register<IAssignCombinedToOutputOperatorSubProcessor, AssignCombinedToOutputOperatorSubProcessor>();
            container.Register<IAssignRootToOutputOperatorSubProcessor, AssignRootToOutputOperatorSubProcessor>();
            container.Register<IAssignTagToOutputOperatorSubProcessor, AssignTagToOutputOperatorSubProcessor>();
            container.Register<IAssignEmptyToOutputOperatorSubProcessor, AssignEmptyToOutputOperatorSubProcessor>();


            container.Register<IAssignRootDefinitionToRootOperatorSubProcessor, AssignRootDefinitionToRootOperatorSubProcessor>();
            container.Register<IAssignStringConstantToRootOperatorSubProcessor, AssignStringConstantToRootOperatorSubProcessor>();
            container.Register<IAssignEmptyToRootOperatorSubProcessor, AssignEmptyToRootOperatorSubProcessor>();


            // Helpers
            //container.Register<IToIdentifierAssignerSelector, ToIdentifierAssignerSelector>()
            //container.Register<IPropertiesToIdentifierAssigner, PropertiesToIdentifierAssigner>()
            //container.Register<IDynamicObjectToIdentifierAssigner, DynamicObjectToIdentifierAssigner>()
            //container.Register<INodeToIdentifierAssigner, NodeToIdentifierAssigner>()

            container.Register<IResultConverter, ResultConverter>();
            //container.Register<IUpdateEntryFactory, UpdateEntryFactory>()

            //container.Register<IAssignOperatorSelector, AssignOperatorSelector>()

            // Alternatives.

            // Assistance.

            // Processing.
            container.Register<IAddOperatorProcessor, AddOperatorProcessor>();

            // Alternatives.

            container.Register<IAddByNameAsNewPathProcessor, AddByNameAsNewPathProcessor>();
            container.Register<IAddRootedPathToExistingPathProcessor, AddRootedPathToExistingPathProcessor>();
            container.Register<IAddAbsolutePathToExistingPathProcessor, AddAbsolutePathToExistingPathProcessor>();
            container.Register<IAddFunctionToExistingPathProcessor, AddFunctionToExistingPathProcessor>();
            container.Register<IAddRelativePathToExistingPathProcessor, AddRelativePathToExistingPathProcessor>();
            container.Register<IAddConstantToExistingPathProcessor, AddConstantToExistingPathProcessor>();
            container.Register<IAddVariableAsNewPathProcessor, AddVariableAsNewPathProcessor>();
            container.Register<IAddVariableToExistingPathProcessor, AddVariableToExistingPathProcessor>();
            // Assistance.
            container.Register<IRecursiveAdder, RecursiveAdder>();

            // Processing.
            container.Register<IRemoveOperatorProcessor, RemoveOperatorProcessor>();

            // Alternatives.
            container.Register<IRemoveByNameFromAbsolutePathProcessor, RemoveByNameFromAbsolutePathProcessor>();
            container.Register<IRemoveByNameFromRelativePathProcessor, RemoveByNameFromRelativePathProcessor>();
            container.Register<IRemoveByIdFromAbsolutePathProcessor, RemoveByIdFromAbsolutePathProcessor>();
            container.Register<IRemoveByIdFromRelativePathProcessor, RemoveByIdFromRelativePathProcessor>();
            // Assistance.
            container.Register<IRecursiveRemover, RecursiveRemover>();
        }
    }
}
