namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByPathBasedYyyymmHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByPathBasedYyyymmHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new IsParentOfPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var year = Int32.Parse(match[0].ToString());
            var month = Int32.Parse(match[2].ToString());

            var time = new DateTime(year, month, 1);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[] 
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{time:MM}"),
                    new IsParentOfPathSubjectPart(), new WildcardPathSubjectPart("*"), // day
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