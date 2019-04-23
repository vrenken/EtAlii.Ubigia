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

        public IAbsolutePathSubjectProcessor AbsolutePathSubjectProcessor { get; private set; }
        public IRelativePathSubjectProcessor RelativePathSubjectProcessor { get; private set; }
        public IRootedPathSubjectProcessor RootedPathSubjectProcessor { get; private set; }

        public IPathSubjectForOutputConverter PathSubjectForOutputConverter { get; }
        public IAddRelativePathToExistingPathProcessor AddRelativePathToExistingPathProcessor { get; }

        
        public ProcessingContext(
            IScriptScope scope, 
            ILogicalContext logical, 
            IPathSubjectForOutputConverter pathSubjectForOutputConverter, 
            IAddRelativePathToExistingPathProcessor addRelativePathToExistingPathProcessor)
        {
            Scope = scope;
            Logical = logical;
            PathSubjectForOutputConverter = pathSubjectForOutputConverter;
            AddRelativePathToExistingPathProcessor = addRelativePathToExistingPathProcessor;
        }

        public void Initialize(
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IAbsolutePathSubjectProcessor absolutePathSubjectProcessor,
            IRelativePathSubjectProcessor relativePathSubjectProcessor,
            IRootedPathSubjectProcessor rootedPathSubjectProcessor,
            IPathProcessor pathProcessor)
        {
            if (PathSubjectToGraphPathConverter != null || PathProcessor != null || AbsolutePathSubjectProcessor != null || RelativePathSubjectProcessor != null || RootedPathSubjectProcessor != null)
            {
                throw new NotSupportedException("The ProcessingContext can only be initialized once");
            }

            PathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
            AbsolutePathSubjectProcessor = absolutePathSubjectProcessor;
            RelativePathSubjectProcessor = relativePathSubjectProcessor;
            RootedPathSubjectProcessor = rootedPathSubjectProcessor;
            PathProcessor = pathProcessor; 
        }
    }
}
