// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class AbsolutePathSubjectExecutionPlanner : IAbsolutePathSubjectExecutionPlanner
    {
        private readonly IScriptProcessingContext _processingContext;

        public AbsolutePathSubjectExecutionPlanner(IScriptProcessingContext processingContext)
        {
            _processingContext = processingContext;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (AbsolutePathSubject)part;
            return new AbsolutePathSubjectExecutionPlan(pathSubject, _processingContext.AbsolutePathSubjectProcessor);
        }
    }
}
