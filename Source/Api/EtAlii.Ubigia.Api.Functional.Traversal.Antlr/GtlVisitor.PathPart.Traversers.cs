// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        // Hierarchical.
        public override object VisitPath_part_traverser_parent(GtlParser.Path_part_traverser_parentContext context) => new ParentPathSubjectPart();
        public override object VisitPath_part_traverser_parents_all(GtlParser.Path_part_traverser_parents_allContext context) => new AllParentsPathSubjectPart();
        public override object VisitPath_part_traverser_children(GtlParser.Path_part_traverser_childrenContext context) => new ChildrenPathSubjectPart();
        public override object VisitPath_part_traverser_children_all(GtlParser.Path_part_traverser_children_allContext context) => new AllChildrenPathSubjectPart();

        // TODO: The commented lines are fishy, we mix and mingle all and oldest.

        // Temporal
        public override object VisitPath_part_traverser_downdate(GtlParser.Path_part_traverser_downdateContext context) => new DowndatePathSubjectPart();
        public override object VisitPath_part_traverser_updates(GtlParser.Path_part_traverser_updatesContext context) => new UpdatesPathSubjectPart();
        // public override object VisitPath_part_traverser_downdates_oldest(GtlParser.Path_part_traverser_downdates_oldestContext context) => new OldestPathSubjectPart();
        // public override object VisitPath_part_traverser_downdates_all(GtlParser.Path_part_traverser_downdates_allContext context) => new AllDowndatesPathSubjectPart();
        public override object VisitPath_part_traverser_downdates_oldest(GtlParser.Path_part_traverser_downdates_oldestContext context) => new AllDowndatesPathSubjectPart();
        // public override object VisitPath_part_traverser_updates_newest(GtlParser.Path_part_traverser_updates_newestContext context) => new NewestPathSubjectPart();
        // public override object VisitPath_part_traverser_updates_all(GtlParser.Path_part_traverser_updates_allContext context) => new AllUpdatesPathSubjectPart();
        public override object VisitPath_part_traverser_updates_newest(GtlParser.Path_part_traverser_updates_newestContext context) => new AllUpdatesPathSubjectPart();


        // Sequential
        public override object VisitPath_part_traverser_previous_single(GtlParser.Path_part_traverser_previous_singleContext context) => new PreviousPathSubjectPart();
        public override object VisitPath_part_traverser_next_single(GtlParser.Path_part_traverser_next_singleContext context) => new NextPathSubjectPart();
        // public override object VisitPath_part_traverser_previous_all(GtlParser.Path_part_traverser_previous_allContext context) => new AllPreviousPathSubjectPart();
        public override object VisitPath_part_traverser_previous_first(GtlParser.Path_part_traverser_previous_firstContext context) => new AllPreviousPathSubjectPart();
        // public override object VisitPath_part_traverser_previous_first(GtlParser.Path_part_traverser_previous_firstContext context) => new FirstPathSubjectPart();
        // public override object VisitPath_part_traverser_next_all(GtlParser.Path_part_traverser_next_allContext context) => new AllNextPathSubjectPart();
        public override object VisitPath_part_traverser_next_last(GtlParser.Path_part_traverser_next_lastContext context) => new AllNextPathSubjectPart();
        // public override object VisitPath_part_traverser_next_last(GtlParser.Path_part_traverser_next_lastContext context) => new LastPathSubjectPart();
    }
}
