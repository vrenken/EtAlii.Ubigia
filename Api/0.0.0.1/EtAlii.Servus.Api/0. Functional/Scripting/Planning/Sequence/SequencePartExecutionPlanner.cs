namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.xTechnology.Structure;

    internal class SequencePartExecutionPlannerSelector :  ISequencePartExecutionPlannerSelector
    {
        private readonly ISelector<SequencePart, Func<object, ISequencePartExecutionPlanner>> _selector;

        internal SequencePartExecutionPlannerSelector(
            IOperatorExecutionPlannerSelector operatorExecutionPlannerSelector,
            ISubjectExecutionPlannerSelector subjectExecutionPlannerSelector,
            ICommentExecutionPlanner commentExecutionPlanner)
        {
            _selector = new Selector<SequencePart, Func<object, ISequencePartExecutionPlanner>>()
                .Register(part => part is Operator, part => (ISequencePartExecutionPlanner)operatorExecutionPlannerSelector.Select(part))
                .Register(part => part is Subject, part => (ISequencePartExecutionPlanner)subjectExecutionPlannerSelector.Select(part))
                .Register(part => part is Comment, part => commentExecutionPlanner);
        }

        public IExecutionPlanner Select(object item)
        {
            var sequencePart = (SequencePart) item;
            var selector = _selector.Select(sequencePart);
            return selector(sequencePart);
        }
    }
}