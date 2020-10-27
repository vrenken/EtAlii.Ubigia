namespace EtAlii.Ubigia
{
    /// <summary>
    /// Use this interface to define a configuration that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Returns the Configuration extensions of the type specified by TExtension. 
        /// </summary>
        /// <typeparam name="TExtension"></typeparam>
        /// <returns></returns>
        TExtension[] GetExtensions<TExtension>()
            where TExtension : IExtension;
    }
}