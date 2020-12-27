// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitOperand_rooted_path(GtlParser.Operand_rooted_pathContext context)
        {
            var pathSubjectParts = new List<PathSubjectPart>();

            foreach (var childContext in context.children)
            {
                var pathSubjectPart = childContext switch
                {
                    GtlParser.TraverserContext traverserContext => Visit(traverserContext) as PathSubjectPart,
                    _ => new ConstantPathSubjectPart(childContext.GetText()),
                    //_ => throw new ScriptParserException("The parser context could not be understood.")
                };
                pathSubjectParts.Add(pathSubjectPart);
            }

            return new RelativePathSubject(pathSubjectParts.ToArray());
        }

        public override object VisitOperand_non_rooted_path(GtlParser.Operand_non_rooted_pathContext context)
        {
            var pathSubjectParts = new List<PathSubjectPart>();

            foreach (var childContext in context.children)
            {
                var pathSubjectPart = childContext switch
                {
                    GtlParser.TraverserContext traverserContext => Visit(traverserContext) as PathSubjectPart,
                    _ => new ConstantPathSubjectPart(childContext.GetText()),
                    //_ => throw new ScriptParserException("The parser context could not be understood.")
                };
                pathSubjectParts.Add(pathSubjectPart);
            }

            return new RelativePathSubject(pathSubjectParts.ToArray());
        }

        public override object VisitTraverser(GtlParser.TraverserContext context)
        {
            return context.GetText() switch
            {
                "//" => new AllParentsPathSubjectPart(),
                "/" => new ParentPathSubjectPart(),
                "\\" => new ChildrenPathSubjectPart(),
                "\\\\" => new AllChildrenPathSubjectPart(),
                _ => throw new ScriptParserException("The path separator could not be understood.")
            };
        }
    }
}
