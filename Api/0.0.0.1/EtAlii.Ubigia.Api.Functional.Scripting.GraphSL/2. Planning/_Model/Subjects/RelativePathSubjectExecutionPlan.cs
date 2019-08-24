namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;

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