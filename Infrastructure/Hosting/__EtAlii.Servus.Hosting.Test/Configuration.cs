namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Infrastructure;

    public class Configuration : IHostingConfiguration
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
