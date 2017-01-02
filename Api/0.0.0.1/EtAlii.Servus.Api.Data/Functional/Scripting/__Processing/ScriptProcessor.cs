namespace EtAlii.Servus.Api.Data
{
    internal class ScriptProcessor : IScriptProcessor
    {
        public ScriptProcessor()
        {
        }

        public void Process(Script script, ScriptScope scope, IDataConnection connection)
        {
            var handlerFactory = new ContainerHandlerFactory(scope, connection);

            foreach (var action in script.Actions)
            {
                action.Handle(handlerFactory);
            }
        }
    }
}
