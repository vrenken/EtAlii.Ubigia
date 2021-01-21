// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitValue_query_fragment(ContextSchemaParser.Value_query_fragmentContext context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            var name = (string)VisitSchema_key(context.schema_key());
            var annotationContext = context.value_annotation();
            var annotation = annotationContext != null
                ? VisitValue_annotation(annotationContext) as ValueAnnotation
                : null;
            return new ValueFragment(name, annotation, requirement, FragmentType.Query, null);
        }

        public override object VisitValue_mutation_fragment(ContextSchemaParser.Value_mutation_fragmentContext context)
        {
            var name = (string)VisitSchema_key(context.schema_key());
            var value = VisitPrimitive_value(context.primitive_value());
            return new ValueFragment(name, null, Requirement.None, FragmentType.Mutation, value);
        }
    }
}
