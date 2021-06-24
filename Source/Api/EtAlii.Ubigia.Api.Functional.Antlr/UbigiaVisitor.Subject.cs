// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        public override object VisitSubject_variable(UbigiaParser.Subject_variableContext context)
        {
            var name = (string)VisitIdentifier(context.identifier());
            return new VariableSubject(name);
        }

        public override object VisitSubject_constant_string(UbigiaParser.Subject_constant_stringContext context)
        {
            var text = (string)VisitString_quoted(context.string_quoted());
            return new StringConstantSubject(text);
        }

        public override object VisitPath_part_matcher_regex(UbigiaParser.Path_part_matcher_regexContext context)
        {
            var text = (string)VisitString_quoted_non_empty(context.string_quoted_non_empty());
            var result = new RegexPathSubjectPart(text);
            return result;
        }
    }
}
