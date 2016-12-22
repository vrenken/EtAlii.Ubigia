namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class TimeRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootHandler[] _allowedPaths;

        public TimeRootHandlerMapper()
        {
            _name = "time";

            _allowedPaths = new IRootHandler[]
            {
                new TimeRootByPathBasedYyyymmddhhmmssHandler(), // 00: YYYY/MM/DD/HH/MM/SS
                new TimeRootByPathBasedYyyymmddhhmmHandler(), // 01: YYYY/MM/DD/HH/MM
                new TimeRootByPathBasedYyyymmddhhHandler(), // 02: YYYY/MM/DD/HH
                new TimeRootByPathBasedYyyymmddHandler(), // 03: YYYY/MM/DD
                new TimeRootByPathBasedYyyymmHandler(), // 04: YYYY/MM
                new TimeRootByPathBasedYyyyHandler(), // 05: YYYY

                new TimeRootByConstantBasedNow1Handler(), // 06: now
                new TimeRootByConstantBasedNow2Handler(), // 07: NOW
                new TimeRootByConstantBasedNow3Handler(), // 08: now

                new TimeRootByRegexBasedYyyymmddhhmmssHandler(), // 09: "YYYY-MM-DD HH:MM:SS"
                new TimeRootByRegexBasedYyyymmddhhmmHandler(), // 10: "YYYY-MM-DD HH:MM"
                new TimeRootByRegexBasedYyyymmddhhHandler(), // 11: "YYYY-MM-DD HH"
                new TimeRootByRegexBasedYyyymmddHandler(), // 12: "YYYY-MM-DD"
                new TimeRootByRegexBasedYyyymmHandler(), // 13: "YYYY-MM"
                new TimeRootByRegexBasedYyyyHandler(), // 14: "YYYY"
            };
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }

        //public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        //{
        //    if (processAsSubject)
        //    {
        //        if (argumentSet.Arguments.Length == 0)
        //        {
        //            output.OnCompleted();
        //        }
        //        else
        //        {
        //            input = argumentSet.Arguments[0] as IObservable<object>;
        //            if (input == null)
        //            {
        //                throw new ScriptProcessingException("Unable to convert arguments for Count function processing");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (argumentSet.Arguments.Length == 1)
        //        {
        //            throw new ScriptProcessingException("Unable to use arguments and input data for Count function processing");
        //        }
        //    }
        //    Process(context, input, scope, output);
        //}

        //private void Process(IFunctionContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        //{
        //    int result = 0;

        //    input.Subscribe(
        //        onError: (e) => output.OnError(e),
        //        onCompleted: () =>
        //        {
        //            output.OnNext(result);
        //            output.OnCompleted();
        //        },
        //        onNext: (o) =>
        //        {
        //            var converter = _converterSelector.Select(o);
        //            result += converter(context, scope, o);
        //        });
        //}

        //private int CountPath(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        //{
        //    var outputObservable = Observable.Create<object>(async outputObserver =>
        //    {
        //        await context.PathProcessor.Process(pathSubject, scope, outputObserver);

        //        return Disposable.Empty;
        //    });

        //    int result = 0;

        //    var task = Task.Run(async () =>
        //    {
        //        result = await outputObservable.Count();
        //    });
        //    task.Wait();

        //    return result;
        //}

        //private int CountObservable(IObservable<object> observable)
        //{

        //    int result = 0;

        //    var task = Task.Run(async () =>
        //    {
        //        result = await observable.Count();
        //    });
        //    task.Wait();

        //    return result;
        //}
    }
}