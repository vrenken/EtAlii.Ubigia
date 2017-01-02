namespace EtAlii.Servus.Infrastructure.Model.Tests
{
    public class Configuration : IHostingConfiguration
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
