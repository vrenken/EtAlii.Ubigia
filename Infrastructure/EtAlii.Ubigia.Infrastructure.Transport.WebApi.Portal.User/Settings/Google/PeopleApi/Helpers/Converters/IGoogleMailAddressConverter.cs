namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    public interface IGoogleMailAddressConverter
    {
        GoogleMailAddress Convert(dynamic googleMailAddress);
    }
}