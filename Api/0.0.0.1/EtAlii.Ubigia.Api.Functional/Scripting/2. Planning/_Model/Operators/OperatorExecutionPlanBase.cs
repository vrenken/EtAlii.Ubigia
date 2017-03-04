namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    public abstract class OperatorExecutionPlanBase : IOperatorExecutionPlan
    {
        protected ISubjectExecutionPlan Left { get; private set; }
        protected ISubjectExecutionPlan Right { get; private set; }

        public Type OutputType => _outputType.Value;
        private readonly Lazy<Type> _outputType;

        protected abstract Type GetOutputType();

        protected OperatorExecutionPlanBase(
            ISubjectExecutionPlan left,
            ISubjectExecutionPlan right)
        {
            Left = left ?? SubjectExecutionPlan.Empty;
            Right = right ?? SubjectExecutionPlan.Empty;

            _outputType = new Lazy<Type>(GetOutputType);
        }

        public IObservable<object> Execute(ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(outputObserver =>
            {
                var leftInput = Left.Execute(scope);
                var rightInput = Right.Execute(scope);

                var parameters = new OperatorParameters(scope, Left.Subject, Right.Subject, leftInput, rightInput, outputObserver);
                Execute(parameters);

                return Disposable.Empty;
            }).ToHotObservable();

            return outputObservable;
        }

        protected abstract void Execute(OperatorParameters parameters);
    }
}