// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

// This file is shared by both the traversal and context projects.

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        public override object VisitRooted_path(UbigiaParser.Rooted_pathContext context)
        {
            var root = (string)VisitIdentifier(context.identifier());
            return BuildRootedPathSubject(root, context.path_part());
        }

        private RootedPathSubject BuildRootedPathSubject(string root, UbigiaParser.Path_partContext[] partContexts)
        {
            var parts = partContexts
                .Select(partContext => (PathSubjectPart)Visit(partContext))
                .ToArray();

            return new RootedPathSubject(root, parts);

        }

        public override object VisitNon_rooted_path(UbigiaParser.Non_rooted_pathContext context)
        {
            var result = BuildNonRootedPathSubject(context.path_part());
            return result;
        }

        private Subject BuildNonRootedPathSubject(UbigiaParser.Path_partContext[] partContexts)
        {
            var parts = partContexts
                .Select(partContext => (PathSubjectPart)VisitPath_part(partContext))
                .ToArray();

            Subject result;
            // A relative path with the length of 1 should not be parsed as a path but as a string constant.
            var lengthIsOne = parts.Length == 1;

            var firstPart = parts[0];
            if (lengthIsOne && firstPart is ConstantPathSubjectPart constantPathSubjectPart)
            {
                result = new StringConstantSubject(constantPathSubjectPart.Name);
            }
            else if (lengthIsOne && firstPart is VariablePathSubjectPart variablePathSubjectPart)
            {
                result = new VariableSubject(variablePathSubjectPart.Name);
            }
            else
            {
                result = firstPart is ParentPathSubjectPart
                    ? new AbsolutePathSubject(parts)
                    : new RelativePathSubject(parts);
            }

            return result;
        }
    }
}
