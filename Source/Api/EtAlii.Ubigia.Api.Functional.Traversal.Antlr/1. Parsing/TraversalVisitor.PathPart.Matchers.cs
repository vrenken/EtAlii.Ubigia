// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class TraversalVisitor
    {
        public override object VisitPath_part_matcher_traversing_wildcard(TraversalScriptParser.Path_part_matcher_traversing_wildcardContext context)
        {
            var number = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned());
            return new TraversingWildcardPathSubjectPart(number);
        }

        public override object VisitMatcher_wildcard_nonquoted(TraversalScriptParser.Matcher_wildcard_nonquotedContext context)
        {
            var sb = new StringBuilder();
            if (VisitIdentifier(context.identifier(0)) is string before)
            {
                sb.Append(before);
            }

            sb.Append('*');

            if (VisitIdentifier(context.identifier(1)) is string after)
            {
                sb.Append(after);
            }

            var text = sb.ToString();
            return new WildcardPathSubjectPart(text);
        }

        public override object VisitMatcher_wildcard_quoted(TraversalScriptParser.Matcher_wildcard_quotedContext context)
        {
            var sb = new StringBuilder();
            if (VisitString_quoted_non_empty(context.string_quoted_non_empty(0)) is string before)
            {
                sb.Append(before);
            }

            sb.Append('*');

            if (VisitString_quoted_non_empty(context.string_quoted_non_empty(1)) is string after)
            {
                sb.Append(after);
            }

            var text = sb.ToString();
            return new WildcardPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_tag_tag_only(TraversalScriptParser.Path_part_matcher_tag_tag_onlyContext context)
        {
            var tag = (string)VisitIdentifier(context.identifier());
            return new TaggedPathSubjectPart(null, tag);
        }

        public override object VisitPath_part_matcher_tag_name_only(TraversalScriptParser.Path_part_matcher_tag_name_onlyContext context)
        {
            var name = (string)VisitIdentifier(context.identifier());
            return new TaggedPathSubjectPart(name, null);
        }

        public override object VisitPath_part_matcher_tag_and_name(TraversalScriptParser.Path_part_matcher_tag_and_nameContext context)
        {
            var name = (string)VisitIdentifier(context.identifier(0));
            var tag = (string)VisitIdentifier(context.identifier(1));

            return new TaggedPathSubjectPart(name, tag);
        }

        public override object VisitPath_part_matcher_identifier(TraversalScriptParser.Path_part_matcher_identifierContext context)
        {
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

        public override object VisitPath_part_matcher_variable(TraversalScriptParser.Path_part_matcher_variableContext context)
        {
            var text = (string)VisitIdentifier(context.identifier());
            return new VariablePathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_integer(TraversalScriptParser.Path_part_matcher_constant_integerContext context)
        {
            var number = VisitInteger_literal_unsigned(context.integer_literal_unsigned());
            return new ConstantPathSubjectPart(number.ToString());
        }

        public override object VisitPath_part_matcher_constant_identifier(TraversalScriptParser.Path_part_matcher_constant_identifierContext context)
        {
            var text = (string)VisitIdentifier(context.identifier());
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_quoted(TraversalScriptParser.Path_part_matcher_constant_quotedContext context)
        {
            var text = (string)VisitString_quoted_non_empty(context.string_quoted_non_empty());
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_constant_unquoted(TraversalScriptParser.Path_part_matcher_constant_unquotedContext context)
        {
            var text = context.GetText();
            return new ConstantPathSubjectPart(text);
        }

        public override object VisitPath_part_matcher_typed(TraversalScriptParser.Path_part_matcher_typedContext context)
        {
            var text = (string)VisitIdentifier(context.identifier());
            var formatter = TypedPathFormatter.FromString(text.ToUpper());
            return new TypedPathSubjectPart(formatter);
        }

        public override object VisitPath_part_matcher_conditional(TraversalScriptParser.Path_part_matcher_conditionalContext context)
        {
            var conditions = context.path_part_matcher_condition()
                .Select(conditionContext =>
                {
                    var property = (string)VisitPath_part_matcher_property(conditionContext.path_part_matcher_property());
                    var comparison = conditionContext.path_part_matcher_condition_comparison().GetText();
                    var value = VisitPath_part_matcher_value(conditionContext.path_part_matcher_value());

                    var conditionType = comparison switch
                    {
                        "=" => ConditionType.Equal,
                        "!=" => ConditionType.NotEqual,
                        "<" => ConditionType.LessThan,
                        "<=" => ConditionType.LessThanOrEqual,
                        ">" => ConditionType.MoreThan,
                        ">=" => ConditionType.MoreThanOrEqual,
                        _ => throw new ScriptParserException($"Unable to interpret comparison: {comparison ?? "NULL"}")
                    };

                    return new Condition(property, conditionType, value);
                })
                .ToArray();

            return new ConditionalPathSubjectPart(conditions);
        }
    }
}
