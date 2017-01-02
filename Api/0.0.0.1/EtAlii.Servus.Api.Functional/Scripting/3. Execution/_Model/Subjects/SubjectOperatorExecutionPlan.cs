namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class SubjectOperatorExecutionPlan : ISubjectExecutionPlan 
    {
        private readonly IOperatorExecutionPlan _operatorExecutionPlan;
        public Type OutputType { get { return _operatorExecutionPlan.OutputType; } }
        public Subject Subject { get; private set; }

        public SubjectOperatorExecutionPlan(IOperatorExecutionPlan operatorExecutionPlan)
        {
            _operatorExecutionPlan = operatorExecutionPlan;
            Subject = new CombinedSubject();
        }

        public IObservable<object> Execute(ExecutionScope scope)
        {
            return _operatorExecutionPlan.Execute(scope);
        }

        public override string ToString()
        {
            return _operatorExecutionPlan.ToString();
        }
    }
}