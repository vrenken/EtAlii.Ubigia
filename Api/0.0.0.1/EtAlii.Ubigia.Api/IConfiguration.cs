namespace EtAlii.Ubigia.Api
{
    public interface IConfiguration<TExtension, out TConfiguration> 
        where TExtension : class 
        where TConfiguration : class
    {
        TExtension[] Extensions { get; }

        TConfiguration Use(TExtension[] extensions);
    }
}