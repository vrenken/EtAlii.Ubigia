namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class FunctionSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IFunctionSubjectProcessor _processor;

        public FunctionSubjectExecutionPlan(
            FunctionSubject subject,
            IFunctionSubjectProcessor processor)
            :base(subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(FunctionSubject);
        }

        protected override void Execute(ExecutionScope scope, IObserver<object> output)
        {
            _processor.Process(Subject, scope, output);
        }

        public override string ToString()
        {
            return Subject.ToString();
        }
    }
}