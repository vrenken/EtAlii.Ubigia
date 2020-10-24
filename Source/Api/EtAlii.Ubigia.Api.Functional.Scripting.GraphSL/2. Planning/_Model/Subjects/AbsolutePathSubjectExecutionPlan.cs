namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;

    internal class AbsolutePathSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IAbsolutePathSubjectProcessor _processor;

        public AbsolutePathSubjectExecutionPlan(AbsolutePathSubject subject, IAbsolutePathSubjectProcessor processor)
            : base (subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (AbsolutePathSubject);
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