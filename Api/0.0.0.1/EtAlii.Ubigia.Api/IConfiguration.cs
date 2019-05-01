namespace EtAlii.Ubigia.Api
{
    /// <summary>
    /// Use this interface to define a configuration that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IConfiguration<TExtension, out TConfiguration> 
        where TExtension : IExtension
        where TConfiguration : class
    {
        TExtension[] Extensions { get; }

        TConfiguration Use(TExtension[] extensions);
    }
}