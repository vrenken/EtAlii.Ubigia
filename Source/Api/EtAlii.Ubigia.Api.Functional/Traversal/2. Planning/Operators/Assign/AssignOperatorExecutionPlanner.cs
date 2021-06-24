// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class AssignOperatorExecutionPlanner : IAssignOperatorExecutionPlanner
    {
        private readonly IAssignOperatorProcessor _processor;

        public AssignOperatorExecutionPlanner(IAssignOperatorProcessor processor)
        {
            _processor = processor;
        }

        public IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right)
        {
            return new AssignOperatorExecutionPlan(left, right, _processor);
        }
    }
}
