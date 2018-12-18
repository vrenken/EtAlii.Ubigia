namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using global::GraphQL;
    using global::GraphQL.Execution;
    using global::GraphQL.Language.AST;
    using global::GraphQL.Types;
    using ISchema = global::GraphQL.Types.ISchema;

    public partial class DynamicSchema : Schema
    {
        private readonly List<IGraphType> _dynamicSchema = new List<IGraphType>();

        private readonly Document _document;
        private readonly IOperationProcessor _operationProcessor;
        private readonly IFieldProcessor _fieldProcessor;
        private readonly Dictionary<Type, GraphType> _graphTypes;
                
        private DynamicSchema(
            IDependencyResolver dependencyResolver,
            Document document, 
            IOperationProcessor operationProcessor, 
            IFieldProcessor fieldProcessor)
            : base(dependencyResolver)
        {
            _document = document;
            _operationProcessor = operationProcessor;
            _fieldProcessor = fieldProcessor;

            _graphTypes = new Dictionary<Type, GraphType>();
            
            Query = new UbigiaQuery();
            Mutation = new UbigiaMutation();
 
            RegisterDirectives(new UbigiaNodesDirective());
            RegisterDirectives(new UbigiaIdDirective());
            
            DependencyResolver = new FuncDependencyResolver(ResolveDynamicType);
        }

        private object ResolveDynamicType(Type type)
        {
            if (_graphTypes.TryGetValue(type, out var graphType))
            {
                return graphType;
            }

            return DependencyResolver.Resolve(type);
        }

        internal static async Task<Schema> Create(IDependencyResolver dependencyResolver, IOperationProcessor operationProcessor, IFieldProcessor fieldProcessor, Document document)
        {
            var dynamicSchema = new DynamicSchema(dependencyResolver, document, operationProcessor, fieldProcessor);
            await dynamicSchema.AddDynamicTypes();
            return dynamicSchema;
        }

        internal static async Task<Schema> Create(IDependencyResolver dependencyResolver, IOperationProcessor operationProcessor, IFieldProcessor fieldProcessor, string query)
        {
            var document = new GraphQLDocumentBuilder().Build(query);
            return await Create(dependencyResolver, operationProcessor, fieldProcessor, document);
        }
 
        private async Task AddDynamicTypes()
        {
            foreach (var operation in _document.Operations)
            {
                switch (operation.OperationType)
                {
                    case OperationType.Query:
                        await AddDynamicTypes(operation);
                        break;
                    default:
                        throw new NotSupportedException();
                }
                
            }
        }
               
        private async Task AddDynamicTypes(Operation queryOperation)
        {
            var registration = await _operationProcessor.Process(queryOperation, (ComplexGraphType<object>)Query, _graphTypes);
            await AddDynamicTypes(queryOperation.SelectionSet, registration);
        }

        private async Task AddDynamicTypes(SelectionSet selectionSet, Context parentContext)
        {
            foreach (var selection in selectionSet.Selections)
            {
                switch (selection)
                {
                    case Field field:
                        var fieldRegistration = await _fieldProcessor.Process(field, parentContext, _graphTypes);
                        if (field.SelectionSet != null && fieldRegistration != null)
                        {
                            await AddDynamicTypes(field.SelectionSet, fieldRegistration);
                        }
                        break;
                }
            }
        }
    }
}
