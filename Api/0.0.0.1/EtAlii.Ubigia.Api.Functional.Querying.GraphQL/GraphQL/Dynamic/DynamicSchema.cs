namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL.Execution;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;
    using ISchema = global::GraphQL.Types.ISchema;

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

        public static async Task<Schema> Create(ISchema originalSchema, IScriptsSet scriptsSet, Document document)
        {
            var staticUbigiaSchema = (StaticSchema)originalSchema;

            var dynamicSchema = new DynamicSchema(staticUbigiaSchema, document, scriptsSet);
            await dynamicSchema.AddDynamicTypes();
            return dynamicSchema;
        }

        public static async Task<Schema> Create(ISchema originalSchema, IScriptsSet scriptsSet, string query)
        {
            var document = new GraphQLDocumentBuilder().Build(query);
            return await Create(originalSchema, scriptsSet, document);
        }

        private async Task AddDynamicTypes()
        {
            var directives = GetStartDirectives(_document);
            foreach (var directive in directives)
            {
                var argument = directive.Arguments.First();
                if (argument.Value is StringValue stringValue)
                {
                    var path = stringValue.Value;
                    var node = await GetNodeForPath(path);
                    var properties = node.GetProperties();
                }
            }
        }

        private async Task<IInternalNode> GetNodeForPath(string path)
        {
            var scriptParseResult = _scriptsSet.Parse(path);
            if (scriptParseResult.Errors.Any())
            {
                throw new InvalidOperationException("Unable to process GraphQL argument 'path' of the start directive.");
            }

            var scope = new ScriptScope();
            var lastSequence = await _scriptsSet.Process(scriptParseResult.Script, scope);
            var results = await lastSequence.Output.ToArray();
            if (results.Length == 0)
            {
                throw new InvalidOperationException("Unable to process GraphQL query 'path' does not return any results.");
            }

            if (results.Length > 1)
            {
                throw new InvalidOperationException("Unable to process GraphQL query 'path' returns too many results.");
            }

            var result = (IInternalNode) results[0];
            return result;
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
