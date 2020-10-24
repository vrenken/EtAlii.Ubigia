namespace EtAlii.Ubigia.Api.Functional.Scripting
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