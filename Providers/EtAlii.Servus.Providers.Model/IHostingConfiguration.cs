namespace EtAlii.Servus.Providers.Model
{

    public interface IHostingConfiguration 
    {
        string Account { get; }
        string Password { get; }
        string Address { get; }
    }
}