//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.MicroContainer;

//    internal class ScriptExecutionScaffolding2 : IScaffolding
//    {
//        public void Register(Container container)
//        {
//            container.Register<IScriptExecutionPlanner, ScriptExecutionPlanner>();
//            container.Register<ISequenceExecutionPlanner, SequenceExecutionPlanner>();

//            container.Register<ICommentExecutionPlanner, CommentExecutionPlanner>();
//            container.Register<ISubjectExecutionPlannerSelector, SubjectExecutionPlannerSelector>();
//            container.Register<IPathSubjectExecutionPlanner, PathSubjectExecutionPlanner>();
//            container.Register<IFunctionSubjectExecutionPlanner, FunctionSubjectExecutionPlanner>();
//            container.Register<IConstantSubjectExecutionPlanner, ConstantSubjectExecutionPlanner>();
//            container.Register<IVariableSubjectExecutionPlanner, VariableSubjectExecutionPlanner>();

//            container.Register<IOperatorExecutionPlannerSelector, OperatorExecutionPlannerSelector>();
//            container.Register<IRemoveOperatorExecutionPlanner, RemoveOperatorExecutionPlanner>();
//            container.Register<IAddOperatorExecutionPlanner, AddOperatorExecutionPlanner>();
//            container.Register<IAssignOperatorExecutionPlanner, AssignOperatorExecutionPlanner>();

//            container.Register<ISequencePartExecutionPlannerSelector, SequencePartExecutionPlannerSelector>();

//            container.Register<IExecutionPlanCombinerSelector, ExecutionPlanCombinerSelector>();
//            container.Register<ISubjectExecutionPlanCombiner, SubjectExecutionPlanCombiner>();
//            container.Register<IOperatorExecutionPlanCombiner, OperatorExecutionPlanCombiner>();
//        }
//    }
//}
