namespace EtAlii.Ubigia.Api.Functional
{
    internal class OperatorExecutionPlanCombiner : IOperatorExecutionPlanCombiner
    {
        private readonly ISequencePartExecutionPlannerSelector _sequencePartExecutionPlannerSelector;
        private readonly ISubjectExecutionPlanCombiner _subjectExecutionPlanCombiner;

        public OperatorExecutionPlanCombiner(
            ISequencePartExecutionPlannerSelector sequencePartExecutionPlannerSelector,
            ISubjectExecutionPlanCombiner subjectExecutionPlanCombiner)
        {
            _sequencePartExecutionPlannerSelector = sequencePartExecutionPlannerSelector;
            _subjectExecutionPlanCombiner = subjectExecutionPlanCombiner;
        }

        public ISubjectExecutionPlan Combine(
            IExecutionPlanner planner,
            SequencePart currentPart,
            SequencePart nextPart,
            ISubjectExecutionPlan rightExecutionPlan,
            out bool skipNext)
        {
            ISubjectExecutionPlan leftExecutionPlan = null;

            if (nextPart != null)
            {
                var nextPartExecutionPlanner = _sequencePartExecutionPlannerSelector.Select(nextPart);
                leftExecutionPlan = _subjectExecutionPlanCombiner.Combine(nextPartExecutionPlanner, nextPart, null, null, out var _);
            }

            skipNext = true;

            var operatorExecutionPlan = (IOperatorExecutionPlan)((IOperatorExecutionPlanner)planner).Plan(currentPart, leftExecutionPlan, rightExecutionPlan);
            return new SubjectOperatorExecutionPlan(operatorExecutionPlan);
        }
    }
}