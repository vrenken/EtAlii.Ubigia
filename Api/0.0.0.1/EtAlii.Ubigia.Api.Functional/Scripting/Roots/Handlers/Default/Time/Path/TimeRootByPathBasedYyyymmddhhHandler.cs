namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByPathBasedYyyymmddhhHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        private readonly ITimePreparer _timePreparer;

        public TimeRootByPathBasedYyyymmddhhHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            Template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var year = Int32.Parse(match[0].ToString());
            var month = Int32.Parse(match[2].ToString());
            var day = Int32.Parse(match[4].ToString());
            var hour = Int32.Parse(match[6].ToString());

            var time = new DateTime(year, month, day, hour, 0, 0, 0);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[] 
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:dd}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:HH}"),
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