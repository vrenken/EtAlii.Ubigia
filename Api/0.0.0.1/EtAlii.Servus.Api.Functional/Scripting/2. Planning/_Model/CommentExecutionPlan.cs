namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class CommentExecutionPlan : IExecutionPlan
    {
        private readonly Comment _comment;

        public Type OutputType { get; private set; }

        public CommentExecutionPlan(Comment comment)
        {
            _comment = comment;
            OutputType = GetType();
        }

        public IObservable<object> Execute(ExecutionScope scope)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[COMMENT]" + _comment + "[/COMMENT]";
        }
    }
}