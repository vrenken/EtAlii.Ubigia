// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;

internal class TimeRootByRegexBasedSeparatedYyyymmddhhmmssmmmHandler : IRootHandler
{

    public PathSubjectPart[] Template { get; }

    private readonly ITimePreparer _timePreparer;

    public TimeRootByRegexBasedSeparatedYyyymmddhhmmssmmmHandler(ITimePreparer timePreparer)
    {
        _timePreparer = timePreparer;

        Template = new PathSubjectPart[] { new RegexPathSubjectPart(@"\d{4}-\d{2}-\d{2}[\sT]\d{2}:\d{2}:\d{2}.\d{3}") };
    }

    public void Process(IScriptProcessingContext context, string root, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
    {
        var timeString = match[0].ToString();
        var year = int.Parse(timeString.Substring(0, 4));
        var month = int.Parse(timeString.Substring(5, 2));
        var day = int.Parse(timeString.Substring(8, 2));
        var hour = int.Parse(timeString.Substring(11, 2));
        var minute = int.Parse(timeString.Substring(14, 2));
        var second = int.Parse(timeString.Substring(17, 2));
        var millisecond = int.Parse(timeString.Substring(20, 3));

        var time = new DateTime(year, month, day, hour, minute, second, millisecond);
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
