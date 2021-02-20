// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Context;

    public partial class UbigiaVisitor
    {
        public override object VisitStructure_fragment(UbigiaParser.Structure_fragmentContext context)
        {
            //var requirement = (Requirement)VisitRequirement(context.requirement());
            var name = (string)VisitSchema_key(context.schema_key());
            var plurality = context.structure_plurality() != null ? Plurality.Multiple : Plurality.Single;
            var annotationContext = context.node_annotation();
            var annotation = annotationContext != null
                ? VisitNode_annotation(annotationContext) as NodeAnnotation
                : null;

            var bodyContext = context.structure_fragment_body();
            var fragments = bodyContext != null
                ? (Fragment[])VisitStructure_fragment_body(bodyContext)
                : Array.Empty<Fragment>();

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

            return new StructureFragment(name, plurality, annotation, valueFragments, structureFragments, fragmentType);
        }

        public override object VisitStructure_fragment_body_newline_separated(UbigiaParser.Structure_fragment_body_newline_separatedContext context)
        {
            return context
                .structure_fragment_body_entry()
                .Select(bodyContext => (Fragment)VisitStructure_fragment_body_entry(bodyContext))
                .ToArray();
        }

        public override object VisitStructure_fragment_body_comma_separated(UbigiaParser.Structure_fragment_body_comma_separatedContext context)
        {
            return context
                .structure_fragment_body_entry()
                .Select(bodyContext => (Fragment)VisitStructure_fragment_body_entry(bodyContext))
                .ToArray();
        }
    }
}
