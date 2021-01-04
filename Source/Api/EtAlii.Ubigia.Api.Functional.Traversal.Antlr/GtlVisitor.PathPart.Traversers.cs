// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        // Hierarchical.
        public override object VisitPath_part_traverser_parent(GtlParser.Path_part_traverser_parentContext context)
        {
            var (before, after, _) = ParseTreeHelper.GetPathSiblings(context);

            if (before is GtlParser.Path_part_traverser_parentContext || after is GtlParser.Path_part_traverser_parentContext)
            {
                throw new ScriptParserException("Two parent path separators cannot be combined.");
            }
            if (after is GtlParser.Path_part_traverser_childrenContext)
            {
                throw new ScriptParserException("The parent path separator cannot be followed by a child path separator.");
            }

            return new ParentPathSubjectPart();
        }

        public override object VisitPath_part_traverser_parents_all(GtlParser.Path_part_traverser_parents_allContext context)
        {
            var (before, after, _) = ParseTreeHelper.GetPathSiblings(context);

            if (before is GtlParser.Path_part_traverser_parents_allContext || after is GtlParser.Path_part_traverser_parents_allContext ||
                before is GtlParser.Path_part_traverser_parents_allContext || after is GtlParser.Path_part_traverser_parents_allContext)
            {
                throw new ScriptParserException("The all parents path separator cannot be combined.");
            }
            if (after is GtlParser.Path_part_traverser_childrenContext)
            {
                throw new ScriptParserException("The all parents path separator cannot be followed by a child path separator.");
            }
            if (after is GtlParser.Path_part_traverser_childrenContext)
            {
                throw new ScriptParserException("The all parents path separator cannot be followed by an all child path separator.");
            }

            return new AllParentsPathSubjectPart();
        }

        public override object VisitPath_part_traverser_children(GtlParser.Path_part_traverser_childrenContext context)
        {
            var (before, after, _) = ParseTreeHelper.GetPathSiblings(context);

            if (before is GtlParser.Path_part_traverser_childrenContext || after is GtlParser.Path_part_traverser_childrenContext)
            {
                throw new ScriptParserException("Two child path separators cannot be combined.");
            }
            if (after is GtlParser.Path_part_traverser_parentContext)
            {
                throw new ScriptParserException("The child path separator cannot be followed by a parent path separator.");
            }

            return new ChildrenPathSubjectPart();
        }

        public override object VisitPath_part_traverser_children_all(GtlParser.Path_part_traverser_children_allContext context)
        {
            var (before, after, _) = ParseTreeHelper.GetPathSiblings(context);

            if (before is GtlParser.Path_part_traverser_childrenContext || after is GtlParser.Path_part_traverser_childrenContext ||
                before is GtlParser.Path_part_traverser_children_allContext || after is GtlParser.Path_part_traverser_children_allContext)
            {
                throw new ScriptParserException("The all children path separator cannot be combined.");
            }
            if (after is GtlParser.Path_part_traverser_parentContext)
            {
                throw new ScriptParserException("The all children path separator cannot be followed by a parent path separator.");
            }
            if (after is GtlParser.Path_part_traverser_parents_allContext)
            {
                throw new ScriptParserException("The all children path separator cannot be followed by an all parents path separator.");
            }

            return new AllChildrenPathSubjectPart();
        }

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
