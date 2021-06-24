// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class AddOperatorExecutionPlan : OperatorExecutionPlanBase
    {
        private readonly IAddOperatorProcessor _processor;

        public AddOperatorExecutionPlan(
            ISubjectExecutionPlan left,
            ISubjectExecutionPlan right,
            IAddOperatorProcessor processor)
            : base(left, right)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof (Identifier);
        }

        protected override Task Execute(OperatorParameters parameters)
        {
            return _processor.Process(parameters);
        }

        public override string ToString()
        {
            return Left + " += " + Right;
        }
    }
}
