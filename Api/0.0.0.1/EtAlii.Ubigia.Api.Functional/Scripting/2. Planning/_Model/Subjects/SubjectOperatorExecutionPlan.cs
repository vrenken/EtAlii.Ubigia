﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class SubjectOperatorExecutionPlan : ISubjectExecutionPlan 
    {
        private readonly IOperatorExecutionPlan _operatorExecutionPlan;
        public Type OutputType => _operatorExecutionPlan.OutputType;
        public Subject Subject { get; }

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