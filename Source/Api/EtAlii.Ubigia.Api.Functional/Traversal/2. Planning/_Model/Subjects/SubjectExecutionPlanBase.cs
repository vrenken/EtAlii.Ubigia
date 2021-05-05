namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public abstract class SubjectExecutionPlanBase : ISubjectExecutionPlan
    {
        public Subject Subject { get; }
        public Type OutputType => _outputType.Value;
        private readonly Lazy<Type> _outputType;

        protected SubjectExecutionPlanBase(Subject subject)
        {
            Subject = subject;
            _outputType = new Lazy<Type>(GetOutputType);
        }

        protected abstract Type GetOutputType();

        public Task<IObservable<object>> Execute(ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await Execute(scope, outputObserver).ConfigureAwait(false);

                return Disposable.Empty;
            }).ToHotObservable();

            return Task.FromResult(outputObservable);
        }
        protected abstract Task Execute(ExecutionScope scope, IObserver<object> output);
    }
}
