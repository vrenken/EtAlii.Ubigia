namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class OperatorExecutionPlannerSelector : IOperatorExecutionPlannerSelector
    {
        private readonly ISelector<Operator, IOperatorExecutionPlanner> _selector;

        public OperatorExecutionPlannerSelector(
            IAddOperatorExecutionPlanner addOperatorExecutionPlanner,
            IAssignOperatorExecutionPlanner assignOperatorExecutionPlanner,
            IRemoveOperatorExecutionPlanner removeOperatorExecutionPlanner)
        {
            _selector = new Selector<Operator, IOperatorExecutionPlanner>()
                .Register(@operator => @operator is AddOperator, addOperatorExecutionPlanner)
                .Register(@operator => @operator is AssignOperator, assignOperatorExecutionPlanner)
                .Register(@operator => @operator is RemoveOperator, removeOperatorExecutionPlanner);
        }

        public IExecutionPlanner Select(object item)
        {
            var operatorPlanner = (Operator)item;
            return _selector.Select(operatorPlanner);
        }
    }
}