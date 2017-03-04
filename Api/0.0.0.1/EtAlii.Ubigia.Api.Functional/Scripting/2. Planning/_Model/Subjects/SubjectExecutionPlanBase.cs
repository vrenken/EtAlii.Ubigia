namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    public abstract class SubjectExecutionPlanBase : ISubjectExecutionPlan
    {
        public Subject Subject { get; set; }
        public Type OutputType => _outputType.Value;
        private readonly Lazy<Type> _outputType;

        protected SubjectExecutionPlanBase(Subject subject)
        {
            Subject = subject;
            _outputType = new Lazy<Type>(GetOutputType);
        }

        protected abstract Type GetOutputType();

        public IObservable<object> Execute(ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(outputObserver =>
            {
                Execute(scope, outputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            return outputObservable;
        }
        protected abstract void Execute(ExecutionScope scope, IObserver<object> output);
    }
}