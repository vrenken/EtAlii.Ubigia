namespace EtAlii.Ubigia.Api
{
    /// <summary>
    /// Use this interface to define a configuration that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IConfiguration<out TConfiguration> 
        where TConfiguration : class
    {
        TExtension[] GetExtensions<TExtension>()
            where TExtension : IExtension;

        TConfiguration Use(IExtension[] extensions);
    }
}