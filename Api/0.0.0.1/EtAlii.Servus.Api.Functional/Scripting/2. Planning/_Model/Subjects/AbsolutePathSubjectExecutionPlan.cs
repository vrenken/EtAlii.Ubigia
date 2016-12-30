namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class AbsolutePathSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IAbsolutePathSubjectProcessor _processor;

        public AbsolutePathSubjectExecutionPlan(
            AbsolutePathSubject subject,
            IAbsolutePathSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (AbsolutePathSubject);
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