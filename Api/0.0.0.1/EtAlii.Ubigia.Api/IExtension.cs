namespace EtAlii.Ubigia.Api
{
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Use this interface to define an extension that can be used by configuration/factory subsystem implementations.
    /// </summary>
    public interface IExtension
    {
        void Initialize(Container container);
    }
}