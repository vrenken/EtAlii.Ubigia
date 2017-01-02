namespace EtAlii.Servus.Api.Data
{
    using EtAlii.xTechnology.MicroContainer;
    using Newtonsoft.Json;

    /// <summary>
    /// The ContainerHandlerFactory returns handlers using a container registration pattern (IoC/DI).
    /// </summary>
    internal class ContainerHandlerFactory : IHandlerFactory
    {
        private readonly Container _container;

        public ContainerHandlerFactory(ScriptScope scope, IDataConnection connection)
        {
            _container = new Container();

            _container.Register<IDataConnection>(() => connection, Lifestyle.Singleton);
            _container.Register<ScriptScope>(() => scope, Lifestyle.Singleton);
            _container.Register<ContentManagerFactory>(Lifestyle.Singleton);
            _container.Register<IContentManager>(() => _container.GetInstance<ContentManagerFactory>().Create(connection), Lifestyle.Singleton);

            _container.Register<JsonSerializer>(() => new SerializerFactory().Create(), Lifestyle.Singleton);
            //_container.Register<IPropertiesSerializer, JsonPropertiesSerializer>(Lifestyle.Singleton);
            _container.Register<IPropertiesSerializer, BsonPropertiesSerializer>(Lifestyle.Singleton);

            _container.Register<ItemOutputHandler>(Lifestyle.Singleton);
            _container.Register<ItemsOutputHandler>(Lifestyle.Singleton);
            _container.Register<VariableAddItemHandler>(Lifestyle.Singleton);
            _container.Register<VariableUpdateItemHandler>(Lifestyle.Singleton);
            _container.Register<VariableItemAssignmentHandler>(Lifestyle.Singleton);
            _container.Register<VariableItemsAssignmentHandler>(Lifestyle.Singleton);
            _container.Register<VariableOutputHandler>(Lifestyle.Singleton);
            _container.Register<VariableStringAssignmentHandler>(Lifestyle.Singleton);
            _container.Register<AddItemHandler>(Lifestyle.Singleton);
            _container.Register<AddItemsHandler>(Lifestyle.Singleton);
            _container.Register<RemoveItemHandler>(Lifestyle.Singleton);
            _container.Register<UpdateItemHandler>(Lifestyle.Singleton);

            _container.Register<IPathHelper, PathHelper>(Lifestyle.Singleton);
            _container.Register<IPathExpander, PathExpander>(Lifestyle.Singleton);
            _container.Register<IdentifierComponentExpander>(Lifestyle.Singleton);
            _container.Register<VariableComponentExpander>(Lifestyle.Singleton);
            _container.Register<NameComponentExpander>(Lifestyle.Singleton);
            _container.Register<IPathTraverser, PathTraverser>(Lifestyle.Singleton);
            _container.Register<ITimeTraverser, TimeTraverser>(Lifestyle.Singleton);
            _container.Register<IAddItemHelper, AddItemHelper>(Lifestyle.Singleton);
            
            _container.Register<TerminalExpressions>(Lifestyle.Singleton);
            _container.Register<PathExpressions>(Lifestyle.Singleton);
            _container.Register<OperatorExpressions>(Lifestyle.Singleton);
        }

        public T Create<T>()
            where T : class, IActionHandler
        {
            return _container.GetInstance<T>();
        }
    }
}
