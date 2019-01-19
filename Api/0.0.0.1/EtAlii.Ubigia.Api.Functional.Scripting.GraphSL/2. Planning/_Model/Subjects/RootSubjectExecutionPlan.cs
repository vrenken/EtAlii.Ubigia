namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

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

        protected override async Task Execute(ExecutionScope scope, IObserver<object> output)
        {
            await _processor.Process(Subject, scope, output);
        }

        public override string ToString()
        {
            return Subject.ToString();
        }
    }
}