namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::GraphQL;
    using global::GraphQL.Execution;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;
    using ISchema = global::GraphQL.Types.ISchema;

    public partial class DynamicSchema : Schema
    {
        private readonly List<IGraphType> _dynamicSchema = new List<IGraphType>();

        private readonly IStaticSchema _staticSchema;
        private readonly Document _document;
        private readonly IFieldTypeBuilder _fieldTypeBuilder;
        private readonly INodeFetcher _nodeFetcher;
        private readonly Dictionary<Type, DynamicObjectGraphType> _graphObjectInstances;
                
        private readonly Type _baseClassType = typeof(DynamicObjectGraphType);

        private DynamicSchema(IStaticSchema staticSchema, Document document, IScriptsSet scriptsSet)
            : base(staticSchema.DependencyResolver)
        {
            _staticSchema = staticSchema;
            _document = document;
            _fieldTypeBuilder = new FieldTypeBuilder();
            _nodeFetcher = new NodeFetcher(scriptsSet);
            
            _graphObjectInstances = new Dictionary<Type, DynamicObjectGraphType>();
            
            Query = staticSchema.Query;
            Mutation = staticSchema.Mutation;
            Directives = staticSchema.Directives;

            DependencyResolver = new FuncDependencyResolver(ResolveDynamicType);
        }

        private object ResolveDynamicType(Type type)
        {
            if (type.IsSubclassOf(_baseClassType))
            {
                return _graphObjectInstances[type];
            }

            return _staticSchema.DependencyResolver.Resolve(type);
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
            var directives = _document.Operations
                .Where(operation => operation.OperationType == OperationType.Query)
                .SelectMany(operation => operation.Directives)
                .Where(directive => directive.Name == "start")
                .AsEnumerable();
            
            foreach (var directive in directives)
            {
                var argument = directive.Arguments.First();
                if (argument.Value is StringValue stringValue)
                {
                    var path = stringValue.Value;
                    var node = await _nodeFetcher.FetchAsync(path);
                    var properties = node.GetProperties();
                    var fieldType = _fieldTypeBuilder.Build(properties, path, directive, out DynamicObjectGraphType fieldTypeInstance);

                    _graphObjectInstances[fieldTypeInstance.GetType()] = fieldTypeInstance;
                    
                    var query = (ComplexGraphType<object>) Query;
                    query.AddField(fieldType);
                }
            }
        }
    }
}
