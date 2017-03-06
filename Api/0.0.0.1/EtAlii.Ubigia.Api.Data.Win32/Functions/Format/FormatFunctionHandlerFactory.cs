namespace EtAlii.Ubigia.Api.Functional.Win32
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
