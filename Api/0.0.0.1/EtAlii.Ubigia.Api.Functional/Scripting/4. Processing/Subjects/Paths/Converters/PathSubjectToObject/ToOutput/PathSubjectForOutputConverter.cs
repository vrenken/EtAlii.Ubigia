namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

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
                await _pathProcessor.Process(pathSubject, scope, outputObserver);

                return Disposable.Empty;
            });

            outputObservable.Subscribe(
                onError: (e) => output.OnError(e),
                onCompleted: () => output.OnCompleted(),
                onNext: o =>
                {
                    var task = Task.Run(async () =>
                    {
                        var entry = await _entriesToDynamicNodesConverter.Convert((IReadOnlyEntry)o, scope);
                        output.OnNext(entry);
                    });
                    task.Wait();
                });
        }
    }
}
