// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;

internal class TimeRootByRegexBasedNowHandler : IRootHandler
{
    public PathSubjectPart[] Template { get; }

    private readonly ITimePreparer _timePreparer;

    public TimeRootByRegexBasedNowHandler(ITimePreparer timePreparer)
    {
        _timePreparer = timePreparer;
        Template = new PathSubjectPart[] { new RegexPathSubjectPart(@"now") };
    }

    public void Process(IScriptProcessingContext context, string root, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
    {
        var time = DateTime.Now;

        _timePreparer.Prepare(context, scope, time);

        var parts = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart(root),
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
