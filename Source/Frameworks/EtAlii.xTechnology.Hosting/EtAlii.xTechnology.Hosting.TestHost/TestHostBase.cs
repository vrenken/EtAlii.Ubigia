// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public class TestHostBase<THostManager> : HostBase
        where THostManager: IHostManager, new()
    {
        protected new THostManager Manager => (THostManager)base.Manager;

        protected TestHostBase(IHostOptions options, ISystemManager systemManager)
            : base(options, systemManager)
        {
        }

        public override void Setup(ICommand[] commands, Status[] status)
        {
            var manager = new THostManager();
            base.Setup(commands, status, manager);
        }
    }
}
