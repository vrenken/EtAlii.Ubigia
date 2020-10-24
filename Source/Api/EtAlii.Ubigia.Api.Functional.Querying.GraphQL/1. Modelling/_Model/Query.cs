namespace EtAlii.Ubigia.Api.Functional.Querying
{
    public class Query
    {
        internal global::GraphQL.Language.AST.Document Document { get; }
        internal string Text { get; }

        internal Query(global::GraphQL.Language.AST.Document document, string text)
        {
            Document = document;
            Text = text;
        }
    }
}