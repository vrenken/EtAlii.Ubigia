namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public class TraversalParameters
    {
        public TraversalParameters(
            GraphPathPart part,
            ITraversalContext context, 
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
        public ITraversalContext Context { get; }
        public ExecutionScope Scope { get; }
        public IObserver<Identifier> Output { get; }
        public IObservable<Identifier> Input { get; }
    }
}