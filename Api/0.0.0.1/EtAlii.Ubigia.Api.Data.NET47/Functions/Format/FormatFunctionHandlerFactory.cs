namespace EtAlii.Ubigia.Api.Functional.NET47
{
    using EtAlii.Ubigia.Api.Functional;
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
