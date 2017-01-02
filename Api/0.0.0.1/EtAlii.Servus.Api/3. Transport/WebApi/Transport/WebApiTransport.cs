namespace EtAlii.Servus.Api.Transport.WebApi
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class WebApiTransport : TransportBase, ITransport
    {
        protected override IScaffolding[] CreateScaffoldingInternal()
        {
            return new IScaffolding[]
            {
                new WebApiTransportClientsScaffolding(),
            };
        }

        protected override void InvokeInternal(Action<ITransport> invocation)
        {
            invocation(this);
        }
    }
}
