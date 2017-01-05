namespace EtAlii.Ubigia.Infrastructure.Transport.WebApi.Portal.Admin
{
    public interface IGoogleNameConverter
    {
        GoogleName Convert(dynamic googleName);
    }
}