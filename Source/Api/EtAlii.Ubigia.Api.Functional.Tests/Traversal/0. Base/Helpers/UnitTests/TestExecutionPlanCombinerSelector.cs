// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    internal class TestExecutionPlanCombinerSelector
    {
        public static IExecutionPlanCombinerSelector Create(
            ISequencePartExecutionPlannerSelector sequencePartExecutionPlannerSelector)
        {
            var sourceExecutionPlanCombiner = new SubjectExecutionPlanCombiner();
            var binaryExecutionPlanCombiner = new OperatorExecutionPlanCombiner(sequencePartExecutionPlannerSelector, sourceExecutionPlanCombiner);

            return new ExecutionPlanCombinerSelector(sourceExecutionPlanCombiner, binaryExecutionPlanCombiner);
        }
    }
}
