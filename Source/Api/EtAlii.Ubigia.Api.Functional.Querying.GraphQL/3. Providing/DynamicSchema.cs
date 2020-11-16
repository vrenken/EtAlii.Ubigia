namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Threading.Tasks;
    using GraphQL.Execution;
    using GraphQL.Language.AST;
    using GraphQL.Types;

    public class DynamicSchema : Schema
    {
        private readonly global::GraphQL.Language.AST.Document _document;
        private readonly IOperationProcessor _operationProcessor;
        private readonly IFieldProcessor _fieldProcessor;

        private readonly IGraphTypeServiceProvider _graphTypeServiceProvider;
                
        private DynamicSchema(
            IServiceProvider serviceProvider,
            global::GraphQL.Language.AST.Document document, 
            IOperationProcessor operationProcessor, 
            IFieldProcessor fieldProcessor)
            : base(serviceProvider)
        {
            _graphTypeServiceProvider = (IGraphTypeServiceProvider) serviceProvider;
            _document = document;
            _operationProcessor = operationProcessor;
            _fieldProcessor = fieldProcessor;
            
            Query = new UbigiaQuery();
            Mutation = new UbigiaMutation();
 
            RegisterDirectives(new UbigiaNodesDirective());
            RegisterDirectives(new UbigiaIdDirective());
        }

        internal static async Task<Schema> Create(IServiceProvider serviceProvider, IOperationProcessor operationProcessor, IFieldProcessor fieldProcessor, global::GraphQL.Language.AST.Document document)
        {
            var dynamicSchema = new DynamicSchema(serviceProvider, document, operationProcessor, fieldProcessor);
            await dynamicSchema.AddDynamicTypes();
            return dynamicSchema;
        }

        internal static async Task<Schema> Create(IServiceProvider serviceProvider, IOperationProcessor operationProcessor, IFieldProcessor fieldProcessor, string query)
        {
            var document = new GraphQLDocumentBuilder().Build(query);
            return await Create(serviceProvider, operationProcessor, fieldProcessor, document);
        }
 
        private async Task AddDynamicTypes()
        {
            if (_document.Operations != null)
            {
                foreach (var operation in _document.Operations)
                {
                    switch (operation.OperationType)
                    {
                        case OperationType.Query:
                            var registration = await _operationProcessor.Process(operation, (ComplexGraphType<object>)Query);
                            await AddDynamicTypes(operation.SelectionSet, registration);
                            break;
                        default:
                            throw new NotSupportedException();
                    }
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
                        var fieldRegistration = await _fieldProcessor.Process(field, parentContext, _graphTypeServiceProvider);
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
