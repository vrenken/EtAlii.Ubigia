namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL;
    using global::GraphQL.Execution;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;

    public class DynamicSchema : Schema
    {
        private readonly List<IGraphType> _dynamicSchema = new List<IGraphType>();

        private readonly ISchema _staticSchema;
        private readonly Document _document;
        private readonly IScriptsSet _scriptsSet;

        private DynamicSchema(IStaticSchema staticSchema, Document document, IScriptsSet scriptsSet)
            : base(staticSchema.DependencyResolver)
        {
            _staticSchema = staticSchema;
            _document = document;
            _scriptsSet = scriptsSet;

            Query = staticSchema.Query;
            Mutation = staticSchema.Mutation;
            Directives = staticSchema.Directives;
        }

        public static Schema Create(ISchema originalSchema, IScriptsSet scriptsSet, Document document)
        {
            var staticUbigiaSchema = (StaticSchema)originalSchema;

            var dynamicSchema = new DynamicSchema(staticUbigiaSchema, document, scriptsSet);
            dynamicSchema.AddDynamicTypes();
            return dynamicSchema;
        }

        public static Schema Create(ISchema originalSchema, IScriptsSet scriptsSet, string query)
        {
            var document = new GraphQLDocumentBuilder().Build(query);
            return Create(originalSchema, scriptsSet, document);
        }

        private void AddDynamicTypes()
        {
            var directives = GetStartDirectives(_document);
            foreach (var directive in directives)
            {
                var argument = directive.Arguments.First();
                if (argument.Value is StringValue stringValue)
                {
                    var path = stringValue.Value;
                    var script = _scriptsSet.Parse(path);
                    if (script.Errors.Any())
                    {
                        throw new InvalidOperationException("Unable to process argument 'path' of the start directive.");
                    }
                    var scope = new ScriptScope();
                    //_scriptSet.Process(script, scope);
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
