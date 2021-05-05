namespace EtAlii.Ubigia.Api.Functional.Traversal
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
