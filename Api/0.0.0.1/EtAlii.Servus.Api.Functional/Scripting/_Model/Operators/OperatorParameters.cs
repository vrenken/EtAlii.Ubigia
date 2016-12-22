namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class OperatorParameters
    {
        public ExecutionScope Scope { get; set; }
        public Subject LeftSubject { get; set; }
        public Subject RightSubject { get; set; }
        public IObservable<object> LeftInput { get; set; }
        public IObservable<object> RightInput { get; set; }
        public IObserver<object> Output { get; set; }

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