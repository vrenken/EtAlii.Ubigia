// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Context;
    using ValueType = EtAlii.Ubigia.Api.Functional.Context.ValueType;

    public partial class UbigiaVisitor
    {
        public override object VisitValue_query_fragment(UbigiaParser.Value_query_fragmentContext context)
        {
            var name = (string)VisitSchema_key(context.schema_key());
            var prefixContext = context.schema_key_prefix();
            var prefix = prefixContext != null
                ? (ValuePrefix)VisitSchema_key_prefix(prefixContext)
                : new ValuePrefix(ValueType.Object, Requirement.None);
            var annotationContext = context.value_annotation();
            var annotation = annotationContext != null
                ? (ValueAnnotation)VisitValue_annotation(annotationContext)
                : null;
            return new ValueFragment(name, prefix, annotation, FragmentType.Query, null);
        }

        public override object VisitValue_mutation_fragment(UbigiaParser.Value_mutation_fragmentContext context)
        {
            var name = (string)VisitSchema_key(context.schema_key());
            var prefixContext = context.schema_key_prefix();
            var prefix = prefixContext != null
                ? (ValuePrefix)VisitSchema_key_prefix(prefixContext)
                : new ValuePrefix(ValueType.Object, Requirement.None);
            var value = VisitPrimitive_value(context.primitive_value());
            return new ValueFragment(name, prefix, null, FragmentType.Mutation, value);
        }

        public override object VisitSchema_key_prefix_type_and_requirement_1(UbigiaParser.Schema_key_prefix_type_and_requirement_1Context context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            var valueType = (ValueType)VisitValue_type(context.value_type());
            return new ValuePrefix(valueType, requirement);
        }

        public override object VisitSchema_key_prefix_type_and_requirement_2(UbigiaParser.Schema_key_prefix_type_and_requirement_2Context context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            var valueType = (ValueType)VisitValue_type(context.value_type());
            return new ValuePrefix(valueType, requirement);
        }

        public override object VisitSchema_key_prefix_requirement(UbigiaParser.Schema_key_prefix_requirementContext context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            return new ValuePrefix(ValueType.Object, requirement);
        }

        public override object VisitSchema_key_prefix_type_only(UbigiaParser.Schema_key_prefix_type_onlyContext context)
        {
            var valueType = (ValueType)VisitValue_type(context.value_type());
            return new ValuePrefix(valueType, Requirement.None);
        }

        public override object VisitValue_type(UbigiaParser.Value_typeContext context)
        {
            return Enum.Parse<ValueType>(context.GetText(), true);
        }
    }
}
