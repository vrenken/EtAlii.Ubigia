﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;

    internal class AssignOperatorExecutionPlan : OperatorExecutionPlanBase
    {
        private readonly IAssignOperatorProcessor _processor;

        public AssignOperatorExecutionPlan(
            ISubjectExecutionPlan left,
            ISubjectExecutionPlan right, 
            IAssignOperatorProcessor processor)
            : base(left, right)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(Identifier);
        }

        protected override Task Execute(OperatorParameters parameters)
        {
            return _processor.Process(parameters);
        }

        public override string ToString()
        {
            return Left + " <= " + Right;
        }
    }
}