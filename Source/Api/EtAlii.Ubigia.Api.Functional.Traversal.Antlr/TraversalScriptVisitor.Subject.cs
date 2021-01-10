// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class TraversalScriptVisitor
    {
        public override object VisitRooted_path(TraversalScriptParser.Rooted_pathContext context)
        {
            var root = (string)VisitIdentifier(context.identifier());
            return BuildRootedPathSubject(root, context.path_part());
        }

        private RootedPathSubject BuildRootedPathSubject(string root, TraversalScriptParser.Path_partContext[] partContexts)
        {
            var parts = partContexts
                .Select(partContext => (PathSubjectPart)Visit(partContext))
                .ToArray();

            return new RootedPathSubject(root, parts);

        }

        public override object VisitNon_rooted_path(TraversalScriptParser.Non_rooted_pathContext context)
        {
            var result = BuildNonRootedPathSubject(context.path_part());
            return result;
        }

        private Subject BuildNonRootedPathSubject(TraversalScriptParser.Path_partContext[] partContexts)
        {
            var parts = partContexts
                .Select(partContext => (PathSubjectPart)Visit(partContext))
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
                    : (NonRootedPathSubject)new RelativePathSubject(parts);
            }

            return result;
        }

        public override object VisitSubject_variable(TraversalScriptParser.Subject_variableContext context)
        {
            var name = (string)VisitIdentifier(context.identifier());
            return new VariableSubject(name);
        }

        public override object VisitSubject_constant_string(TraversalScriptParser.Subject_constant_stringContext context)
        {
            var text = (string)VisitString_quoted(context.string_quoted());
            return new StringConstantSubject(text);
        }

        public override object VisitPath_part_matcher_regex(TraversalScriptParser.Path_part_matcher_regexContext context)
        {
            var text = (string)VisitString_quoted_non_empty(context.string_quoted_non_empty());
            var result = new RegexPathSubjectPart(text);
            return result;
        }
    }
}
