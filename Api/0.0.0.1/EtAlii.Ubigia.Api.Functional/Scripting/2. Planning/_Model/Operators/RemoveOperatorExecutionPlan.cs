namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class RemoveOperatorExecutionPlan : OperatorExecutionPlanBase
    {
        private readonly IRemoveOperatorProcessor _processor;

        public RemoveOperatorExecutionPlan(
            ISubjectExecutionPlan left, 
            ISubjectExecutionPlan right, 
            IRemoveOperatorProcessor processor)
            : base(left, right)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(Identifier);
        }

        protected override void Execute(OperatorParameters parameters)
        {
            _processor.Process(parameters);
        }

        public override string ToString()
        {
            return Left.ToString() + " -= " + Right.ToString();
        }
    }
}