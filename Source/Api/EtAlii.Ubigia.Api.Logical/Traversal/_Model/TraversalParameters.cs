// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public class TraversalParameters
    {
        public TraversalParameters(
            GraphPathPart part,
            IPathTraversalContext context, 
            ExecutionScope scope, 
            IObserver<Identifier> output, 
            IObservable<Identifier> input)
        {
            Part = part;
            Context = context;
            Scope = scope;
            Output = output;
            Input = input;
        }

        public GraphPathPart Part { get; }
        public IPathTraversalContext Context { get; }
        public ExecutionScope Scope { get; }
        public IObserver<Identifier> Output { get; }
        public IObservable<Identifier> Input { get; }
    }
}