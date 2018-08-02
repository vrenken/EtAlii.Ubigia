namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL;
    using global::GraphQL.Execution;
    using global::GraphQL.Language;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    public class DynamicSchema : Schema
    {
        private readonly List<IGraphType> _dynamicSchema = new List<IGraphType>();

        private readonly ISchema _staticSchema;
        private readonly Document _document;

        private DynamicSchema(IStaticSchema staticSchema, Document document)
            : base(staticSchema.DependencyResolver)
        {
            _staticSchema = staticSchema;
            _document = document;

            Query = staticSchema.Query;
            Mutation = staticSchema.Mutation;
            Directives = staticSchema.Directives;
        }

        public static Schema Create(ISchema originalSchema, Document document)
        {
            var staticUbigiaSchema = (StaticSchema)originalSchema;

            var dynamicSchema = new DynamicSchema(staticUbigiaSchema, document);
            dynamicSchema.AddDynamicTypes();
            return dynamicSchema;
        }

        public static Schema Create(ISchema originalSchema, string query)
        {
            var document = new GraphQLDocumentBuilder().Build(query);
            return Create(originalSchema, document);
        }

        private void AddDynamicTypes()
        {
            var directives = GetStartDirectives(_document);
            foreach (var directive in directives)
            {
                var argument = directive.Arguments.First();
                if (argument.GetValue() is string path)
                {
                    //path;
                }
            }

        }
        private IEnumerable<Directive> GetStartDirectives(Document document)
        {
            return document.Operations
                .Where(operation => operation.OperationType == OperationType.Query)
                .SelectMany(operation => operation.Directives)
                .Where(directive => directive.Name == "start")
                .AsEnumerable();
        }
    }
}
