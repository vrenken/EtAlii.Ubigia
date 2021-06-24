// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class SystemManager : ISystemManager
    {
	    public ISystem[] Systems { get; private set; }

        public void Setup(ISystem[] systems)
        {
            Systems = systems;
        }

        public async Task Start()
        {
            foreach (var system in Systems)
            {
                await system.Start().ConfigureAwait(false);
            }
        }

        public async Task Stop()
        {
            foreach (var system in Systems.Reverse())
            {
                await system.Stop().ConfigureAwait(false);
            }
        }
    }
}