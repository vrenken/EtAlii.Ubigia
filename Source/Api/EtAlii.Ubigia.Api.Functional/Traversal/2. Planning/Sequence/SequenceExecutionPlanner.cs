// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    internal class SequenceExecutionPlanner : ISequenceExecutionPlanner
    {
        private readonly ISequencePartExecutionPlannerSelector _sequencePartExecutionPlannerSelector;
        private readonly IExecutionPlanCombinerSelector _executionPlanCombinerSelector;

        public SequenceExecutionPlanner(
            ISequencePartExecutionPlannerSelector sequencePartExecutionPlannerSelector,
            IExecutionPlanCombinerSelector executionPlanCombinerSelector)
        {
            _sequencePartExecutionPlannerSelector = sequencePartExecutionPlannerSelector;
            _executionPlanCombinerSelector = executionPlanCombinerSelector;
        }

        public ISequenceExecutionPlan Plan(Sequence sequence)
        {
            ISubjectExecutionPlan previousPartExecutionPlan = null;

            // We are not interested in planning execution of comment parts, so let's exclude them.
            var parts = sequence.Parts
                .Where(p => !(p is Comment))
                .ToArray();
            var count = parts.Length;

            for (var i = 1; i <= count; i++)
            {
                var currentPosition = count - i;

                var currentPart = parts[currentPosition];
                var currentPartExecutionPlanner = _sequencePartExecutionPlannerSelector.Select(currentPart);
                var currentPartExecutionPlanCombiner = _executionPlanCombinerSelector.Select(currentPartExecutionPlanner);

                var nextPart = currentPosition >= 1 ? parts[currentPosition - 1] : null;
                var currentPartExecutionPlan = currentPartExecutionPlanCombiner.Combine(currentPartExecutionPlanner, currentPart, nextPart, previousPartExecutionPlan, out var skipNext);
                i = skipNext ? i + 1 : i;

                previousPartExecutionPlan = currentPartExecutionPlan;
            }

            var startExecutionPlan = previousPartExecutionPlan;
            var executionPlan = startExecutionPlan != null ? new SequenceExecutionPlan(startExecutionPlan) : SequenceExecutionPlan.Empty;
            return executionPlan;
        }
    }
}
