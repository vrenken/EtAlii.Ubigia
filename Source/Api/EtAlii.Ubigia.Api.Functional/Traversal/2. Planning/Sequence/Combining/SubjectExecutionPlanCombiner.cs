// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class SubjectExecutionPlanCombiner : ISubjectExecutionPlanCombiner
    {
        public ISubjectExecutionPlan Combine(
            IExecutionPlanner planner,
            SequencePart currentPart,
            SequencePart nextPart,
            ISubjectExecutionPlan rightExecutionPlan,
            out bool skipNext)
        {
            skipNext = false;
            return ((ISubjectExecutionPlanner)planner).Plan(currentPart);
        }
    }
}
