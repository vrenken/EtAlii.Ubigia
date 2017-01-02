namespace EtAlii.Ubigia.Api.Functional
{
    using System;
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