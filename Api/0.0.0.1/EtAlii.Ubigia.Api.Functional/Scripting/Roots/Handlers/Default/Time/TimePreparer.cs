namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    internal class TimePreparer : ITimePreparer
    {
        public void Prepare(IRootContext context, ExecutionScope scope, DateTime time)
        {
            // TODO: using an empty execution scope should not be needed.
            scope = new ExecutionScope(false);

            var pathToAddTo = new AbsolutePathSubject(new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time") });

            var pathToAdd = new RelativePathSubject(new PathSubjectPart[] 
            {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:HH}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:mm}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:ss}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:fff}"),
            });

            var leftInput = Observable.Create<object>(leftInputObserver =>
            {
                context.Converter.Convert(pathToAddTo, scope, leftInputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            var outputObservable = Observable.Create<object>(outputObserver =>
            {
                var rightInput = Observable.Empty<object>();
                var parameters = new OperatorParameters(scope, pathToAddTo, pathToAdd, leftInput, rightInput, outputObserver);
                context.AddByNameToRelativePathProcessor.Process(parameters);

                return Disposable.Empty;
            }).ToHotObservable();

            outputObservable.Wait();
        }
    }
}