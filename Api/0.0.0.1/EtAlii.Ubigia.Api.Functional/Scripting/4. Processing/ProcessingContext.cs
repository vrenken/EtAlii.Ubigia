namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    internal class ProcessingContext : IProcessingContext
    {
        public IScriptScope Scope { get; }

        public ILogicalContext Logical { get; }

        public IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get; private set; }

        public IPathProcessor PathProcessor { get; private set; }

        public ProcessingContext(IScriptScope scope, ILogicalContext logical)
        {
            Scope = scope;
            Logical = logical;
        }

        public void Initialize(IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, IPathProcessor pathProcessor)
        {
            if (PathSubjectToGraphPathConverter != null || PathProcessor != null)
            {
                throw new NotSupportedException("The ProcessingContext can only be initialized once");
            }

            PathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
            PathProcessor = pathProcessor;
        }
    }
}
