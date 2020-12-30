// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSubject_rooted_path(GtlParser.Subject_rooted_pathContext context)
        {
            var root = context.path_part_root().GetText().TrimEnd(':');

            var parts = context
                .path_part()
                .Select(childContext => (PathSubjectPart)Visit(childContext))
                .ToArray();

            return new RootedPathSubject(root, parts);
        }

        public override object VisitSubject_non_rooted_path(GtlParser.Subject_non_rooted_pathContext context)
        {
            var parts = context.children
                .Select(childContext => (PathSubjectPart)Visit(childContext))
                .ToArray();

            // A relative path with the length of 1 should not be parsed as a path but as a string constant.
            var lengthIsOne = parts.Length == 1;
            return lengthIsOne switch
            {
                true when parts[0] is ConstantPathSubjectPart => new StringConstantSubject( ((ConstantPathSubjectPart)parts[0]).Name),
                true when parts[0] is VariablePathSubjectPart => new VariableSubject(((VariablePathSubjectPart)parts[0]).Name),
                _ => parts[0] is ParentPathSubjectPart
                    ? new AbsolutePathSubject(parts)
                    : (NonRootedPathSubject)new RelativePathSubject(parts)
            };
        }

        public override object VisitPath_part_match(GtlParser.Path_part_matchContext context)
        {
            var constantPart = context.PATH_PART_MATCHER_CONSTANT();
            if (constantPart != null)
            {
                var text = constantPart.GetText();
                return new ConstantPathSubjectPart(text);
            }

            // var wildcardPart = context.PATH_PART_MATCHER_WILDCARD();
            // if (wildcardPart != null)
            // {
            //     wildcardPart.
            // }

            var pathSubjectPart = base.Visit(context);
            if (pathSubjectPart is not PathSubjectPart)
            {
                throw new ScriptParserException($"The path subject part could not be understood: {context.GetText()}" );
            }

            return pathSubjectPart;
        }

        public override object VisitSubject_variable(GtlParser.Subject_variableContext context)
        {
            var name = context.IDENTITY().GetText();
            return new VariableSubject(name);
        }
    }
}
