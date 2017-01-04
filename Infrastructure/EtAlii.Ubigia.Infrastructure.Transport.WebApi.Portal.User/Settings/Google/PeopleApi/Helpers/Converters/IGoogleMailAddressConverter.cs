namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleMailAddressConverter
    {
        GoogleMailAddress Convert(dynamic googleMailAddress);
    }
}