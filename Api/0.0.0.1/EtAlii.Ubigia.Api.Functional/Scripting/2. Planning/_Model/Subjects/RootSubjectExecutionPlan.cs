namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class RootSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IRootSubjectProcessor _processor;

        public RootSubjectExecutionPlan(
            RootSubject subject,
            IRootSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (RootSubject);
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