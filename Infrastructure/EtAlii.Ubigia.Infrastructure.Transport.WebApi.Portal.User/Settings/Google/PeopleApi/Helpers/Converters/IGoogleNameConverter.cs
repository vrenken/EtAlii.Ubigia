namespace EtAlii.Ubigia.Infrastructure.WebApi.Portal.Admin
{
    public interface IGoogleNameConverter
    {
        GoogleName Convert(dynamic googleName);
    }
}