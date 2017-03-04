namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByRegexBasedYyyymmddHandler : IRootHandler
    {

        public PathSubjectPart[] Template => _template;
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByRegexBasedYyyymmddHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] { new RegexPathSubjectPart(@"\d{4}\d{2}\d{2}") };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var timeString = match[0].ToString();
            var year = Int32.Parse(timeString.Substring(0, 4));
            var month = Int32.Parse(timeString.Substring(4, 2));
            var day = Int32.Parse(timeString.Substring(6, 2));

            var time = new DateTime(year, month, day);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[] 
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new IsParentOfPathSubjectPart(), new WildcardPathSubjectPart("*"), // hour
                    new IsParentOfPathSubjectPart(), new WildcardPathSubjectPart("*"), // minute
                    new IsParentOfPathSubjectPart(), new WildcardPathSubjectPart("*"), // second
                    new IsParentOfPathSubjectPart(), new WildcardPathSubjectPart("*"), // millisecond
                }
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);
        }
    }
}