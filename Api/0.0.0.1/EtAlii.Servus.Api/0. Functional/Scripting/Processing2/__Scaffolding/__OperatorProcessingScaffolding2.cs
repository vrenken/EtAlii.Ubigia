//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.MicroContainer;

//    internal class OperatorProcessingScaffolding2 : IScaffolding
//    {
//        public void Register(Container container)
//        {
//            //container.Register<IOperatorProcessorSelector2, OperatorProcessorSelector2>();

//            // Processing.
//            container.Register<IAssignOperatorProcessor2, AssignOperatorProcessor2>();
//            container.Register<IAssignOperatorSelector2, AssignOperatorSelector2>();
//            // Alternatives.
//            container.Register<IAssignToPathProcessor2, AssignToPathProcessor2>();
//            container.Register<IAssignToOutputProcessor2, AssignToOutputProcessor2>();
//            container.Register<IAssignToVariableProcessor2, AssignToVariableProcessor2>();
//            container.Register<IAssignToFunctionProcessor2, AssignToFunctionProcessor2>();
//            container.Register<IAssignNodeToPathProcessor2, AssignNodeToPathProcessor2>();
//            container.Register<IAssignDynamicObjectToPathProcessor2, AssignDynamicObjectToPathProcessor2>();
//            container.Register<IAssignDictionaryToPathProcessor2, AssignPropertiesDictionaryToPathProcessor2>();
//            container.Register<IResultConverterSelector2, ResultConverterSelector2>();

//            // Assistance.
//            container.Register<IDynamicObjectToPathInputConverterSelector2, DynamicObjectToPathInputConverterSelector2>();
//            container.Register<IFunctionInputConverterSelector2, FunctionInputConverterSelector2>();
//            container.Register<IUpdateEntryFactory2, UpdateEntryFactory2>();

//            // Processing.
//            container.Register<IAddOperatorProcessor2, AddOperatorProcessor2>();
//            container.Register<IAddOperatorSelector2, AddOperatorSelector2>();
//            // Alternatives.
//            container.Register<IAddByNameToAbsolutePathProcessor2, AddByNameToAbsolutePathProcessor2>();
//            container.Register<IAddByNameToRelativePathProcessor2, AddByNameToRelativePathProcessor2>();
//            container.Register<IAddByIdToAbsolutePathProcessor2, AddByIdToAbsolutePathProcessor2>();
//            container.Register<IAddByIdToRelativePathProcessor2, AddByIdToRelativePathProcessor2>();
//            // Assistance.
//            container.Register<IRecursiveAdder2, RecursiveAdder2>();

//            // Processing.
//            container.Register<IRemoveOperatorProcessor2, RemoveOperatorProcessor2>();
//            container.Register<IRemoveOperatorSelector2, RemoveOperatorSelector2>();
//            // Alternatives.
//            container.Register<IRemoveByNameFromAbsolutePathProcessor2, RemoveByNameFromAbsolutePathProcessor2>();
//            container.Register<IRemoveByNameFromRelativePathProcessor2, RemoveByNameFromRelativePathProcessor2>();
//            container.Register<IRemoveByIdFromAbsolutePathProcessor2, RemoveByIdFromAbsolutePathProcessor2>();
//            container.Register<IRemoveByIdFromRelativePathProcessor2, RemoveByIdFromRelativePathProcessor2>();
//            // Assistance.
//            container.Register<IRecursiveRemover2, RecursiveRemover2>();
//        }
//    }
//}
