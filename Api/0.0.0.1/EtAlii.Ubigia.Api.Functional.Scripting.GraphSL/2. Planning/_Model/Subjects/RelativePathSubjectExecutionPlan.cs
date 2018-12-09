namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class RelativePathSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IRelativePathSubjectProcessor _processor;

        public RelativePathSubjectExecutionPlan(
            RelativePathSubject subject,
            IRelativePathSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (RelativePathSubject);
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