namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class TimeRootHandler : IRootHandler
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootSubHandler[] AllowedPaths { get { return _allowedPaths; } }
        private readonly IRootSubHandler[] _allowedPaths;

        internal const string YearFormatter = "YYYY";
        internal const string MonthFormatter = "MM";
        internal const string DayFormatter = "DD";
        internal const string HourFormatter = "HH";
        internal const string MinuteFormatter = "mm";
        internal const string SecondFormatter = "ss";

        public TimeRootHandler()
        {
            _name = "time";

            _allowedPaths = new IRootSubHandler[]
            {
                new TimeRootByPathBasedYYYYMMDDHHMMSSSubHandler(), // 00: YYYY/MM/DD/HH/MM/SS
                new TimeRootByPathBasedYYYYMMDDHHMMSubHandler(), // 01: YYYY/MM/DD/HH/MM
                new TimeRootByPathBasedYYYYMMDDHHSubHandler(), // 02: YYYY/MM/DD/HH
                new TimeRootByPathBasedYYYYMMDDSubHandler(), // 03: YYYY/MM/DD
                new TimeRootByPathBasedYYYYMMSubHandler(), // 04: YYYY/MM
                new TimeRootByPathBasedYYYYSubHandler(), // 05: YYYY

                new TimeRootByConstantBasedNow1SubHandler(), // 06: now
                new TimeRootByConstantBasedNow2SubHandler(), // 07: NOW
                new TimeRootByConstantBasedNow3SubHandler(), // 08: now

                new TimeRootByRegexBasedYYYYMMDDHHMMSSSubHandler(), // 09: "YYYY-MM-DD HH:MM:SS"
                new TimeRootByRegexBasedYYYYMMDDHHMMSubHandler(), // 10: "YYYY-MM-DD HH:MM"
                new TimeRootByRegexBasedYYYYMMDDHHSubHandler(), // 11: "YYYY-MM-DD HH"
                new TimeRootByRegexBasedYYYYMMDDSubHandler(), // 12: "YYYY-MM-DD"
                new TimeRootByRegexBasedYYYYMMSubHandler(), // 13: "YYYY-MM"
                new TimeRootByRegexBasedYYYYSubHandler(), // 14: "YYYY"
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