namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByRegexBasedYyyymmddhhHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByRegexBasedYyyymmddhhHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] { new RegexPathSubjectPart(@"\d{4}\d{2}\d{2}\d{2}") };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var timeString = match[0].ToString();
            var year = Int32.Parse(timeString.Substring(0, 4));
            var month = Int32.Parse(timeString.Substring(4, 2));
            var day = Int32.Parse(timeString.Substring(6, 2));
            var hour = Int32.Parse(timeString.Substring(8, 2));

            var time = new DateTime(year, month, day, hour, 0, 0);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[] 
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"), 
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(year.ToString("D4")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(month.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(day.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(hour.ToString("D2")),
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