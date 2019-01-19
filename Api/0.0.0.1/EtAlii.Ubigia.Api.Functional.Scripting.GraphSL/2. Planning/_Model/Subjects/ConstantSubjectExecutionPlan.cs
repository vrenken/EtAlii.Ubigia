namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class ConstantSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IConstantSubjectProcessor _processor;

        public ConstantSubjectExecutionPlan(
            ConstantSubject subject,
            IConstantSubjectProcessor processor)
            : base(subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return Subject.GetType();
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