namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByPathBasedYyyymmddhhmmssmmmHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        private readonly ITimePreparer _timePreparer;

        public TimeRootByPathBasedYyyymmddhhmmssmmmHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            Template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.SecondFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MillisecondFormatter)
            };
        }

        public void Process(IProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var year = int.Parse(match[0].ToString());
            var month = int.Parse(match[2].ToString());
            var day = int.Parse(match[4].ToString());
            var hour = int.Parse(match[6].ToString());
            var minute = int.Parse(match[8].ToString());
            var second = int.Parse(match[10].ToString());
            var millisecond = int.Parse(match[12].ToString());

            var time = new DateTime(year, month, day, hour, minute, second, millisecond);
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
                    new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:fff}"),
                }
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);

            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}