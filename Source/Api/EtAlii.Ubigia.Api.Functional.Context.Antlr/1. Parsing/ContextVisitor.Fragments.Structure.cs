// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitStructure_fragment(ContextSchemaParser.Structure_fragmentContext context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            var name = context.schema_key().ToString();
            var annotationContext = context.node_annotation();
            var annotation = annotationContext != null
                ? VisitNode_annotation(annotationContext) as NodeAnnotation
                : null;

            var fragments = (Fragment[])VisitStructure_fragment_body(context.structure_fragment_body());

            var valueFragments = fragments
                .OfType<ValueFragment>()
                .ToArray();
            var structureFragments = fragments
                .OfType<StructureFragment>()
                .ToArray();

            var fragmentType = annotation == null || annotation is SelectSingleNodeAnnotation || annotation is SelectMultipleNodesAnnotation
                ? FragmentType.Query
                : FragmentType.Mutation;

            if (valueFragments.Any(vf => vf.Type == FragmentType.Mutation))
            {
                fragmentType = FragmentType.Mutation;
            }

            return new StructureFragment(name, annotation, requirement, valueFragments, structureFragments, fragmentType);
        }

        public override object VisitStructure_fragment_body_newline_separated(ContextSchemaParser.Structure_fragment_body_newline_separatedContext context)
        {
            return context
                .structure_fragment_body_entry()
                .Select(bodyContext => (Fragment)VisitStructure_fragment_body_entry(bodyContext))
                .ToArray();
        }

        public override object VisitStructure_fragment_body_comma_separated(ContextSchemaParser.Structure_fragment_body_comma_separatedContext context)
        {
            return context
                .structure_fragment_body_entry()
                .Select(bodyContext => (Fragment)VisitStructure_fragment_body_entry(bodyContext))
                .ToArray();
        }
    }
}
