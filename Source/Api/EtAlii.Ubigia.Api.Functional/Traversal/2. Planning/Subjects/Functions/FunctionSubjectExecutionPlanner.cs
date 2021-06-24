// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class FunctionSubjectExecutionPlanner : IFunctionSubjectExecutionPlanner
    {
        private readonly IFunctionSubjectProcessor _processor;

        public FunctionSubjectExecutionPlanner(IFunctionSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var functionSubject = (FunctionSubject)part;
            return new FunctionSubjectExecutionPlan(functionSubject, _processor);
        }
    }
}
