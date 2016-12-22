namespace EtAlii.Servus.Infrastructure
{
    public class AuthenticationToken
    {
        public long Salt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}