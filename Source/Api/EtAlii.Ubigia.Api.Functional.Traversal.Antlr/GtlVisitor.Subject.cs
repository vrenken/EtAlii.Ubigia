// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
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
            var constantPart = context.PATH_PART_MATCHER_CONSTANT();
            if (constantPart != null)
            {
                var text = constantPart.GetText();
                return new ConstantPathSubjectPart(text);
            }

            var variablePart = context.PATH_PART_MATCHER_VARIABLE();
            if (variablePart != null)
            {
                var text = variablePart.GetText();
                return new VariablePathSubjectPart(text);
            }

            var identifierPart = context.PATH_PART_MATCHER_IDENTIFIER();
            if (identifierPart != null)
            {
                var parts = identifierPart
                    .GetText()
                    .Substring(1)
                    .Split(".");
                var storage = Guid.Parse(parts[0]);
                var account = Guid.Parse(parts[1]);
                var space = Guid.Parse(parts[2]);

                var era = ulong.Parse(parts[3]);
                var period = ulong.Parse(parts[4]);
                var moment = ulong.Parse(parts[5]);

                var identifier = Identifier.Create(storage, account, space, era, period, moment);

                return new IdentifierPathSubjectPart(identifier);
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
    }
}
