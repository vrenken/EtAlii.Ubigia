// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSubject_root(GtlParser.Subject_rootContext context)
        {
            var name = context.identifier().GetText();
            var result = new RootSubject(name);

            var parentContext = GetParent<GtlParser.SequenceContext>(context, out var parentOfChild);

            if (parentContext.GetChild(0) != parentOfChild)
            {
                throw new ScriptParserException("A root subject can only be used as first subject.");
            }

            var (_, after) = GetSiblings<GtlParser.SequenceContext>(context);
            var isAssignmentOperator = after.ChildCount == 1 &&
                                       after.GetChild(0) is GtlParser.Operator_assignContext;

            if (!isAssignmentOperator)
            {
                throw new ScriptParserException("Root subjects can only be modified using the assignment operator.");
            }

            return result;
        }

        public override object VisitSubject_root_definition(GtlParser.Subject_root_definitionContext context)
        {
            var type = context.GetText();
            var result = new RootDefinitionSubject(type);
            var (before, after) = GetSiblings<GtlParser.SequenceContext>(context);
            if (before == null)
            {
                throw new ScriptParserException("A root definition subject can not be used as first subject.");
            }
            var isAssignmentOperator = before.ChildCount == 1 &&
                                       before.GetChild(0) is GtlParser.Operator_assignContext;
            if (!isAssignmentOperator)
            {
                throw new ScriptParserException("Root definition subjects can only be used with the assignment operator.");
            }
            if (after != null)
            {
                throw new ScriptParserException("Root definition subjects can only be used as the last subject in a sequence.");
            }

            return result;
        }
    }
}
