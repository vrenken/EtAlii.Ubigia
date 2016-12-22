namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    public class TimeRootByRegexBasedNowHandler : IRootHandler
    {
        public PathSubjectPart[] Template { get { return _template; } }
        private readonly PathSubjectPart[] _template;

        public TimeRootByRegexBasedNowHandler()
        {
            _template = new PathSubjectPart[] { new RegexPathSubjectPart(@"now") };
        }
        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var now = DateTime.Now;

            var parts = new PathSubjectPart[] { new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart("Time"), new IsParentOfPathSubjectPart() }
                .Concat(new PathSubjectPart[]
                {
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{now:yyyy}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{now:MM}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{now:dd}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{now:HH}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{now:mm}"),
                    new IsParentOfPathSubjectPart(), new ConstantPathSubjectPart($"{now:ss}"),
                })
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.Converter.Convert(path, scope, output);

        }
    }
}