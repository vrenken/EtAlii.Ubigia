namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleMailAddressConverter
    {
        GoogleMailAddress Convert(dynamic googleMailAddress);
    }
}