namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByRegexBasedNowHandler : IRootHandler
    {
        public PathSubjectPart[] Template { get; }

        private readonly ITimePreparer _timePreparer;

        public TimeRootByRegexBasedNowHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;
            Template = new PathSubjectPart[] { new RegexPathSubjectPart(@"now") };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var time = DateTime.Now;

            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"), new IsParentOfPathSubjectPart() }
                .Concat(new PathSubjectPart[]
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:HH}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:mm}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:ss}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:fff}"),
                })
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);

        }
    }
}