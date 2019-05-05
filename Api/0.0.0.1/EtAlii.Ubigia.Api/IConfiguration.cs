namespace EtAlii.Ubigia.Api
{
    /// <summary>
    /// Use this interface to define a configuration that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IConfiguration
    {
        TExtension[] GetExtensions<TExtension>()
            where TExtension : IExtension;
    }
}