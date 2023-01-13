// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public interface IRootHandlerMapper
{
    RootType Type { get; }

    IRootHandler[] AllowedRootHandlers { get; }

    //string TypeName [ get; ]

    //ParameterSet[] ParameterSets [ get; ]
    //void Process(IRootContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
    //void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
}
