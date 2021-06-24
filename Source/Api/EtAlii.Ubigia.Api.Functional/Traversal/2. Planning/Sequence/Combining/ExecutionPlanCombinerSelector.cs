// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal class ExecutionPlanCombinerSelector : IExecutionPlanCombinerSelector
    {
        private readonly ISubjectExecutionPlanCombiner _subjectExecutionPlanCombiner;
        private readonly IOperatorExecutionPlanCombiner _operatorExecutionPlanCombiner;

        public ExecutionPlanCombinerSelector(
            ISubjectExecutionPlanCombiner subjectExecutionPlanCombiner,
            IOperatorExecutionPlanCombiner operatorExecutionPlanCombiner)
        {
            _subjectExecutionPlanCombiner = subjectExecutionPlanCombiner;
            _operatorExecutionPlanCombiner = operatorExecutionPlanCombiner;
        }

        public IExecutionPlanCombiner Select(IExecutionPlanner planner)
        {
            return planner switch
            {
                ISubjectExecutionPlanner => _subjectExecutionPlanCombiner,
                IOperatorExecutionPlanner => _operatorExecutionPlanCombiner,
                _ => throw new InvalidOperationException($"Unable to select option for criteria: {(planner != null ? planner.ToString() : "[NULL]")}")
            };
        }
    }
}
