namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class RootDefinitionSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IRootDefinitionSubjectProcessor _processor;

        public RootDefinitionSubjectExecutionPlan(
            RootDefinitionSubject subject,
            IRootDefinitionSubjectProcessor processor)
            :base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (RootDefinitionSubject);
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