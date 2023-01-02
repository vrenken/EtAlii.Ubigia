// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        public override object VisitNode_identity_literal(UbigiaParser.Node_identity_literalContext context)
        {
            var name = (string)base.VisitNode_identity_literal(context);
            return new NodeIdentity { Name = name, IsVariable = false };
        }

        public override object VisitNode_identity_variable(UbigiaParser.Node_identity_variableContext context)
        {
            var name = (string)base.VisitNode_identity_variable(context);
            return new NodeIdentity { Name = name[1..], IsVariable = true };
        }

        public override object VisitNode_annotation_add_and_select_multiple_nodes(UbigiaParser.Node_annotation_add_and_select_multiple_nodesContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var identity = (NodeIdentity)VisitNode_identity(context.node_identity());
            return new AddAndSelectMultipleNodesAnnotation(sourcePath, identity);
        }

        public override object VisitNode_annotation_add_and_select_single_node(UbigiaParser.Node_annotation_add_and_select_single_nodeContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var identity = (NodeIdentity)VisitNode_identity(context.node_identity());
            return new AddAndSelectSingleNodeAnnotation(sourcePath, identity);
        }

        public override object VisitNode_annotation_remove_and_select_multiple_nodes(UbigiaParser.Node_annotation_remove_and_select_multiple_nodesContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var name = (string)VisitSchema_key(context.schema_key());
            return new RemoveAndSelectMultipleNodesAnnotation(sourcePath, name);
        }

        public override object VisitNode_annotation_remove_and_select_single_node(UbigiaParser.Node_annotation_remove_and_select_single_nodeContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            var name = (string)VisitSchema_key(context.schema_key());
            return new RemoveAndSelectSingleNodeAnnotation(sourcePath, name);
        }

        public override object VisitNode_annotation_link_and_select_multiple_nodes(UbigiaParser.Node_annotation_link_and_select_multiple_nodesContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path(0));
            var targetPath = (PathSubject)VisitSchema_path(context.schema_path(1));
            var targetLink = (NonRootedPathSubject)VisitSchema_path(context.schema_path(2));
            return new LinkAndSelectMultipleNodesAnnotation(sourcePath, targetPath, targetLink);
        }

        public override object VisitNode_annotation_link_and_select_single_node(UbigiaParser.Node_annotation_link_and_select_single_nodeContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path(0));
            var targetPath = (PathSubject)VisitSchema_path(context.schema_path(1));
            var targetLink = (NonRootedPathSubject)VisitSchema_path(context.schema_path(2));
            return new LinkAndSelectSingleNodeAnnotation(sourcePath, targetPath, targetLink);
        }

        public override object VisitNode_annotation_unlink_and_select_multiple_nodes(UbigiaParser.Node_annotation_unlink_and_select_multiple_nodesContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path(0));
            var targetPath = (PathSubject)VisitSchema_path(context.schema_path(1));
            var targetLink = (NonRootedPathSubject)VisitSchema_path(context.schema_path(2));
            return new UnlinkAndSelectMultipleNodesAnnotation(sourcePath, targetPath, targetLink);
        }

        public override object VisitNode_annotation_unlink_and_select_single_node(UbigiaParser.Node_annotation_unlink_and_select_single_nodeContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path(0));
            var targetPath = (PathSubject)VisitSchema_path(context.schema_path(1));
            var targetLink = (NonRootedPathSubject)VisitSchema_path(context.schema_path(2));
            return new UnlinkAndSelectSingleNodeAnnotation(sourcePath, targetPath, targetLink);
        }

        public override object VisitNode_annotation_select_multiple_nodes(UbigiaParser.Node_annotation_select_multiple_nodesContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            return new SelectMultipleNodesAnnotation(sourcePath);
        }

        public override object VisitNode_annotation_select_single_node(UbigiaParser.Node_annotation_select_single_nodeContext context)
        {
            var sourcePath = (PathSubject)VisitSchema_path(context.schema_path());
            return new SelectSingleNodeAnnotation(sourcePath);
        }

        public override object VisitNode_annotation_select_current_node(UbigiaParser.Node_annotation_select_current_nodeContext context)
        {
            return new SelectCurrentNodeAnnotation();
        }

        public override object VisitNode_annotation_map_sequence(UbigiaParser.Node_annotation_map_sequenceContext context)
        {
            var sequence = (Sequence)Visit(context.sequence());

            // This is fundamentally wrong, but should get us going.
            // See https://github.com/vrenken/EtAlii.Ubigia/issues/66 for more info.
            var path = sequence.Parts
                .OfType<PathSubject>()
                .FirstOrDefault();
            return new SelectSingleNodeAnnotation(path);
        }
    }
}
