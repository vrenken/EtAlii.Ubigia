﻿namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.Servus.Api.Logical;

    internal class ProcessingContext : IProcessingContext
    {
        public IScriptScope Scope { get { return _scope; } }
        private readonly IScriptScope _scope;

        public ILogicalContext Logical { get { return _logical; } }
        private readonly ILogicalContext _logical;

        public IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get { return _pathSubjectToGraphPathConverter; } }
        private IPathSubjectToGraphPathConverter _pathSubjectToGraphPathConverter;

        public IPathProcessor PathProcessor { get { return _pathProcessor; } }
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
