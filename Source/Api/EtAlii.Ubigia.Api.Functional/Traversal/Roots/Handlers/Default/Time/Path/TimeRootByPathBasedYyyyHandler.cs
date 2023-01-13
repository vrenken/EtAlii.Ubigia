// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;

internal class TimeRootByPathBasedYyyyHandler : IRootHandler
{

    public PathSubjectPart[] Template { get; }

    private readonly ITimePreparer _timePreparer;

    public TimeRootByPathBasedYyyyHandler(ITimePreparer timePreparer)
    {
        _timePreparer = timePreparer;

        Template = new PathSubjectPart[] {
            new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
        };
    }

    public void Process(IScriptProcessingContext context, string root, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
    {
        var year = int.Parse(match[0].ToString());

        var time = new DateTime(year, 1, 1);
        _timePreparer.Prepare(context, scope, time);

        var parts = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(), new ConstantPathSubjectPart(root),
                new ParentPathSubjectPart(), new ConstantPathSubjectPart($"{time:yyyy}"),
                new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // month
                new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // day
                new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // hour
                new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // minute
                new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // second
                new ParentPathSubjectPart(), new WildcardPathSubjectPart("*"), // millisecond
            }
            .Concat(rest)
            .ToArray();
        var path = new AbsolutePathSubject(parts);

        context.PathSubjectForOutputConverter.Convert(path, scope, output);
    }
}
