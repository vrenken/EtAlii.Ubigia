﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public abstract class OperatorExecutionPlanBase : IOperatorExecutionPlan
    {
        protected ISubjectExecutionPlan Left { get; }
        protected ISubjectExecutionPlan Right { get; }

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

        public Task<IObservable<object>> Execute(ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                var leftInput = await Left.Execute(scope);
                var rightInput = await Right.Execute(scope);

                var parameters = new OperatorParameters(scope, Left.Subject, Right.Subject, leftInput, rightInput, outputObserver);
                await Execute(parameters);

                return Disposable.Empty;
            }).ToHotObservable();

            return Task.FromResult(outputObservable);
        }

        protected abstract Task Execute(OperatorParameters parameters);
    }
}