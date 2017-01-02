namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class RenameFunctionHandlerFactory
    {
        public IFunctionHandler Create()
        {
            var container = new Container();
            container.Register<IFunctionHandler, RenameFunctionHandler>(Lifestyle.Singleton);
            return container.GetInstance<IFunctionHandler>();
        }
    }
}
