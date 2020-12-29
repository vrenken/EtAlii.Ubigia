// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSubject_rooted_path(GtlParser.Subject_rooted_pathContext context)
        {
            var pathSubjectParts = new List<PathSubjectPart>();

            foreach (var childContext in context.children)
            {
                var pathSubjectPart = childContext switch
                {
                    GtlParser.Path_part_traverserContext traverserContext => Visit(traverserContext) as PathSubjectPart,
                    _ => new ConstantPathSubjectPart(childContext.GetText()),
                    //_ => throw new ScriptParserException("The parser context could not be understood.")
                };
                pathSubjectParts.Add(pathSubjectPart);
            }

            return new RelativePathSubject(pathSubjectParts.ToArray());
        }

        public override object VisitSubject_non_rooted_path(GtlParser.Subject_non_rooted_pathContext context)
        {
            var parts = context.children
                .Select(childContext => (PathSubjectPart)Visit(childContext))
                .ToArray();
            return new RelativePathSubject(parts);
        }

        public override object VisitPath_part_traverser(GtlParser.Path_part_traverserContext context)
        {
            // Hierarchical
            if (context.PATH_PART_TRAVERSER_PARENT() != null) return new ParentPathSubjectPart();
            if (context.PATH_PART_TRAVERSER_PARENTS_ALL() != null) return new AllParentsPathSubjectPart();
            if (context.PATH_PART_TRAVERSER_CHILDREN() != null) return new ChildrenPathSubjectPart();
            if (context.PATH_PART_TRAVERSER_CHILDREN_ALL() != null) return new AllChildrenPathSubjectPart();

            // Temporal
            if (context.PATH_PART_TRAVERSER_DOWNDATE() != null) return new DowndatePathSubjectPart();
            if (context.PATH_PART_TRAVERSER_UPDATES() != null) return new UpdatesPathSubjectPart();
            // if (context.PATH_PART_TRAVERSER_DOWNDATES_OLDEST() != null) return new OldestPathSubjectPart(); // TODO: This is fishy, we mix and mingle all and oldest.
            // if (context.PATH_PART_TRAVERSER_DOWNDATES_ALL() != null) return new AllDowndatesPathSubjectPart();
            // if (context.PATH_PART_TRAVERSER_UPDATES_NEWEST() != null) return new NewestPathSubjectPart();
            // if (context.PATH_PART_TRAVERSER_UPDATES_ALL() != null) return new AllUpdatesPathSubjectPart();

            // Sequential
            if (context.PATH_PART_TRAVERSER_PREVIOUS_SINGLE() != null) return new PreviousPathSubjectPart();
            if (context.PATH_PART_TRAVERSER_NEXT_SINGLE() != null) return new NextPathSubjectPart();
            // if (context.PATH_PART_TRAVERSER_PREVIOUS_ALL() != null) return new AllPreviousPathSubjectPart(); // This is fishy, we mix and mingle all and oldest.
            // if (context.PATH_PART_TRAVERSER_PREVIOUS_FIRST() != null) return new FirstPathSubjectPart();
            // if (context.PATH_PART_TRAVERSER_NEXT_ALL() != null) return new AllNextPathSubjectPart();
            // if (context.PATH_PART_TRAVERSER_NEXT_LAST() != null) return new LastPathSubjectPart();

            throw new ScriptParserException($"The path traverser part could not be understood: {context.GetText()}" );
        }

        public override object VisitPath_part_match(GtlParser.Path_part_matchContext context)
        {
            var constant = context.PATH_PART_MATCHER_CONSTANT();
            if (constant != null)
            {
                var text = constant.GetText();
                return new ConstantPathSubjectPart(text);
            }

            var variable = context.PATH_PART_MATCHER_VARIABLE();
            if (variable != null)
            {
                var text = variable.GetText();
                return new VariablePathSubjectPart(text);
            }

            // var wildcard = context.PATH_PART_MATCHER_WILDCARD();
            // if (wildcard != null)
            // {
            //     wildcard.
            // }

            // var identifier = context.PATH_PART_MATCHER_IDENTIFIER();
            // if (identifier != null)
            // {
            //     var text = identifier.GetText();
            //     return new new IdentifierPathSubjectPart() VariablePathSubjectPart(text);
            // }

            return base.Visit(context);
        }

    }
}
