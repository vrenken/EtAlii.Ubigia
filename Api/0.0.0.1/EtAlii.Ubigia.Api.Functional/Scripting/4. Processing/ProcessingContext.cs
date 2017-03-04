namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    internal class ProcessingContext : IProcessingContext
    {
        public IScriptScope Scope => _scope;
        private readonly IScriptScope _scope;

        public ILogicalContext Logical => _logical;
        private readonly ILogicalContext _logical;

        public IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter => _pathSubjectToGraphPathConverter;
        private IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;

        public IPathProcessor PathProcessor => _pathProcessor;
        private IPathProcessor _pathProcessor;

        public ProcessingContext(IScriptScope scope, ILogicalContext logical)
        {
            _scope = scope;
            _logical = logical;
        }

        public void Initialize(IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter, IPathProcessor pathProcessor)
        {
            if (_pathSubjectToGraphPathConverter != null || _pathProcessor != null)
            {
                throw new NotSupportedException("The ProcessingContext can only be initialized once");
            }

            _pathSubjectToGraphPathConverter = pathSubjectToGraphPathConverter;
            _pathProcessor = pathProcessor;
        }
    }
}
