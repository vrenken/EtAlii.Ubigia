namespace EtAlii.Ubigia.Api
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
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
