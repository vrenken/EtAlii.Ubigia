namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Language.AST;
    using GraphQL.Types;

    public partial class DynamicSchema : Schema
    {
        private readonly global::GraphQL.Language.AST.Document _document;
        private readonly IOperationProcessor _operationProcessor;
        private readonly IFieldProcessor _fieldProcessor;
        private readonly Dictionary<System.Type, GraphType> _graphTypes;
                
        private DynamicSchema(
            IDependencyResolver dependencyResolver,
            global::GraphQL.Language.AST.Document document, 
            IOperationProcessor operationProcessor, 
            IFieldProcessor fieldProcessor)
            : base(dependencyResolver)
        {
            _document = document;
            _operationProcessor = operationProcessor;
            _fieldProcessor = fieldProcessor;

            _graphTypes = new Dictionary<System.Type, GraphType>();
            
            Query = new UbigiaQuery();
            Mutation = new UbigiaMutation();
 
            RegisterDirectives(new UbigiaNodesDirective());
            RegisterDirectives(new UbigiaIdDirective());
            
            DependencyResolver = new FuncDependencyResolver(ResolveDynamicType);
        }

        private object ResolveDynamicType(System.Type type)
        {
            if (_graphTypes.TryGetValue(type, out var graphType))
            {
                return graphType;
            }

            return DependencyResolver.Resolve(type);
        }

        internal static async Task<Schema> Create(IDependencyResolver dependencyResolver, IOperationProcessor operationProcessor, IFieldProcessor fieldProcessor, global::GraphQL.Language.AST.Document document)
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
                        var registration = await _operationProcessor.Process(operation, (ComplexGraphType<object>)Query, _graphTypes);
                        await AddDynamicTypes(operation.SelectionSet, registration);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
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
