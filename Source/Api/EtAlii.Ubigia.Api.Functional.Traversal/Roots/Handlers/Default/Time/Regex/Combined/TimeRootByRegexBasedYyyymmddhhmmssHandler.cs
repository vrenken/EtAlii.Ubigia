namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class TimeRootByRegexBasedYyyymmddhhmmssHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        private readonly ITimePreparer _timePreparer;

        public TimeRootByRegexBasedYyyymmddhhmmssHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            Template = new PathSubjectPart[] { new RegexPathSubjectPart(@"\d{4}\d{2}\d{2}\d{2}\d{2}\d{2}") };
        }

        public void Process(IScriptProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var timeString = match[0].ToString();
            var year = int.Parse(timeString.Substring(0, 4));
            var month = int.Parse(timeString.Substring(4, 2));
            var day = int.Parse(timeString.Substring(6, 2));
            var hour = int.Parse(timeString.Substring(8, 2));
            var minute = int.Parse(timeString.Substring(10, 2));
            var second = int.Parse(timeString.Substring(12, 2));

            var time = new DateTime(year, month, day, hour, minute, second, 0);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[]
                {
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart("Time"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:HH}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:mm}"),
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:ss}"),
                    new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // millisecond
                }
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}