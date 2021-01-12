// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Context.Antlr;

    public partial class ContextVisitor
    {
        public override object VisitStructure_fragment(ContextSchemaParser.Structure_fragmentContext context)
        {
            var requirement = (Requirement)VisitRequirement(context.requirement());
            var name = context.schema_key().ToString();
            var annotation = VisitNode_annotation(context.node_annotation()) as NodeAnnotation;
            return new StructureFragment(name, annotation, requirement, System.Array.Empty<ValueFragment>(), System.Array.Empty<StructureFragment>(), FragmentType.Query);
        }
    }
}
