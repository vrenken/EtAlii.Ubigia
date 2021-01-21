namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    public class OperatorParameters
    {
        public ExecutionScope Scope { get; }
        public Subject LeftSubject { get; }
        public Subject RightSubject { get; }
        public IObservable<object> LeftInput { get; }
        public IObservable<object> RightInput { get; }
        public IObserver<object> Output { get; }

        public OperatorParameters(
            ExecutionScope scope,
            Subject leftSubject,
            Subject rightSubject,
            IObservable<object> leftInput,
            IObservable<object> rightInput,
            IObserver<object> output)
        {
            Scope = scope;
            LeftSubject = leftSubject;
            RightSubject = rightSubject;
            LeftInput = leftInput;
            RightInput = rightInput;
            Output = output;
        }
    }
}
