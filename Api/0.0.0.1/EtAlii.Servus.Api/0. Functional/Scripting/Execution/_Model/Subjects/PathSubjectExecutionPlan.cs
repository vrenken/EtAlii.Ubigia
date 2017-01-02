namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class PathSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IPathSubjectProcessor _processor;

        public PathSubjectExecutionPlan(
            PathSubject subject,
            IPathSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (PathSubject);
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