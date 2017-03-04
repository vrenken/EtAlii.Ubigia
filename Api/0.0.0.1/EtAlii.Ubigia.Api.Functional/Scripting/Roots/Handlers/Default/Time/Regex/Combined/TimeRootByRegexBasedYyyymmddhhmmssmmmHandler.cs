namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByRegexBasedYyyymmddhhmmssmmmHandler : IRootHandler
    {

        public PathSubjectPart[] Template => _template;
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByRegexBasedYyyymmddhhmmssmmmHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] { new RegexPathSubjectPart(@"\d{4}\d{2}\d{2}\d{2}\d{2}\d{2}\d{3}") };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var timeString = match[0].ToString();
            var year = Int32.Parse(timeString.Substring(0, 4));
            var month = Int32.Parse(timeString.Substring(4, 2));
            var day = Int32.Parse(timeString.Substring(6, 2));
            var hour = Int32.Parse(timeString.Substring(8, 2));
            var minute = Int32.Parse(timeString.Substring(10, 2));
            var second = Int32.Parse(timeString.Substring(12, 2));
            var millisecond = Int32.Parse(timeString.Substring(14, 3));

            var time = new DateTime(year, month, day, hour, minute, second, millisecond);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[]
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:HH}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:mm}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:ss}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:fff}"),
                }
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);
        }
    }
}