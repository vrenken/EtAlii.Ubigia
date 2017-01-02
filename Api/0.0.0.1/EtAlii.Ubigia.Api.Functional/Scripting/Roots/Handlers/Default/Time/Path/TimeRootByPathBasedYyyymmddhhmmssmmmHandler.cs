namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByPathBasedYyyymmddhhmmssmmmHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByPathBasedYyyymmddhhmmssmmmHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MinuteFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.SecondFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MillisecondFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var year = Int32.Parse(match[0].ToString());
            var month = Int32.Parse(match[2].ToString());
            var day = Int32.Parse(match[4].ToString());
            var hour = Int32.Parse(match[6].ToString());
            var minute = Int32.Parse(match[8].ToString());
            var second = Int32.Parse(match[10].ToString());
            var millisecond = Int32.Parse(match[12].ToString());

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