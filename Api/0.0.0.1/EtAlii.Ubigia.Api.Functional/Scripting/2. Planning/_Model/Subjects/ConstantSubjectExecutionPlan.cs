namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class ConstantSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IConstantSubjectProcessor _processor;

        public ConstantSubjectExecutionPlan(
            ConstantSubject subject,
            IConstantSubjectProcessor processor)
            : base(subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return Subject.GetType();
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