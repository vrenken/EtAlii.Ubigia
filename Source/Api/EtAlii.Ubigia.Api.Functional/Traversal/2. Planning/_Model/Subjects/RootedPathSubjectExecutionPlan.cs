namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class RootedPathSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IRootedPathSubjectProcessor _processor;

        public RootedPathSubjectExecutionPlan(
            RootedPathSubject subject,
            IRootedPathSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (RootedPathSubject);
        }

        protected override Task Execute(ExecutionScope scope, IObserver<object> output)
        {
            return _processor.Process(Subject, scope, output);
        }

        public override string ToString()
        {
            return Subject.ToString();
        }
    }
}
