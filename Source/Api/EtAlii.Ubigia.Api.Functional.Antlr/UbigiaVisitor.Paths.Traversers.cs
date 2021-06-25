// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// This file is shared by both the traversal and context projects.

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        // Hierarchical.
        public override object VisitPath_part_traverser_parent(UbigiaParser.Path_part_traverser_parentContext context) => new ParentPathSubjectPart();

        public override object VisitPath_part_traverser_parents_all(UbigiaParser.Path_part_traverser_parents_allContext context) => new AllParentsPathSubjectPart();

        public override object VisitPath_part_traverser_children(UbigiaParser.Path_part_traverser_childrenContext context) => new ChildrenPathSubjectPart();

        public override object VisitPath_part_traverser_children_all(UbigiaParser.Path_part_traverser_children_allContext context) => new AllChildrenPathSubjectPart();

        // Temporal
        public override object VisitPath_part_traverser_downdate(UbigiaParser.Path_part_traverser_downdateContext context) => new DowndatePathSubjectPart();
        public override object VisitPath_part_traverser_updates(UbigiaParser.Path_part_traverser_updatesContext context) => new UpdatesPathSubjectPart();

        // public override object VisitPath_part_traverser_downdates_oldest[TraversalScriptParser.Path_part_traverser_downdates_oldestContext context] => new OldestPathSubjectPart[]
        // public override object VisitPath_part_traverser_downdates_all[TraversalScriptParser.Path_part_traverser_downdates_allContext context] => new AllDowndatesPathSubjectPart[]
        public override object VisitPath_part_traverser_downdates_oldest(UbigiaParser.Path_part_traverser_downdates_oldestContext context) => new AllDowndatesPathSubjectPart();
        // public override object VisitPath_part_traverser_updates_newest[TraversalScriptParser.Path_part_traverser_updates_newestContext context] => new NewestPathSubjectPart[]
        // public override object VisitPath_part_traverser_updates_all[TraversalScriptParser.Path_part_traverser_updates_allContext context] => new AllUpdatesPathSubjectPart[]
        public override object VisitPath_part_traverser_updates_newest(UbigiaParser.Path_part_traverser_updates_newestContext context) => new AllUpdatesPathSubjectPart();


        // Sequential
        public override object VisitPath_part_traverser_previous_single(UbigiaParser.Path_part_traverser_previous_singleContext context) => new PreviousPathSubjectPart();
        public override object VisitPath_part_traverser_next_single(UbigiaParser.Path_part_traverser_next_singleContext context) => new NextPathSubjectPart();
        // public override object VisitPath_part_traverser_previous_all[TraversalScriptParser.Path_part_traverser_previous_allContext context] => new AllPreviousPathSubjectPart[]
        public override object VisitPath_part_traverser_previous_first(UbigiaParser.Path_part_traverser_previous_firstContext context) => new AllPreviousPathSubjectPart();
        // public override object VisitPath_part_traverser_previous_first[TraversalScriptParser.Path_part_traverser_previous_firstContext context] => new FirstPathSubjectPart[]
        // public override object VisitPath_part_traverser_next_all[TraversalScriptParser.Path_part_traverser_next_allContext context] => new AllNextPathSubjectPart[]
        public override object VisitPath_part_traverser_next_last(UbigiaParser.Path_part_traverser_next_lastContext context) => new AllNextPathSubjectPart();
        // public override object VisitPath_part_traverser_next_last[TraversalScriptParser.Path_part_traverser_next_lastContext context] => new LastPathSubjectPart[]
    }
}
