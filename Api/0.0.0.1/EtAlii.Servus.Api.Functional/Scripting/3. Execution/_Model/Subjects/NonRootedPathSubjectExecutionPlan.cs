namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class NonRootedPathSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly INonRootedPathSubjectProcessor _processor;

        public NonRootedPathSubjectExecutionPlan(
            NonRootedPathSubject subject,
            INonRootedPathSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (NonRootedPathSubject);
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