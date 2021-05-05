namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class CommentExecutionPlanner : ICommentExecutionPlanner
    {
        public IScriptExecutionPlan Plan(SequencePart part)
        {
            var comment = (Comment) part;
            return new CommentExecutionPlan(comment);
        }
    }
}
