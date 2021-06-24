// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class RootedPathSubjectExecutionPlanner : IRootedPathSubjectExecutionPlanner
    {
        private readonly IScriptProcessingContext _processingContext;

        public RootedPathSubjectExecutionPlanner(IScriptProcessingContext processingContext)
        {
            _processingContext = processingContext;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (RootedPathSubject)part;
            return new RootedPathSubjectExecutionPlan(pathSubject, _processingContext.RootedPathSubjectProcessor);
        }
    }
}
