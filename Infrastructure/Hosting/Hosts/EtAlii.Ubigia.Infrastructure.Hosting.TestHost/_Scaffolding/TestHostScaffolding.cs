namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore
{
	using EtAlii.xTechnology.MicroContainer;

	public class TestHostScaffolding : IScaffolding
	{
		public void Register(Container container)
		{
            // Register the interface/class singletons needed for test hosting.
        }
    }
}
