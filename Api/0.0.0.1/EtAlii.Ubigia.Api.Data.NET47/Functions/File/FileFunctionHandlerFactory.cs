namespace EtAlii.Ubigia.Api.Functional.NET47
{
    using EtAlii.xTechnology.MicroContainer;

    internal class FileFunctionHandlerFactory
    {
        public IFunctionHandler Create()
        {
            var container = new Container();
            container.Register<IFunctionHandler, FileFunctionHandler>();
            return container.GetInstance<IFunctionHandler>();
        }
    }
}