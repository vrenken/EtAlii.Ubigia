namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;
    using System.Threading.Tasks;

    class SystemManager : ISystemManager
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
                await system.Start();
            }
        }

        public async Task Stop()
        {
            foreach (var system in Systems.Reverse())
            {
                await system.Stop();
            }
        }
    }
}