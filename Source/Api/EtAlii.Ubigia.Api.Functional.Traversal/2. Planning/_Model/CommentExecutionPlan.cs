namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    public class CommentExecutionPlan : IScriptExecutionPlan
    {
        private readonly Comment _comment;

        public Type OutputType { get; }

        public CommentExecutionPlan(Comment comment)
        {
            _comment = comment;
            OutputType = GetType();
        }

        public Task<IObservable<object>> Execute(ExecutionScope scope)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[COMMENT]" + _comment + "[/COMMENT]";
        }
    }
}
