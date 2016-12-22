namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleNameConverter
    {
        GoogleName Convert(dynamic googleName);
    }
}