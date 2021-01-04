// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitPath_part_matcher_traversing_wildcard(GtlParser.Path_part_matcher_traversing_wildcardContext context)
        {
            var number = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned());
            var result = new TraversingWildcardPathSubjectPart(number);

            var (before, after, _) = ParseTreeHelper.GetPathSiblings(context);

            if (before is GtlParser.Path_part_matcher_constantContext || after is GtlParser.Path_part_matcher_constantContext ||
                before is GtlParser.Path_part_matcher_wildcardContext || after is GtlParser.Path_part_matcher_wildcardContext ||
                before is GtlParser.Path_part_matcher_tagContext || after is GtlParser.Path_part_matcher_tagContext ||
                before is GtlParser.Path_part_matcher_traversing_wildcardContext || after is GtlParser.Path_part_matcher_traversing_wildcardContext)
            {
                throw new ScriptParserException("A traversing wildcard path part cannot be combined with other constant, tagged, wildcard or string path parts.");
            }

            return result;
        }

        public override object VisitPath_part_matcher_wildcard(GtlParser.Path_part_matcher_wildcardContext context)
        {
            var text = context.GetText();
            var result = new WildcardPathSubjectPart(text);

            var isNonRootedPathSubject = context.Parent.Parent is GtlParser.Subject_non_rooted_pathContext;

            var (before, after, first) = ParseTreeHelper.GetPathSiblings(context);
            if (before is GtlParser.Path_part_matcher_constantContext || after is GtlParser.Path_part_matcher_constantContext ||
                before is GtlParser.Path_part_matcher_wildcardContext || after is GtlParser.Path_part_matcher_wildcardContext ||
                before is GtlParser.Path_part_matcher_tagContext || after is GtlParser.Path_part_matcher_tagContext ||
                before is GtlParser.Path_part_matcher_traversing_wildcardContext || after is GtlParser.Path_part_matcher_traversing_wildcardContext)
            {
                throw new ScriptParserException("A wildcard path part cannot be combined with other constant, tagged, wildcard or string path parts.");
            }

            if ((first == context && isNonRootedPathSubject) ||
                (before == first && before is GtlParser.Path_part_traverser_parentContext) && !(before is GtlParser.Path_part_matcher_variableContext))
            {
                throw new ScriptParserException("A wildcard path part cannot be used at the beginning of a graph path.");
            }
            return result;
        }

        public override object VisitPath_part_matcher_tag_tag_only(GtlParser.Path_part_matcher_tag_tag_onlyContext context)
        {
            var tag = (string)VisitIdentifier(context.identifier());
            return new TaggedPathSubjectPart(null, tag);
        }

        public override object VisitPath_part_matcher_tag_name_only(GtlParser.Path_part_matcher_tag_name_onlyContext context)
        {
            var name = (string)VisitIdentifier(context.identifier());
            return new TaggedPathSubjectPart(name, null);
        }

        public override object VisitPath_part_matcher_tag_and_name(GtlParser.Path_part_matcher_tag_and_nameContext context)
        {
            var name = (string)VisitIdentifier(context.identifier(0));
            var tag = (string)VisitIdentifier(context.identifier(1));

            return new TaggedPathSubjectPart(name, tag);
        }

        public override object VisitPath_part_matcher_identifier(GtlParser.Path_part_matcher_identifierContext context)
        {
            var (before, _, first) = ParseTreeHelper.GetPathSiblings(context);

            if(before != null && before is not GtlParser.Path_part_traverser_parentContext || before != first)
            {
                throw new ScriptParserException("A identifier path part can only be used at the start of a path");
            }

            var parts = context
                .GetText()
                .Substring(1).Split('.');
            var storage = Guid.Parse(parts[0]);
            var account = Guid.Parse(parts[1]);
            var space = Guid.Parse(parts[2]);

            var era = ulong.Parse(parts[3]);
            var period = ulong.Parse(parts[4]);
            var moment = ulong.Parse(parts[5]);

            var identifier = Identifier.Create(storage, account, space, era, period, moment);

            return new IdentifierPathSubjectPart(identifier);
        }

        public override object VisitPath_part_matcher_variable(GtlParser.Path_part_matcher_variableContext context)
        {
            var text = (string)VisitIdentifier(context.identifier());
            return new VariablePathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_integer(GtlParser.Path_part_matcher_constant_integerContext context)
        {
            var number = VisitInteger_literal_unsigned(context.integer_literal_unsigned());
            return new ConstantPathSubjectPart(number.ToString());
        }

        public override object VisitPath_part_matcher_constant_identifier(GtlParser.Path_part_matcher_constant_identifierContext context)
        {
            var text = (string)VisitIdentifier(context.identifier());
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_quoted(GtlParser.Path_part_matcher_constant_quotedContext context)
        {
            var text = (string)VisitString_quoted_non_empty(context.string_quoted_non_empty());
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_unquoted(GtlParser.Path_part_matcher_constant_unquotedContext context)
        {
            var text = context.GetText();

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ScriptParserException("Whitespace in a path part requires a quoted string.");
            }
            
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_typed(GtlParser.Path_part_matcher_typedContext context)
        {
            var text = (string)VisitIdentifier(context.identifier());
            var formatter = TypedPathFormatter.FromString(text.ToUpper());
            return new TypedPathSubjectPart(formatter);
        }
    }
}
