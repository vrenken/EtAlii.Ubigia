// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitValue_query_fragment(ContextSchemaParser.Value_query_fragmentContext context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            var name = context.schema_key().ToString();
            var annotation = VisitValue_annotation(context.value_annotation()) as ValueAnnotation;
            return new ValueFragment(name, annotation, requirement, FragmentType.Query, null);
        }

        public override object VisitValue_mutation_fragment(ContextSchemaParser.Value_mutation_fragmentContext context)
        {
            var name = context.schema_key().ToString();
            var value = (string)VisitString_quoted_non_empty(context.string_quoted_non_empty());
            return new ValueFragment(name, null, Requirement.None, FragmentType.Mutation, value);
        }
    }
}
