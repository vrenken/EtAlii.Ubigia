namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByRegexBasedSeparatedYyyymmddhhmmssmmmHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByRegexBasedSeparatedYyyymmddhhmmssmmmHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] { new RegexPathSubjectPart(@"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}.\d{3}") };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var timeString = match[0].ToString();
            var year = Int32.Parse(timeString.Substring(0, 4));
            var month = Int32.Parse(timeString.Substring(5, 2));
            var day = Int32.Parse(timeString.Substring(8, 2));
            var hour = Int32.Parse(timeString.Substring(11, 2));
            var minute = Int32.Parse(timeString.Substring(14, 2));
            var second = Int32.Parse(timeString.Substring(17, 2));
            var millisecond = Int32.Parse(timeString.Substring(19, 3));

            var time = new DateTime(year, month, day, hour, minute, second, millisecond);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[]
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"), 
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(year.ToString("D4")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(month.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(day.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(hour.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(minute.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(second.ToString("D2")),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(millisecond.ToString("D3")),
                }
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);
        }
    }
}