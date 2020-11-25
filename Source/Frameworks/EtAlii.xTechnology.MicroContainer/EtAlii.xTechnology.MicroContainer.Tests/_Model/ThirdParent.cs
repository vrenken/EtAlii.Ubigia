namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class ThirdParent : IThirdParent
    {
        public object Instance { get; private set; }

        public void Initialize(object instance)
        {
            Instance = instance;
        }
    }
}
