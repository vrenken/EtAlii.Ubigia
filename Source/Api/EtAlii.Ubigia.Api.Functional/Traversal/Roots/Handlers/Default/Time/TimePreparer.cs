// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    internal class TimePreparer : ITimePreparer
    {
        public void Prepare(IScriptProcessingContext context, ExecutionScope scope, DateTime time)
        {
            // TODO: Fix the TimePreparer so that it no longer requires a flush of the ExecutionScope (and cache).
            // Using an empty execution scope should not be needed. The one provided should be used.
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/98
            // scope = new ExecutionScope();

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
                context.PathSubjectForOutputConverter.Convert(pathToAddTo, scope, leftInputObserver);

                return Disposable.Empty;
            }).ToHotObservable();

            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                var rightInput = Observable.Empty<object>();
                var parameters = new OperatorParameters(scope, pathToAddTo, pathToAdd, leftInput, rightInput, outputObserver);
                await context.AddRelativePathToExistingPathProcessor.Process(parameters).ConfigureAwait(false);

                return Disposable.Empty;
            }).ToHotObservable();

            outputObservable.Wait();
        }
    }
}
