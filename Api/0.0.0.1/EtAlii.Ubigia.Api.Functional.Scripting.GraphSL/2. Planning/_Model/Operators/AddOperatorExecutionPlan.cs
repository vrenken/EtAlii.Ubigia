namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class AddOperatorExecutionPlan : OperatorExecutionPlanBase
    {
        private readonly IAddOperatorProcessor _processor;

        public AddOperatorExecutionPlan(
            ISubjectExecutionPlan left,
            ISubjectExecutionPlan right, 
            IAddOperatorProcessor processor)
            : base(left, right)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (Identifier);
        }

        protected override async Task Execute(OperatorParameters parameters)
        {
            await _processor.Process(parameters);
        }

        public override string ToString()
        {
            return Left + " += " + Right;
        }
    }
}