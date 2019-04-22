namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class TimePreparer : ITimePreparer
    {
        public void Prepare(IRootContext context, ExecutionScope scope, DateTime time)
        {
            // TODO: using an empty execution scope should not be needed.
            scope = new ExecutionScope(false);

            var pathToAddTo = new AbsolutePathSubject(new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart("Time") });

            var pathToAdd = new RelativePathSubject(new PathSubjectPart[] 
            {
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:HH}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:mm}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:ss}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:fff}"),
            });

            var leftInput = Observable.Create<object>(leftInputObserver =>
            {
                context.Converter.Convert(pathToAddTo, scope, leftInputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                var rightInput = Observable.Empty<object>();
                var parameters = new OperatorParameters(scope, pathToAddTo, pathToAdd, leftInput, rightInput, outputObserver);
                await context.AddByNameToExistingPathProcessor.Process(parameters);

                return Disposable.Empty;
            }).ToHotObservable();

            outputObservable.Wait();
        }
    }
}