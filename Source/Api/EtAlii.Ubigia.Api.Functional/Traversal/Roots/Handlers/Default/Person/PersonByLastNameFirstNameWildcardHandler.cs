// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class PersonByLastNameFirstNameWildcardHandler : IRootHandler
    {

        public PathSubjectPart[] Template { get; }

        public PersonByLastNameFirstNameWildcardHandler()
        {
            Template = new PathSubjectPart[]
            {
                new TypedPathSubjectPart(NamePathFormatter.LastNameFormatter), new ParentPathSubjectPart(),
                new WildcardPathSubjectPart("*"),
            };
        }

        public void Process(IScriptProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            var parts = new PathSubjectPart[] { new ParentPathSubjectPart(), new ConstantPathSubjectPart("Person"), new ParentPathSubjectPart() }
               .Concat(match)
               .Concat(rest)
               .ToArray();
            var path = new AbsolutePathSubject(parts);
            context.PathSubjectForOutputConverter.Convert(path, scope, output);
        }
    }
}
