﻿namespace EtAlii.xTechnology.Hosting
{
    public class TestHostBase<THostManager> : HostBase
        where THostManager: IHostManager, new()
    {
        protected new THostManager Manager => (THostManager)base.Manager;

        protected TestHostBase(IHostConfiguration configuration, ISystemManager systemManager)
            : base(configuration, systemManager)
        {
        }

        public override void Setup(ICommand[] commands, Status[] status)
        {
            var manager = new THostManager();
            base.Setup(commands, status, manager);
        }
    }
}
