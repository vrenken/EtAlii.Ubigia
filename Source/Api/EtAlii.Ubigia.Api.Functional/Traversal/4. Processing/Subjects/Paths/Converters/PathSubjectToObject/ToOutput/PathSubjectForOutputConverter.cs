// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    internal class PathSubjectForOutputConverter : IPathSubjectForOutputConverter
    {
        private readonly IPathProcessor _pathProcessor;
        private readonly IEntriesToDynamicNodesConverter _entriesToDynamicNodesConverter;

        public PathSubjectForOutputConverter(
            IPathProcessor pathProcessor,
            IEntriesToDynamicNodesConverter entriesToDynamicNodesConverter)
        {
            _pathProcessor = pathProcessor;
            _entriesToDynamicNodesConverter = entriesToDynamicNodesConverter;
        }

        public void Convert(
            PathSubject pathSubject,
            ExecutionScope scope,
            IObserver<object> output)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await _pathProcessor.Process(pathSubject, scope, outputObserver).ConfigureAwait(false);

                return Disposable.Empty;
            });

            outputObservable.SubscribeAsync(
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: async o =>
                {
                    try
                    {
                        var entry = await _entriesToDynamicNodesConverter.Convert((IReadOnlyEntry)o, scope).ConfigureAwait(false);
                        output.OnNext(entry);
                    }
                    catch (Exception e)
                    {
                        var message = "Failure converting path subject items for output";
                        output.OnError(new InvalidOperationException(message, e));
                    }
                });
        }
    }
}
