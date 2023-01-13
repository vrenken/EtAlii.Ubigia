// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Linq;

internal class MediaByCompanyFamilyModelHandler : IRootHandler
{
    public PathSubjectPart[] Template { get; }

    public MediaByCompanyFamilyModelHandler()
    {
        // media:COMPANY/FAMILY/MODEL
        Template = new PathSubjectPart[]
        {
            new TypedPathSubjectPart(MediaPathFormatter.CompanyNameFormatter), new ParentPathSubjectPart(),
            new TypedPathSubjectPart(MediaPathFormatter.ProductFamilyNameFormatter), new ParentPathSubjectPart(),
            new TypedPathSubjectPart(MediaPathFormatter.ProductModelNameFormatter)
        };
    }

    public void Process(IScriptProcessingContext context, string root, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
    {
        var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart(root), new ParentPathSubjectPart() }
            .Concat(match)
            .Concat(new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart("000") })
            .Concat(rest)
            .ToArray();
        var path = new AbsolutePathSubject(parts);
        context.PathSubjectForOutputConverter.Convert(path, scope, output);
    }
}
