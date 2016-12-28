namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    internal class TimeRootByPathBasedYyyyHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;
        private readonly ITimePreparer _timePreparer;

        public TimeRootByPathBasedYyyyHandler(ITimePreparer timePreparer)
        {
            _timePreparer = timePreparer;

            _template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
        }

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var year = Int32.Parse(match[0].ToString());

            var time = new DateTime(year, 1, 1);
            _timePreparer.Prepare(context, scope, time);

            var parts = new PathSubjectPart[] 
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart(year.ToString("D4")),
                    new IsParentOfPathSubjectPart(), new WildcardPathSubjectPart("*"), // month
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