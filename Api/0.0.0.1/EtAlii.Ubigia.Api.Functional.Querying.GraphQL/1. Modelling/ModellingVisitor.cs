namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using GraphQL.Language.AST;

    internal class ModellingVisitor : IGraphQLAstVisitor
    {
        public Document Document { get; private set; }
        protected Stack<object> Stack { get; } = new Stack<object>();
        
        public void Visit(Operation operation)
        {
            switch (operation.OperationType)
            {
                case OperationType.Mutation:
                    var mutation = new Mutation();
                    
                    break;
                case OperationType.Query:
                    break;
            }
        }

        public void Visit(GraphQL.Language.AST.Document document)
        {
            Document = new Document();
            Stack.Push(Document);
            
            foreach (var operation in document.Operations)
            {
                operation.Accept(this);
            }

            Stack.Pop();
        }
    }

}