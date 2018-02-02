namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    public interface IGoogleNameConverter
    {
        GoogleName Convert(dynamic googleName);
    }
}