// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingScriptProcessorExtension : IScriptProcessorExtension
    {
        private readonly IProfiler _profiler;

        public ProfilingScriptProcessorExtension(IProfiler profiler)
        {
            _profiler = profiler;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _profiler);
            container.RegisterDecorator(typeof(IScriptProcessor), typeof(ProfilingScriptProcessor));
            //container.RegisterDecorator(typeof(ISequenceProcessor), typeof(ProfilingSequenceProcessor))

            //container.RegisterDecorator(typeof(IOperatorsProcessor2), typeof(ProfilingOperatorsProcessor))
            //container.RegisterDecorator(typeof(ISubjectsProcessor2), typeof(ProfilingSubjectsProcessor))
            //container.RegisterDecorator(typeof(ICommentProcessor2), typeof(ProfilingCommentProcessor))

            container.RegisterDecorator(typeof(IAbsolutePathSubjectProcessor), typeof(ProfilingAbsolutePathSubjectProcessor));
            container.RegisterDecorator(typeof(IRelativePathSubjectProcessor), typeof(ProfilingRelativePathSubjectProcessor));
            container.RegisterDecorator(typeof(IPathSubjectToGraphPathConverter), typeof(ProfilingPathSubjectToGraphPathConverter));
            container.RegisterDecorator(typeof(IPathProcessor), typeof(ProfilingPathProcessor));

            container.RegisterDecorator(typeof(IEntriesToDynamicNodesConverter), typeof(ProfilingEntriesToDynamicNodesConverter));

            container.RegisterDecorator(typeof(IPathSubjectForOutputConverter), typeof(ProfilingPathSubjectForOutputConverter));
            //container.RegisterDecorator(typeof(IPathSubjectForFunctionAssignmentOperationConverter2), typeof(ProfilingPathSubjectForFunctionAssignmentOperationConverter))
            //container.RegisterDecorator(typeof(IPathSubjectForPathAddOperationConverter2), typeof(ProfilingPathSubjectForPathAddOperationConverter))
            //container.RegisterDecorator(typeof(IPathSubjectForPathAssignmentOperationConverter2), typeof(ProfilingPathSubjectForPathAssignmentOperationConverter))
            //container.RegisterDecorator(typeof(IPathSubjectForPathRemoveOperationConverter2), typeof(ProfilingPathSubjectForPathRemoveOperationConverter))
            //container.RegisterDecorator(typeof(IPathSubjectForVariableAddOperationConverter2), typeof(ProfilingPathSubjectForVariableAddOperationConverter))
            //container.RegisterDecorator(typeof(IPathSubjectForVariableAssignmentOperationConverter2), typeof(ProfilingPathSubjectForVariableAssignmentOperationConverter))
        }
    }
}
