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

        public GraphPathPart Part { get; private set; }
        public ITraversalContext Context { get; private set; }
        public ExecutionScope Scope { get; private set; }
        public IObserver<Identifier> Output { get; private set; }
        public IObservable<Identifier> Input { get; private set; }
    }
}