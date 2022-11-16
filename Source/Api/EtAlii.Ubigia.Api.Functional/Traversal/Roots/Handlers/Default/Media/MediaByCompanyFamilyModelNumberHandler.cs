//  Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class MediaByCompanyFamilyModelNumberHandler : IRootHandler
    {
        public PathSubjectPart[] Template { get; }

        public MediaByCompanyFamilyModelNumberHandler()
        {
            // media:COMPANY/FAMILY/MODEL/NUMBER
            Template = new PathSubjectPart[]
            {
                new TypedPathSubjectPart(MediaPathFormatter.CompanyNameFormatter), new ParentPathSubjectPart(),
                new TypedPathSubjectPart(MediaPathFormatter.ProductFamilyNameFormatter), new ParentPathSubjectPart(),
                new TypedPathSubjectPart(MediaPathFormatter.ProductModelNameFormatter), new ParentPathSubjectPart(),
                new TypedPathSubjectPart(MediaPathFormatter.ProductNumberFormatter)
            };
        }

        public void Process(IScriptProcessingContext context, string root, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart(root), new ParentPathSubjectPart() }
                .Concat(match)
                .Concat(rest)
                .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}
