using System.Collections.Generic;
using GraphQL.Language.AST;

namespace EtAlii.Ubigia.Api.Functional
{
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

        public void Visit(GraphQL.Language.AST.Document visitableDocument)
        {
            Document = new Document();
            Stack.Push(Document);
            
            foreach (var operation in visitableDocument.Operations)
            {
                operation.Accept(this);
            }

            Stack.Pop();
        }
    }

}