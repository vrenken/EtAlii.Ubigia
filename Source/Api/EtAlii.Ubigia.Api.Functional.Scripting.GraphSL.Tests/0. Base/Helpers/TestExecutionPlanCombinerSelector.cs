namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
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