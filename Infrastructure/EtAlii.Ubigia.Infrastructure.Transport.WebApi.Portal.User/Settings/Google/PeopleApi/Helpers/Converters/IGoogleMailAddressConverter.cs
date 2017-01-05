namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    public interface IGoogleMailAddressConverter
    {
        GoogleMailAddress Convert(dynamic googleMailAddress);
    }
}