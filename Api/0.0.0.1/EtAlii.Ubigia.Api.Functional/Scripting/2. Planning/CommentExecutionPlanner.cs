namespace EtAlii.Ubigia.Api.Functional
{
    internal class CommentExecutionPlanner : ICommentExecutionPlanner
    {
        public IExecutionPlan Plan(SequencePart part)
        {
            var comment = (Comment) part;
            return new CommentExecutionPlan(comment);
        }
    }
}