namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	public class HostTestContextFactory : IHostTestContextFactory
	{
		public IHostTestContext Create()
		{
			return new HostTestContext();
		}
	}
}