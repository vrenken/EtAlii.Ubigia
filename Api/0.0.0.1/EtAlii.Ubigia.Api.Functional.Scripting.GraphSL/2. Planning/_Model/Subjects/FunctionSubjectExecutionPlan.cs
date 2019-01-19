namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class FunctionSubjectExecutionPlan : SubjectExecutionPlanBase
    {
        private readonly IFunctionSubjectProcessor _processor;

        public FunctionSubjectExecutionPlan(
            FunctionSubject subject,
            IFunctionSubjectProcessor processor)
            :base(subject)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(FunctionSubject);
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