namespace EtAlii.Ubigia.Api.Functional
{
    public class Query
    {
        internal GraphQL.Language.AST.Document Document { get; }
        internal string Text { get; }

        internal Query(GraphQL.Language.AST.Document document, string text)
        {
            Document = document;
            Text = text;
        }
    }
}