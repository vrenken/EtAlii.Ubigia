namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public interface IThirdParent
    {
        object Instance { get; }
        void Initialize(object instance);
    }
}