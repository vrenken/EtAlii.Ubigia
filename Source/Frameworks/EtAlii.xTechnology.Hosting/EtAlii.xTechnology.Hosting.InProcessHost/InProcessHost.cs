namespace EtAlii.xTechnology.Hosting
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class InProcessHost : HostBase
    {
        protected InProcessHost(IHostConfiguration configuration, ISystemManager systemManager)
            : base(configuration, systemManager)
        {
        }
    }
}