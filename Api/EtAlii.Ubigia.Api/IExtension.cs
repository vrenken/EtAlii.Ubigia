namespace EtAlii.Ubigia.Api
{
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Use this interface to define an extension that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// Initialize the Extension by adding the corresponding registrations to the specified container.
        /// </summary>
        /// <param name="container"></param>
        void Initialize(Container container);
    }
}