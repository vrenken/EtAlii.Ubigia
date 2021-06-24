// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    internal class ScriptProcessingContext : IScriptProcessingContext
    {
        public IScriptScope Scope { get; }

        public ILogicalContext Logical { get; }

        public IPathSubjectToGraphPathConverter PathSubjectToGraphPathConverter { get; private set; }

        public IPathProcessor PathProcessor { get; private set; }

        public IAbsolutePathSubjectProcessor AbsolutePathSubjectProcessor { get; private set; }
        public IRelativePathSubjectProcessor RelativePathSubjectProcessor { get; private set; }
        public IRootedPathSubjectProcessor RootedPathSubjectProcessor { get; private set; }

        public IPathSubjectForOutputConverter PathSubjectForOutputConverter { get; private set; }
        public IAddRelativePathToExistingPathProcessor AddRelativePathToExistingPathProcessor { get; private set; }


        public ScriptProcessingContext(IScriptScope scope, ILogicalContext logical)
        {
            Scope = scope;
            Logical = logical;
        }

        public void Initialize(
            IPathSubjectToGraphPathConverter pathSubjectToGraphPathConverter,
            IAbsolutePathSubjectProcessor absolutePathSubjectProcessor,
            IRelativePathSubjectProcessor relativePathSubjectProcessor,
            IRootedPathSubjectProcessor rootedPathSubjectProcessor,
            IPathProcessor pathProcessor,
            IPathSubjectForOutputConverter pathSubjectForOutputConverter,
            IAddRelativePathToExistingPathProcessor addRelativePathToExistingPathProcessor)
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
            PathSubjectForOutputConverter = pathSubjectForOutputConverter;
            AddRelativePathToExistingPathProcessor = addRelativePathToExistingPathProcessor;
        }
    }
}
