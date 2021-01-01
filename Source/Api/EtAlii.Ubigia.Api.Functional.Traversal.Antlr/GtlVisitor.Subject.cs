// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSubject_rooted_path(GtlParser.Subject_rooted_pathContext context)
        {
            var root = (string)VisitIdentifier(context.identifier());
            return BuildRootedPathSubject(root, context.path_part());
        }

        private RootedPathSubject BuildRootedPathSubject(string root, GtlParser.Path_partContext[] partContexts)
        {
            var parts = partContexts
                .Select(partContext => (PathSubjectPart)Visit(partContext))
                .ToArray();

            return new RootedPathSubject(root, parts);

        }

        public override object VisitSubject_non_rooted_path(GtlParser.Subject_non_rooted_pathContext context)
        {
            return BuildNonRootedPathSubject(context.path_part());
        }

        private Subject BuildNonRootedPathSubject(GtlParser.Path_partContext[] partContexts)
        {
            var parts = partContexts
                .Select(partContext => (PathSubjectPart)Visit(partContext))
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

        public override object VisitSubject_variable(GtlParser.Subject_variableContext context)
        {
            var name = (string)VisitIdentifier(context.identifier());
            return new VariableSubject(name);
        }

        public override object VisitSubject_constant_string(GtlParser.Subject_constant_stringContext context)
        {
            var text = (string)VisitString_quoted(context.string_quoted());
            return new StringConstantSubject(text);
        }
    }
}
