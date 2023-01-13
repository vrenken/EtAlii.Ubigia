// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using EtAlii.Ubigia.Diagnostics.Profiling;
using EtAlii.xTechnology.MicroContainer;

public class ProfilingFunctionalExtension2 : IExtension
{
    private readonly IProfiler _profiler;

    public ProfilingFunctionalExtension2(IProfiler profiler)
    {
        _profiler = profiler;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        container.Register(() => _profiler);
        //container.RegisterDecorator(typeof(IScriptProcessor), typeof(ProfilingScriptProcessor))
        //container.RegisterDecorator(typeof(ISequenceProcessor), typeof(ProfilingSequenceProcessor))

        //container.RegisterDecorator(typeof(IOperatorsProcessor), typeof(ProfilingOperatorsProcessor))
        //container.RegisterDecorator(typeof(ISubjectsProcessor), typeof(ProfilingSubjectsProcessor))
        //container.RegisterDecorator(typeof(ICommentProcessor), typeof(ProfilingCommentProcessor))

        //container.RegisterDecorator(typeof(IPathSubjectProcessor), typeof(ProfilingPathSubjectProcessor))
        //container.RegisterDecorator(typeof(IPathSubjectToGraphPathConverter), typeof(ProfilingPathSubjectToGraphPathConverter))
        //container.RegisterDecorator(typeof(IPathProcessor), typeof(ProfilingPathProcessor))

        container.RegisterDecorator<IEntriesToDynamicNodesConverter, ProfilingEntriesToDynamicNodesConverter>();

        //container.RegisterDecorator(typeof(IPathSubjectForOutputConverter), typeof(ProfilingPathSubjectForOutputConverter))
        //container.RegisterDecorator(typeof(IPathSubjectForFunctionAssignmentOperationConverter), typeof(ProfilingPathSubjectForFunctionAssignmentOperationConverter))
        //container.RegisterDecorator(typeof(IPathSubjectForPathAddOperationConverter), typeof(ProfilingPathSubjectForPathAddOperationConverter))
        //container.RegisterDecorator(typeof(IPathSubjectForPathAssignmentOperationConverter), typeof(ProfilingPathSubjectForPathAssignmentOperationConverter))
        //container.RegisterDecorator(typeof(IPathSubjectForPathRemoveOperationConverter), typeof(ProfilingPathSubjectForPathRemoveOperationConverter))
        //container.RegisterDecorator(typeof(IPathSubjectForVariableAddOperationConverter), typeof(ProfilingPathSubjectForVariableAddOperationConverter))
        //container.RegisterDecorator(typeof(IPathSubjectForVariableAssignmentOperationConverter), typeof(ProfilingPathSubjectForVariableAssignmentOperationConverter))
    }
}
