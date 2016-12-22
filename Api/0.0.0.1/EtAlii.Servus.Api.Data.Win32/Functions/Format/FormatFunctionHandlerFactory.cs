namespace EtAlii.Servus.Api
{
    using System;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class FormatFunctionHandlerFactory
    {
        public IFunctionHandler Create()
        {
            var container = new Container();
            container.Register<IFunctionHandler, FormatFunctionHandler>();
            return container.GetInstance<IFunctionHandler>();
        }
    }
}
